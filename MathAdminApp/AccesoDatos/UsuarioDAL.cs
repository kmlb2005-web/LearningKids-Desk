// ============================================================
// Capa de Acceso a Datos: UsuarioDAL
// Operaciones CRUD para la tabla Usuarios
// ============================================================

using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Linq;
using MathAdminApp.Modelos;

namespace MathAdminApp.AccesoDatos
{
    /// <summary>
    /// Clase de acceso a datos para la entidad Usuario.
    /// Contiene las operaciones CRUD contra SQL Server.
    /// </summary>
    public class UsuarioDAL
    {
        /// <summary>
        /// Valida las credenciales de un usuario para iniciar sesion.
        /// Retorna el usuario si las credenciales son correctas, null si no.
        /// </summary>
        public Usuario? ValidarLogin(string nombreUsuario, string contrasena)
        {
            // Intentar autenticacion contra la API remota si ApiBaseUrl esta configurada.
            try
            {
                var baseUrl = ConexionBD.ApiBaseUrl?.TrimEnd('/') ?? string.Empty;
                if (!string.IsNullOrWhiteSpace(baseUrl))
                {
                    using var http = new HttpClient { BaseAddress = new Uri(baseUrl) };

                    var payload = new
                    {
                        username = nombreUsuario,
                        password = contrasena,
                        nombreUsuario = nombreUsuario,
                        contrasena = contrasena
                    };

                    var resp = http.PostAsJsonAsync("/api/usuarios/login/personal", payload).GetAwaiter().GetResult();
                    if (!resp.IsSuccessStatusCode) return null;

                    var loginResp = resp.Content.ReadFromJsonAsync<ApiLoginResponse>().GetAwaiter().GetResult();
                    if (loginResp?.Usuario == null) return null;

                    return new Usuario
                    {
                        Id = loginResp.Usuario.IdUsuario,
                        Nombre = loginResp.Usuario.Nombre ?? string.Empty,
                        Correo = string.Empty,
                        NombreUsuario = loginResp.Usuario.Username ?? string.Empty,
                        Grado = string.Empty,
                        Rol = loginResp.Usuario.RolNombre ?? string.Empty,
                        RolNombre = loginResp.Usuario.RolNombre ?? string.Empty,
                        IdRol = loginResp.Usuario.IdRol,
                        Activo = true,
                        FechaCreacion = DateTime.Now
                    };
                }
            }
            catch
            {
                // Si falla la llamada a la API, devolvemos null para indicar credenciales invalidas / error.
                return null;
            }

            // Si no hay ApiBaseUrl configurada o la llamada no fue exitosa, devolvemos null (sin fallback a SQL).
            return null;
        }

        /// <summary>
        /// Obtiene todos los usuarios desde la API (/api/usuarios).
        /// </summary>
        public List<Usuario> ObtenerUsuarios()
        {
            try
            {
                using var http = GetHttpClient();
                var resp = http.GetAsync("/api/usuarios").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    var apiList = resp.Content.ReadFromJsonAsync<List<ApiUsuario>>().GetAwaiter().GetResult();
                    if (apiList != null)
                    {
                        return apiList.Select(MapApiUsuario).ToList();
                    }
                }
            }
            catch
            {
                // ignore
            }

            return new List<Usuario>();
        }

