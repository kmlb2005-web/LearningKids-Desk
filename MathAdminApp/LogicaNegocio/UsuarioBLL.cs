// ============================================================
// Capa de Logica de Negocio: UsuarioBLL
// Logica de negocio para operaciones de usuarios
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    /// <summary>
    /// Clase de logica de negocio para la entidad Usuario.
    /// Contiene validaciones y reglas de negocio.
    /// </summary>
    public class UsuarioBLL
    {
        private readonly UsuarioDAL _dal = new();

        /// <summary>
        /// Valida credenciales y retorna el usuario si es correcto.
        /// </summary>
        public Usuario? IniciarSesion(string nombreUsuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("La contrasena es obligatoria.");

            return _dal.ValidarLogin(nombreUsuario.Trim(), contrasena);
        }

        /// <summary>
        /// Obtiene la lista de todos los alumnos.
        /// </summary>
        public List<Usuario> ObtenerAlumnos() => _dal.ObtenerAlumnos();

        /// <summary>
        /// Devuelve los usuarios visibles para el usuario actual según su rol.
        /// - Administrador: todos los usuarios.
        /// - Docente: solo los alumnos relacionados con el docente.
        /// </summary>
        public List<Usuario> ObtenerUsuariosVisibles(Usuario usuarioActual)
        {
            if (usuarioActual == null) return new List<Usuario>();

            // Preferir RolNombre (viene de la API: ADMIN, DOCENTE, ALUMNO)
            var rolApi = usuarioActual.RolNombre?.Trim().ToUpperInvariant() ?? string.Empty;

            if (rolApi == "ADMIN" || string.Equals(usuarioActual.Rol, "Administrador", StringComparison.OrdinalIgnoreCase))
            {
                return _dal.ObtenerUsuarios();
            }

            if (rolApi == "DOCENTE" || string.Equals(usuarioActual.Rol, "Docente", StringComparison.OrdinalIgnoreCase))
            {
                return _dal.ObtenerAlumnosPorDocente(usuarioActual.Id);
            }

            // Por defecto devolver solo alumnos
            return _dal.ObtenerAlumnos();
        }

        /// <summary>
        /// Agrega un nuevo alumno con validaciones.
        /// </summary>
        public bool AgregarAlumno(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(usuario.Correo))
                throw new ArgumentException("El correo es obligatorio.");
            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio.");
            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
                throw new ArgumentException("La contrasena es obligatoria.");

            usuario.Rol = "Alumno";
            return _dal.Agregar(usuario);
        }

        /// <summary>
        /// Actualiza los datos de un alumno.
        /// </summary>
        public bool ActualizarAlumno(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(usuario.Correo))
                throw new ArgumentException("El correo es obligatorio.");

            return _dal.Actualizar(usuario);
        }

        /// <summary>
        /// Desactiva un alumno.
        /// </summary>
        public bool DesactivarAlumno(int id) => _dal.Desactivar(id);

        /// <summary>
        /// Cuenta el total de alumnos activos.
        /// </summary>
        public int ContarAlumnos() => _dal.ContarAlumnos();
    }
}
