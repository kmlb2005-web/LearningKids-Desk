// ============================================================
// Capa de Acceso a Datos: PreguntaDAL
// Operaciones CRUD para la tabla Preguntas
// ============================================================

using System.Data.SqlClient;
using MathAdminApp.Modelos;

namespace MathAdminApp.AccesoDatos
{
    /// <summary>
    /// Clase de acceso a datos para la entidad Pregunta.
    /// </summary>
    public class PreguntaDAL
    {
        /// <summary>
        /// Obtiene las preguntas de un examen especifico.
        /// </summary>
        public List<Pregunta> ObtenerPorExamen(int examenId)
        {
            var lista = new List<Pregunta>();
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"SELECT Id, ExamenId, Texto, Tipo, OpcionA, OpcionB, OpcionC, OpcionD, RespuestaCorrecta 
                             FROM Preguntas 
                             WHERE ExamenId = @ExamenId 
                             ORDER BY Id";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@ExamenId", examenId);

            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(new Pregunta
                {
                    Id = lector.GetInt32(0),
                    ExamenId = lector.GetInt32(1),
                    Texto = lector.GetString(2),
                    Tipo = lector.GetString(3),
                    OpcionA = lector.IsDBNull(4) ? "" : lector.GetString(4),
                    OpcionB = lector.IsDBNull(5) ? "" : lector.GetString(5),
                    OpcionC = lector.IsDBNull(6) ? "" : lector.GetString(6),
                    OpcionD = lector.IsDBNull(7) ? "" : lector.GetString(7),
                    RespuestaCorrecta = lector.GetString(8)
                });
            }
            return lista;
        }

        /// <summary>
        /// Agrega una nueva pregunta a un examen.
        /// </summary>
        public bool Agregar(Pregunta pregunta)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"INSERT INTO Preguntas (ExamenId, Texto, Tipo, OpcionA, OpcionB, OpcionC, OpcionD, RespuestaCorrecta) 
                             VALUES (@ExamenId, @Texto, @Tipo, @OpcionA, @OpcionB, @OpcionC, @OpcionD, @RespuestaCorrecta)";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@ExamenId", pregunta.ExamenId);
            comando.Parameters.AddWithValue("@Texto", pregunta.Texto);
            comando.Parameters.AddWithValue("@Tipo", pregunta.Tipo);
            comando.Parameters.AddWithValue("@OpcionA", pregunta.OpcionA ?? "");
            comando.Parameters.AddWithValue("@OpcionB", pregunta.OpcionB ?? "");
            comando.Parameters.AddWithValue("@OpcionC", pregunta.OpcionC ?? "");
            comando.Parameters.AddWithValue("@OpcionD", pregunta.OpcionD ?? "");
            comando.Parameters.AddWithValue("@RespuestaCorrecta", pregunta.RespuestaCorrecta);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Elimina una pregunta.
        /// </summary>
        public bool Eliminar(int id)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "DELETE FROM Preguntas WHERE Id = @Id";
            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@Id", id);

            return comando.ExecuteNonQuery() > 0;
        }
    }
}