        /// <summary>
        /// Obtiene los alumnos relacionados a un docente mediante el endpoint /api/DocenteAlumnos/{docenteId}.
        /// </summary>
        public List<Usuario> ObtenerAlumnosPorDocente(int docenteId)
        {
            try
            {
                using var http = GetHttpClient();
                var resp = http.GetAsync($"/api/DocenteAlumnos/{docenteId}").GetAwaiter().GetResult();
                if (!resp.IsSuccessStatusCode) return new List<Usuario>();

                var content = resp.Content.ReadAsStringAsync().GetAwaiter().GetResult();
                if (string.IsNullOrWhiteSpace(content)) return new List<Usuario>();

                // El endpoint ahora devuelve una estructura con 'alumnos' dentro
                try
                {
                    var dto = JsonSerializer.Deserialize<DocenteAlumnosResponse>(content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                    if (dto?.Alumnos == null || dto.Alumnos.Count == 0) return new List<Usuario>();

                    return dto.Alumnos.Select(a => new Usuario
                    {
                        Id = a.IdAlumno,
                        Nombre = a.NombreAlumno ?? string.Empty,
                        NombreUsuario = a.UsernameAlumno ?? string.Empty,
                        Correo = string.Empty,
                        Contrasena = string.Empty,
                        Grado = a.Grado.ToString(),
                        IdRol = 3,
                        RolNombre = "ALUMNO",
                        Rol = "Alumno",
                        Activo = true,
                        FechaCreacion = DateTime.Now
                    }).ToList();
                }
                catch
                {
                    // Si falla deserializacion, devolver vacio
                    return new List<Usuario>();
                }
            }
            catch
            {
                // ignore
            }

            return new List<Usuario>();
        }

        private class ApiLoginResponse
        {
            [JsonPropertyName("message")]
            public string? Message { get; set; }

            [JsonPropertyName("usuario")]
            public ApiUser? Usuario { get; set; }
        }

        private class ApiUser
        {
            [JsonPropertyName("idUsuario")]
            public int IdUsuario { get; set; }

            [JsonPropertyName("nombre")]
            public string? Nombre { get; set; }

            [JsonPropertyName("username")]
            public string? Username { get; set; }

            [JsonPropertyName("idRol")]
            public int IdRol { get; set; }

            [JsonPropertyName("rolNombre")]
            public string? RolNombre { get; set; }
        }

        /// <summary>
        /// Obtiene la lista de todos los alumnos (rol = "ALUMNO") desde la API.
        /// Hace fallback a la DB si la llamada a la API falla.
        /// </summary>
        public List<Usuario> ObtenerAlumnos()
        {
            try
            {
                using var http = GetHttpClient();
                var resp = http.GetAsync("/api/usuarios").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    var apiList = resp.Content.ReadFromJsonAsync<List<ApiUsuario>>().GetAwaiter().GetResult();
                    if (apiList != null)
                    {
                        return apiList
                            .Where(u => string.Equals(u.Rol?.Nombre, "ALUMNO", StringComparison.OrdinalIgnoreCase) || u.IdRol == 3)
                            .Select(MapApiUsuario)
                            .OrderBy(u => u.Nombre)
                            .ToList();
                    }
                }
            }
            catch
            {
                // ignore and fallback to DB
            }
            // Si la API no responde o retorna error, devolvemos lista vacía (sin fallback a SQL local).
            return new List<Usuario>();
        }

        /// <summary>
        /// Agrega un nuevo alumno a la base de datos.
        /// </summary>
        public bool Agregar(Usuario usuario)
        {
            // Intentar crear usuario en la API
            try
            {
                using var http = GetHttpClient();

                var payload = new
                {
                    nombre = usuario.Nombre,
                    username = usuario.NombreUsuario,
                    password = usuario.Contrasena,
                    correo = usuario.Correo,
                    grado = usuario.Grado,
                    idRol = MapRolToId(usuario.Rol)
                };

                var resp = http.PostAsJsonAsync("/api/usuarios", payload).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                    return true;
            }
            catch
            {
                // ignore and fallback
            }
            // Si la API falla o devuelve error, no se realiza la insercion local (comportamiento API-only)
            return false;
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        public bool Actualizar(Usuario usuario)
        {
            // Intentar actualizar via API
            try
            {
                using var http = GetHttpClient();

                var payload = new
                {
                    idUsuario = usuario.Id,
                    nombre = usuario.Nombre,
                    username = usuario.NombreUsuario,
                    password = usuario.Contrasena,
                    correo = usuario.Correo,
                    grado = usuario.Grado,
                    idRol = MapRolToId(usuario.Rol)
                };

                var resp = http.PutAsJsonAsync($"/api/usuarios/{usuario.Id}", payload).GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode) return true;
            }
            catch
            {
                // ignore and fallback
            }
            // Si la actualización por API falla, devolvemos false (sin fallback a SQL local).
            return false;
        }

