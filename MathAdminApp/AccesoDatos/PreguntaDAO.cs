// ============================================================
// Capa de Acceso a Datos: PreguntaDAL
// CRUD para tabla Pregunta
// ============================================================

using System.Data.SqlClient;
using MathAdminApp.Modelos;

namespace MathAdminApp.AccesoDatos
{
    public class PreguntaDAO
    {
        // =====================================================
        // OBTENER POR PRUEBA
        // =====================================================
        public List<Pregunta> ObtenerPorPrueba(int idPrueba)
        {
            var lista = new List<Pregunta>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT 
                    idPregunta,
                    texto,
                    idPrueba
                FROM Preguntas
                WHERE idPrueba = @IdPrueba
                ORDER BY idPregunta";

            using var comando =
                new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue(
                "@IdPrueba",
                idPrueba
            );

            using var lector =
                comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Pregunta
                {
                    IdPregunta = lector.GetInt32(0),
                    Texto = lector.GetString(1),
                    IdPrueba = lector.GetInt32(2)
                });
            }

            return lista;
        }

        // =====================================================
        // AGREGAR
        // =====================================================
        public bool Agregar(Pregunta pregunta)
        {
            using var conexion =
                ConexionBD.ObtenerConexion();

            conexion.Open();

            string query = @"
                INSERT INTO Preguntas
                (
                    texto,
                    idPrueba
                )
                VALUES
                (
                    @Texto,
                    @IdPrueba
                );

                SELECT SCOPE_IDENTITY();";

            using var comando =
                new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue(
                "@Texto",
                pregunta.Texto
            );

            comando.Parameters.AddWithValue(
                "@IdPrueba",
                pregunta.IdPrueba
            );

            pregunta.IdPregunta = Convert.ToInt32(comando.ExecuteScalar());

            return pregunta.IdPregunta > 0;
        }

        // =====================================================
        // ACTUALIZAR
        // =====================================================
        public bool Actualizar(Pregunta pregunta)
        {
            using var conexion =
                ConexionBD.ObtenerConexion();

            conexion.Open();

            string query = @"
                UPDATE Preguntas
                SET
                    texto = @Texto,
                    idPrueba = @IdPrueba
                WHERE idPregunta = @IdPregunta";

            using var comando =
                new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue(
                "@IdPregunta",
                pregunta.IdPregunta
            );

            comando.Parameters.AddWithValue(
                "@Texto",
                pregunta.Texto
            );

            comando.Parameters.AddWithValue(
                "@IdPrueba",
                pregunta.IdPrueba
            );

            return comando.ExecuteNonQuery() > 0;
        }

        // =====================================================
        // ELIMINAR
        // =====================================================
        public bool Eliminar(int idPregunta)
        {
            using var conexion =
                ConexionBD.ObtenerConexion();

            conexion.Open();

            using var transaccion = conexion.BeginTransaction();

            try
            {
                using (var eliminarRespuestas = new SqlCommand(
                    "DELETE FROM Respuestas WHERE idPregunta = @IdPregunta",
                    conexion,
                    transaccion))
                {
                    eliminarRespuestas.Parameters.AddWithValue("@IdPregunta", idPregunta);
                    eliminarRespuestas.ExecuteNonQuery();
                }

                using var eliminarPregunta = new SqlCommand(
                    "DELETE FROM Preguntas WHERE idPregunta = @IdPregunta",
                    conexion,
                    transaccion);

                eliminarPregunta.Parameters.AddWithValue("@IdPregunta", idPregunta);

                int filas = eliminarPregunta.ExecuteNonQuery();
                transaccion.Commit();

                return filas > 0;
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }
    }
}
