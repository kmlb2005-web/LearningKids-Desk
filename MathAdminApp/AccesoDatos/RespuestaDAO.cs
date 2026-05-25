using MathAdminApp.Modelos;
using System.Data.SqlClient;

namespace MathAdminApp.AccesoDatos
{
    public class RespuestaDAO
    {
        public List<Respuesta> ObtenerPorPregunta(int idPregunta)
        {
            List<Respuesta> lista = new();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idRespuesta,
                    texto,
                    esCorrecta,
                    idPregunta
                FROM Respuestas
                WHERE idPregunta = @IdPregunta
                ORDER BY idRespuesta";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@IdPregunta", idPregunta);

            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Respuesta
                {
                    IdRespuesta = lector.GetInt32(0),
                    Texto = lector.GetString(1),
                    EsCorrecta = lector.GetBoolean(2),
                    IdPregunta = lector.GetInt32(3)
                });
            }

            return lista;
        }

        public bool ReemplazarPorPregunta(int idPregunta, List<Respuesta> respuestas)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            using var transaccion = conexion.BeginTransaction();

            try
            {
                using (var eliminar = new SqlCommand(
                    "DELETE FROM Respuestas WHERE idPregunta = @IdPregunta",
                    conexion,
                    transaccion))
                {
                    eliminar.Parameters.AddWithValue("@IdPregunta", idPregunta);
                    eliminar.ExecuteNonQuery();
                }

                foreach (var respuesta in respuestas)
                {
                    using var insertar = new SqlCommand(@"
                        INSERT INTO Respuestas
                        (
                            texto,
                            esCorrecta,
                            idPregunta
                        )
                        VALUES
                        (
                            @Texto,
                            @EsCorrecta,
                            @IdPregunta
                        )",
                        conexion,
                        transaccion);

                    insertar.Parameters.AddWithValue("@Texto", respuesta.Texto);
                    insertar.Parameters.AddWithValue("@EsCorrecta", respuesta.EsCorrecta);
                    insertar.Parameters.AddWithValue("@IdPregunta", idPregunta);
                    insertar.ExecuteNonQuery();
                }

                transaccion.Commit();
                return true;
            }
            catch
            {
                transaccion.Rollback();
                throw;
            }
        }
    }
}