        /// <summary>
        /// Desactiva un usuario (no lo elimina fisicamente).
        /// </summary>
        public bool Desactivar(int id)
        {
            // Intentar eliminar via API (DELETE)
            try
            {
                using var http = GetHttpClient();
                var resp = http.DeleteAsync($"/api/usuarios/{id}").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode) return true;
            }
            catch
            {
                // ignore and fallback
            }
            // Si la eliminación por API falla, devolvemos false (sin fallback a SQL local).
            return false;
        }

        /// <summary>
        /// Obtiene el total de alumnos activos.
        /// </summary>
        public int ContarAlumnos()
        {
            try
            {
                using var http = GetHttpClient();
                var resp = http.GetAsync("/api/usuarios").GetAwaiter().GetResult();
                if (resp.IsSuccessStatusCode)
                {
                    var apiList = resp.Content.ReadFromJsonAsync<List<ApiUsuario>>().GetAwaiter().GetResult();
                    if (apiList != null)
                    {
                        return apiList.Count(u => string.Equals(u.Rol?.Nombre, "ALUMNO", StringComparison.OrdinalIgnoreCase) || u.IdRol == 3);
                    }
                }
            }
            catch
            {
                // ignore and fallback
            }
            // Si la llamada a la API falla, devolvemos 0 (sin fallback a SQL local).
            return 0;
        }

        // =====================
        // Helpers & DTOs
        // =====================
        private static HttpClient GetHttpClient()
        {
            var baseUrl = ConexionBD.ApiBaseUrl?.TrimEnd('/') ?? string.Empty;
            var client = new HttpClient();
            if (!string.IsNullOrWhiteSpace(baseUrl)) client.BaseAddress = new Uri(baseUrl);
            return client;
        }

        private static Usuario MapApiUsuario(ApiUsuario a)
        {
            return new Usuario
            {
                Id = a.IdUsuario,
                Nombre = a.Nombre ?? string.Empty,
                Correo = a.Correo ?? string.Empty,
                NombreUsuario = a.Username ?? string.Empty,
                Contrasena = a.Password ?? string.Empty,
                Grado = a.Grado ?? string.Empty,
                IdRol = a.IdRol,
                RolNombre = a.Rol?.Nombre ?? string.Empty,
                Rol = a.Rol?.Nombre switch
                {
                    null => (a.IdRol == 1 ? "Administrador" : a.IdRol == 2 ? "Docente" : "Alumno"),
                    var s when string.Equals(s, "ADMIN", StringComparison.OrdinalIgnoreCase) => "Administrador",
                    var s when string.Equals(s, "DOCENTE", StringComparison.OrdinalIgnoreCase) => "Docente",
                    var s when string.Equals(s, "ALUMNO", StringComparison.OrdinalIgnoreCase) => "Alumno",
                    _ => a.Rol?.Nombre ?? string.Empty
                },
                Activo = true,
                FechaCreacion = DateTime.Now
            };
        }

        private static int MapRolToId(string? rol)
        {
            if (string.IsNullOrWhiteSpace(rol)) return 3;
            return rol.Trim().ToLower() switch
            {
                "administrador" => 1,
                "admin" => 1,
                "docente" => 2,
                "alumno" => 3,
                _ => 3
            };
        }

        private class ApiUsuario
        {
            [JsonPropertyName("idUsuario")] public int IdUsuario { get; set; }
            [JsonPropertyName("nombre")] public string? Nombre { get; set; }
            [JsonPropertyName("username")] public string? Username { get; set; }
            [JsonPropertyName("password")] public string? Password { get; set; }
            [JsonPropertyName("idRol")] public int IdRol { get; set; }
            [JsonPropertyName("correo")] public string? Correo { get; set; }
            [JsonPropertyName("grado")] public string? Grado { get; set; }
            [JsonPropertyName("rol")] public ApiRol? Rol { get; set; }
        }

        private class ApiRol
        {
            [JsonPropertyName("idRol")] public int IdRol { get; set; }
            [JsonPropertyName("nombre")] public string? Nombre { get; set; }
        }

        private class DocenteAlumnoRelation
        {
            [JsonPropertyName("id")] public int Id { get; set; }
            [JsonPropertyName("idDocente")] public int IdDocente { get; set; }
            [JsonPropertyName("idAlumno")] public int IdAlumno { get; set; }
            [JsonPropertyName("nombreDocente")] public string? NombreDocente { get; set; }
            [JsonPropertyName("nombreAlumno")] public string? NombreAlumno { get; set; }
        }

        private class DocenteAlumnosResponse
        {
            [JsonPropertyName("idDocente")] public int IdDocente { get; set; }
            [JsonPropertyName("nombreDocente")] public string? NombreDocente { get; set; }
            [JsonPropertyName("usernameDocente")] public string? UsernameDocente { get; set; }
            [JsonPropertyName("alumnos")] public List<AlumnoDto>? Alumnos { get; set; }
        }

        private class AlumnoDto
        {
            [JsonPropertyName("idAlumno")] public int IdAlumno { get; set; }
            [JsonPropertyName("nombreAlumno")] public string? NombreAlumno { get; set; }
            [JsonPropertyName("usernameAlumno")] public string? UsernameAlumno { get; set; }
            [JsonPropertyName("grado")] public int Grado { get; set; }
        }
    }
}
