// ============================================================
// Capa de Acceso a Datos: UsuarioDAL
// Operaciones CRUD para la tabla Usuarios
// ============================================================

using System.Data.SqlClient;
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
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"SELECT Id, Nombre, Correo, NombreUsuario, Grado, Rol, Activo, FechaCreacion 
                             FROM Usuarios 
                             WHERE NombreUsuario = @NombreUsuario 
                               AND Contrasena = @Contrasena 
                               AND Activo = 1";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@NombreUsuario", nombreUsuario);
            comando.Parameters.AddWithValue("@Contrasena", contrasena);

            using var lector = comando.ExecuteReader();
            if (lector.Read())
            {
                return new Usuario
                {
                    Id = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Correo = lector.GetString(2),
                    NombreUsuario = lector.GetString(3),
                    Grado = lector.GetString(4),
                    Rol = lector.GetString(5),
                    Activo = lector.GetBoolean(6),
                    FechaCreacion = lector.GetDateTime(7)
                };
            }
            return null;
        }

        /// <summary>
        /// Obtiene la lista de todos los alumnos (rol = "Alumno").
        /// </summary>
        public List<Usuario> ObtenerAlumnos()
        {
            var lista = new List<Usuario>();
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"SELECT Id, Nombre, Correo, NombreUsuario, Grado, Rol, Activo, FechaCreacion 
                             FROM Usuarios 
                             WHERE Rol = 'Alumno' 
                             ORDER BY Nombre";

            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(new Usuario
                {
                    Id = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Correo = lector.GetString(2),
                    NombreUsuario = lector.GetString(3),
                    Grado = lector.GetString(4),
                    Rol = lector.GetString(5),
                    Activo = lector.GetBoolean(6),
                    FechaCreacion = lector.GetDateTime(7)
                });
            }
            return lista;
        }

        /// <summary>
        /// Agrega un nuevo alumno a la base de datos.
        /// </summary>
        public bool Agregar(Usuario usuario)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"INSERT INTO Usuarios (Nombre, Correo, NombreUsuario, Contrasena, Grado, Rol, Activo, FechaCreacion) 
                             VALUES (@Nombre, @Correo, @NombreUsuario, @Contrasena, @Grado, @Rol, 1, GETDATE())";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@Correo", usuario.Correo);
            comando.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
            comando.Parameters.AddWithValue("@Contrasena", usuario.Contrasena);
            comando.Parameters.AddWithValue("@Grado", usuario.Grado);
            comando.Parameters.AddWithValue("@Rol", usuario.Rol);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Actualiza los datos de un usuario existente.
        /// </summary>
        public bool Actualizar(Usuario usuario)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"UPDATE Usuarios 
                             SET Nombre = @Nombre, 
                                 Correo = @Correo, 
                                 NombreUsuario = @NombreUsuario, 
                                 Grado = @Grado 
                             WHERE Id = @Id";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@Correo", usuario.Correo);
            comando.Parameters.AddWithValue("@NombreUsuario", usuario.NombreUsuario);
            comando.Parameters.AddWithValue("@Grado", usuario.Grado);
            comando.Parameters.AddWithValue("@Id", usuario.Id);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Desactiva un usuario (no lo elimina fisicamente).
        /// </summary>
        public bool Desactivar(int id)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "UPDATE Usuarios SET Activo = 0 WHERE Id = @Id";
            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@Id", id);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Obtiene el total de alumnos activos.
        /// </summary>
        public int ContarAlumnos()
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Usuarios WHERE Rol = 'Alumno' AND Activo = 1";
            using var comando = new SqlCommand(query, conexion);
            return (int)comando.ExecuteScalar();
        }
    }
}
