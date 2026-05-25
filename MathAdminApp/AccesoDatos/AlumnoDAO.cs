// ============================================================
// Capa de Acceso a Datos: AlumnoDAO
// Adaptado al modelo Alumno
// ============================================================
using MathAdminApp.Modelos;
using System.Data.SqlClient;

namespace MathAdminApp.AccesoDatos
{
    internal class AlumnoDAO
    {
        /// <summary>
        /// Obtener todos los alumnos
        /// </summary>
        public List<Alumno> ObtenerAlumnos()
        {
            List<Alumno> lista = new List<Alumno>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idAlumno,
                    idTutor,
                    grado
                FROM Alumnos
                ORDER BY idAlumno";

            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Alumno
                {
                    IdAlumno = lector.GetInt32(0),

                    IdTutor = lector.IsDBNull(1)
                        ? null
                        : lector.GetInt32(1),

                    Grado = lector.IsDBNull(2)
                        ? null
                        : lector.GetInt32(2)
                });
            }

            return lista;
        }

        /// <summary>
        /// Obtener alumno por ID
        /// </summary>
        public Alumno? ObtenerPorId(int idAlumno)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idAlumno,
                    idTutor,
                    grado
                FROM Alumnos
                WHERE idAlumno = @IdAlumno";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdAlumno", idAlumno);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new Alumno
                {
                    IdAlumno = lector.GetInt32(0),

                    IdTutor = lector.IsDBNull(1)
                        ? null
                        : lector.GetInt32(1),

                    Grado = lector.IsDBNull(2)
                        ? null
                        : lector.GetInt32(2)
                };
            }

            return null;
        }

        /// <summary>
        /// Agregar alumno
        /// </summary>
        public bool Agregar(Alumno alumno)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                INSERT INTO Alumnos
                (
                    idAlumno,
                    idTutor,
                    grado
                )
                VALUES
                (
                    @IdAlumno,
                    @IdTutor,
                    @Grado
                )";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdAlumno", alumno.IdAlumno);

            comando.Parameters.AddWithValue(
                "@IdTutor",
                alumno.IdTutor.HasValue
                    ? alumno.IdTutor.Value
                    : DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Grado",
                alumno.Grado.HasValue
                    ? alumno.Grado.Value
                    : DBNull.Value);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Actualizar alumno
        /// </summary>
        public bool Actualizar(Alumno alumno)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                UPDATE Alumnos
                SET
                    idTutor = @IdTutor,
                    grado = @Grado
                WHERE idAlumno = @IdAlumno";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdAlumno", alumno.IdAlumno);

            comando.Parameters.AddWithValue(
                "@IdTutor",
                alumno.IdTutor.HasValue
                    ? alumno.IdTutor.Value
                    : DBNull.Value);

            comando.Parameters.AddWithValue(
                "@Grado",
                alumno.Grado.HasValue
                    ? alumno.Grado.Value
                    : DBNull.Value);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Eliminar alumno
        /// </summary>
        public bool Eliminar(int idAlumno)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                DELETE FROM Alumnos
                WHERE idAlumno = @IdAlumno";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdAlumno", idAlumno);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Contar alumnos
        /// </summary>
        public int ContarAlumnos()
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Alumnos";

            using var comando = new SqlCommand(query, conexion);

            return (int)comando.ExecuteScalar();
        }
    }
}