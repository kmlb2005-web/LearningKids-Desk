// ============================================================
// Capa de Acceso a Datos: UsuarioDAL
// Adaptado al modelo Usuario actual
// ============================================================

using System.Data.SqlClient;
using MathAdminApp.Modelos;

namespace MathAdminApp.AccesoDatos
{
    public class UsuarioDAO
    {
        /// <summary>
        /// Validar login
        /// </summary>
        public Usuario? ValidarLogin(string username, string password)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT 
                    idUsuario,
                    ISNULL(nombre, ''),
                    ISNULL(username, ''),
                    ISNULL(password, ''),
                    ISNULL(idRol, 0)
                FROM Usuarios
                WHERE username = @Username
                  AND password = @Password
                  AND ISNULL(activo, 1) = 1";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Username", username);
            comando.Parameters.AddWithValue("@Password", password);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new Usuario
                {
                    IdUsuario = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Username = lector.GetString(2),
                    Password = lector.GetString(3),
                    IdRol = lector.GetInt32(4)
                };
            }

            return null;
        }

        /// <summary>
        /// Obtener todos los usuarios
        /// </summary>
        public List<Usuario> ObtenerUsuarios()
        {
            List<Usuario> lista = new List<Usuario>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT 
                    idUsuario,
                    ISNULL(nombre, ''),
                    ISNULL(username, ''),
                    ISNULL(password, ''),
                    ISNULL(idRol, 0)
                FROM Usuarios
                WHERE ISNULL(activo, 1) = 1
                ORDER BY nombre";

            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Usuario
                {
                    IdUsuario = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Username = lector.GetString(2),
                    Password = lector.GetString(3),
                    IdRol = lector.GetInt32(4)
                });
            }

            return lista;
        }

        /// <summary>
        /// Obtener solo alumnos
        /// </summary>
        public List<Usuario> ObtenerAlumnos()
        {
            List<Usuario> lista = new List<Usuario>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT 
                    u.idUsuario,
                    ISNULL(u.nombre, ''),
                    ISNULL(u.username, ''),
                    ISNULL(u.password, ''),
                    ISNULL(u.idRol, 0)
                FROM Usuarios u
                INNER JOIN Roles r 
                    ON r.idRol = u.idRol
                WHERE UPPER(r.nombre) = 'ALUMNO'
                  AND ISNULL(u.activo, 1) = 1
                  AND ISNULL(r.activo, 1) = 1
                ORDER BY u.nombre";

            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Usuario
                {
                    IdUsuario = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Username = lector.GetString(2),
                    Password = lector.GetString(3),
                    IdRol = lector.GetInt32(4)
                });
            }

            return lista;
        }

        public List<Usuario> ObtenerAlumnosPorDocente(int idDocente)
        {
            List<Usuario> lista = new List<Usuario>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    u.idUsuario,
                    ISNULL(u.nombre, ''),
                    ISNULL(u.username, ''),
                    ISNULL(u.password, ''),
                    ISNULL(u.idRol, 0)
                FROM Usuarios u
                INNER JOIN DocenteAlumno da
                    ON da.idAlumno = u.idUsuario
                INNER JOIN Roles r
                    ON r.idRol = u.idRol
                WHERE da.idDocente = @IdDocente
                  AND UPPER(r.nombre) = 'ALUMNO'
                  AND ISNULL(u.activo, 1) = 1
                  AND ISNULL(da.activo, 1) = 1
                  AND ISNULL(r.activo, 1) = 1
                ORDER BY u.nombre";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@IdDocente", idDocente);

            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Usuario
                {
                    IdUsuario = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Username = lector.GetString(2),
                    Password = lector.GetString(3),
                    IdRol = lector.GetInt32(4)
                });
            }

            return lista;
        }

        /// <summary>
        /// Agregar usuario
        /// </summary>
        public bool Agregar(Usuario usuario)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            SqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                string query = @"
                    INSERT INTO Usuarios
                    (
                        nombre,
                        username,
                        password,
                        idRol,
                        activo
                    )
                    VALUES
                    (
                        @Nombre,
                        @Username,
                        @Password,
                        @IdRol,
                        1
                    );

                    SELECT SCOPE_IDENTITY();";

                using var comando = new SqlCommand(query, conexion, transaccion);

                comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
                comando.Parameters.AddWithValue("@Username", usuario.Username);
                comando.Parameters.AddWithValue("@Password", usuario.Password);
                comando.Parameters.AddWithValue("@IdRol", usuario.IdRol);

                int idUsuario = Convert.ToInt32(comando.ExecuteScalar());
                usuario.IdUsuario = idUsuario;

                // Si es alumno (rol 3)
                if (usuario.IdRol == 3)
                {
                    string queryAlumno = @"
                        INSERT INTO Alumnos
                        (
                            idAlumno,
                            idTutor,
                            grado,
                            activo
                        )
                        VALUES
                        (
                            @IdAlumno,
                            NULL,
                            NULL,
                            1
                        )";

                    using var comandoAlumno =
                        new SqlCommand(queryAlumno, conexion, transaccion);

                    comandoAlumno.Parameters.AddWithValue("@IdAlumno", idUsuario);

                    comandoAlumno.ExecuteNonQuery();
                }

                transaccion.Commit();
                return true;
            }
            catch
            {
                transaccion.Rollback();
                return false;
            }
        }

        /// <summary>
        /// Actualizar usuario
        /// </summary>
        public bool Actualizar(Usuario usuario)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                UPDATE Usuarios
                SET
                    nombre = @Nombre,
                    username = @Username,
                    password = @Password,
                    idRol = @IdRol
                WHERE idUsuario = @IdUsuario";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Nombre", usuario.Nombre);
            comando.Parameters.AddWithValue("@Username", usuario.Username);
            comando.Parameters.AddWithValue("@Password", usuario.Password);
            comando.Parameters.AddWithValue("@IdRol", usuario.IdRol);
            comando.Parameters.AddWithValue("@IdUsuario", usuario.IdUsuario);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Eliminar usuario
        /// </summary>
        public bool Eliminar(int idUsuario)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            SqlTransaction transaccion = conexion.BeginTransaction();

            try
            {
                string queryDocenteAlumno =
                    "UPDATE DocenteAlumno SET activo = 0 WHERE idAlumno = @IdUsuario OR idDocente = @IdUsuario";

                using var comandoDocenteAlumno =
                    new SqlCommand(queryDocenteAlumno, conexion, transaccion);

                comandoDocenteAlumno.Parameters.AddWithValue("@IdUsuario", idUsuario);
                comandoDocenteAlumno.ExecuteNonQuery();

                string queryAlumno =
                    "UPDATE Alumnos SET activo = 0 WHERE idAlumno = @IdUsuario";

                using var comandoAlumno =
                    new SqlCommand(queryAlumno, conexion, transaccion);

                comandoAlumno.Parameters.AddWithValue("@IdUsuario", idUsuario);

                comandoAlumno.ExecuteNonQuery();

                string queryUsuario =
                    "UPDATE Usuarios SET activo = 0 WHERE idUsuario = @IdUsuario";

                using var comandoUsuario =
                    new SqlCommand(queryUsuario, conexion, transaccion);

                comandoUsuario.Parameters.AddWithValue("@IdUsuario", idUsuario);

                int filas = comandoUsuario.ExecuteNonQuery();

                transaccion.Commit();

                return filas > 0;
            }
            catch
            {
                transaccion.Rollback();
                return false;
            }
        }

        /// <summary>
        /// Contar alumnos
        /// </summary>
        public int ContarAlumnos()
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT COUNT(*)
                FROM Usuarios u
                INNER JOIN Roles r
                    ON r.idRol = u.idRol
                WHERE UPPER(r.nombre) = 'ALUMNO'
                  AND ISNULL(u.activo, 1) = 1
                  AND ISNULL(r.activo, 1) = 1";

            using var comando = new SqlCommand(query, conexion);

            return (int)comando.ExecuteScalar();
        }
    }
}
