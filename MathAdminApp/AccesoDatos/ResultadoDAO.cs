// ============================================================
// Capa de Acceso a Datos: ResultadoDAO
// Adaptado al modelo Resultado
// ============================================================
using MathAdminApp.Modelos;
using System.Data.SqlClient;

namespace MathAdminApp.AccesoDatos
{
    internal class ResultadoDAO
    {
        /// <summary>
        /// Obtener todos los resultados
        /// </summary>
        public List<Resultado> ObtenerResultados()
        {
            List<Resultado> lista = new List<Resultado>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idResultado,
                    idAlumno,
                    idPrueba,
                    calificacion,
                    fecha
                FROM Resultados
                ORDER BY fecha DESC";

            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Resultado
                {
                    IdResultado = lector.GetInt32(0),
                    IdAlumno = lector.GetInt32(1),
                    IdPrueba = lector.GetInt32(2),
                    Calificacion = lector.GetDecimal(3),
                    Fecha = lector.GetDateTime(4)
                });
            }

            return lista;
        }

        public List<Resultado> ObtenerPorDocente(int idDocente)
        {
            List<Resultado> lista = new List<Resultado>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    r.idResultado,
                    r.idAlumno,
                    r.idPrueba,
                    r.calificacion,
                    r.fecha
                FROM Resultados r
                INNER JOIN DocenteAlumno da
                    ON da.idAlumno = r.idAlumno
                WHERE da.idDocente = @IdDocente
                ORDER BY r.fecha DESC";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@IdDocente", idDocente);

            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Resultado
                {
                    IdResultado = lector.GetInt32(0),
                    IdAlumno = lector.GetInt32(1),
                    IdPrueba = lector.GetInt32(2),
                    Calificacion = lector.GetDecimal(3),
                    Fecha = lector.GetDateTime(4)
                });
            }

            return lista;
        }

        /// <summary>
        /// Obtener resultado por ID
        /// </summary>
        public Resultado? ObtenerPorId(int idResultado)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idResultado,
                    idAlumno,
                    idPrueba,
                    calificacion,
                    fecha
                FROM Resultados
                WHERE idResultado = @IdResultado";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdResultado", idResultado);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new Resultado
                {
                    IdResultado = lector.GetInt32(0),
                    IdAlumno = lector.GetInt32(1),
                    IdPrueba = lector.GetInt32(2),
                    Calificacion = lector.GetDecimal(3),
                    Fecha = lector.GetDateTime(4)
                };
            }

            return null;
        }

        /// <summary>
        /// Obtener resultados por alumno
        /// </summary>
        /// 


        public List<Resultado> ObtenerPorAlumno(int idAlumno)
        {
            List<Resultado> lista = new List<Resultado>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idResultado,
                    idAlumno,
                    idPrueba,
                    calificacion,
                    fecha
                FROM Resultados
                WHERE idAlumno = @IdAlumno
                ORDER BY fecha DESC";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdAlumno", idAlumno);

            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Resultado
                {
                    IdResultado = lector.GetInt32(0),
                    IdAlumno = lector.GetInt32(1),
                    IdPrueba = lector.GetInt32(2),
                    Calificacion = lector.GetDecimal(3),
                    Fecha = lector.GetDateTime(4)
                });
            }

            return lista;
        }

        /// <summary>
        /// Agregar resultado
        /// </summary>
        public bool Agregar(Resultado resultado)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                INSERT INTO Resultados
                (
                    idAlumno,
                    idPrueba,
                    calificacion,
                    fecha
                )
                VALUES
                (
                    @IdAlumno,
                    @IdPrueba,
                    @Calificacion,
                    @Fecha
                )";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdAlumno", resultado.IdAlumno);
            comando.Parameters.AddWithValue("@IdPrueba", resultado.IdPrueba);
            comando.Parameters.AddWithValue("@Calificacion", resultado.Calificacion);
            comando.Parameters.AddWithValue("@Fecha", resultado.Fecha);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Actualizar resultado
        /// </summary>
        public bool Actualizar(Resultado resultado)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                UPDATE Resultados
                SET
                    idAlumno = @IdAlumno,
                    idPrueba = @IdPrueba,
                    calificacion = @Calificacion,
                    fecha = @Fecha
                WHERE idResultado = @IdResultado";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdResultado", resultado.IdResultado);
            comando.Parameters.AddWithValue("@IdAlumno", resultado.IdAlumno);
            comando.Parameters.AddWithValue("@IdPrueba", resultado.IdPrueba);
            comando.Parameters.AddWithValue("@Calificacion", resultado.Calificacion);
            comando.Parameters.AddWithValue("@Fecha", resultado.Fecha);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Eliminar resultado
        /// </summary>
        public bool Eliminar(int idResultado)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                DELETE FROM Resultados
                WHERE idResultado = @IdResultado";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdResultado", idResultado);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Contar resultados
        /// </summary>
        public int ContarResultados()
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Resultados";

            using var comando = new SqlCommand(query, conexion);

            return (int)comando.ExecuteScalar();
        }
    }
}
