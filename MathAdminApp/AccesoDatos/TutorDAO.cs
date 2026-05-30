// ============================================================
// Capa de Acceso a Datos: TutorDAL
// Adaptado al modelo Tutor
// ============================================================
using MathAdminApp.Modelos;
using System.Data.SqlClient;

namespace MathAdminApp.AccesoDatos
{
    internal class TutorDAO
    {
        /// <summary>
        /// Obtener todos los tutores
        /// </summary>
        public List<Tutor> ObtenerTutores()
        {
            List<Tutor> lista = new List<Tutor>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idTutor,
                    nombre,
                    correo
                FROM Tutores
                WHERE ISNULL(activo, 1) = 1
                ORDER BY nombre";

            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Tutor
                {
                    IdTutor = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Correo = lector.GetString(2)
                });
            }

            return lista;
        }

        /// <summary>
        /// Obtener tutor por ID
        /// </summary>
        public Tutor? ObtenerPorId(int idTutor)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idTutor,
                    nombre,
                    correo
                FROM Tutores
                WHERE idTutor = @IdTutor
                  AND ISNULL(activo, 1) = 1";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdTutor", idTutor);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new Tutor
                {
                    IdTutor = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Correo = lector.GetString(2)
                };
            }

            return null;
        }

        /// <summary>
        /// Obtener tutor por correo
        /// </summary>
        public Tutor? ObtenerPorCorreo(string correo)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idTutor,
                    nombre,
                    correo
                FROM Tutores
                WHERE correo = @Correo
                  AND ISNULL(activo, 1) = 1";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Correo", correo);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new Tutor
                {
                    IdTutor = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Correo = lector.GetString(2)
                };
            }

            return null;
        }

        /// <summary>
        /// Agregar tutor
        /// </summary>
        public bool Agregar(Tutor tutor)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                INSERT INTO Tutores
                (
                    nombre,
                    correo,
                    activo
                )
                VALUES
                (
                    @Nombre,
                    @Correo,
                    1
                )";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Nombre", tutor.Nombre);
            comando.Parameters.AddWithValue("@Correo", tutor.Correo);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Actualizar tutor
        /// </summary>
        public bool Actualizar(Tutor tutor)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                UPDATE Tutores
                SET
                    nombre = @Nombre,
                    correo = @Correo
                WHERE idTutor = @IdTutor";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdTutor", tutor.IdTutor);
            comando.Parameters.AddWithValue("@Nombre", tutor.Nombre);
            comando.Parameters.AddWithValue("@Correo", tutor.Correo);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Eliminar tutor
        /// </summary>
        public bool Eliminar(int idTutor)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                UPDATE Tutores
                SET activo = 0
                WHERE idTutor = @IdTutor";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdTutor", idTutor);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Contar tutores
        /// </summary>
        public int ContarTutores()
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Tutores WHERE ISNULL(activo, 1) = 1";

            using var comando = new SqlCommand(query, conexion);

            return (int)comando.ExecuteScalar();
        }
    }
}
