// ============================================================
// Capa de Acceso a Datos: PruebaDAL
// CRUD para tabla Prueba
// ============================================================

using MathAdminApp.Modelos;
using System.Data.SqlClient;

namespace MathAdminApp.AccesoDatos
{
    public class PruebaDAO
    {
        // =====================================================
        // OBTENER TODAS O FILTRAR POR TEMA
        // =====================================================
        public List<Prueba> ObtenerPorTema(int? idTema = null)
        {
            var lista = new List<Prueba>();

            using var conexion =
                ConexionBD.ObtenerConexion();

            conexion.Open();

            string query = @"
                SELECT
                    idPrueba,
                    titulo,
                    idTema,
                    creadoPor
                FROM Pruebas";

            if (idTema.HasValue)
                query += " WHERE idTema = @IdTema";

            query += " ORDER BY titulo";

            using var comando =
                new SqlCommand(query, conexion);

            if (idTema.HasValue)
            {
                comando.Parameters.AddWithValue(
                    "@IdTema",
                    idTema.Value
                );
            }

            using var lector =
                comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Prueba
                {
                    IdPrueba = lector.GetInt32(0),
                    Titulo = lector.GetString(1),
                    IdTema = lector.GetInt32(2),
                    CreadoPor = lector.GetInt32(3)
                });
            }

            return lista;
        }

        // =====================================================
        // AGREGAR
        // =====================================================
        public bool Agregar(Prueba prueba)
        {
            using var conexion =
                ConexionBD.ObtenerConexion();

            conexion.Open();

            string query = @"
                INSERT INTO Pruebas
                (
                    titulo,
                    idTema,
                    creadoPor
                )
                VALUES
                (
                    @Titulo,
                    @IdTema,
                    @CreadoPor
                )";

            using var comando =
                new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue(
                "@Titulo",
                prueba.Titulo
            );

            comando.Parameters.AddWithValue(
                "@IdTema",
                prueba.IdTema
            );

            comando.Parameters.AddWithValue(
                "@CreadoPor",
                prueba.CreadoPor
            );

            return comando.ExecuteNonQuery() > 0;
        }

        // =====================================================
        // ELIMINAR
        // =====================================================
        public bool Eliminar(int idPrueba)
        {
            using var conexion =
                ConexionBD.ObtenerConexion();

            conexion.Open();

            string query =
                "DELETE FROM Pruebas WHERE idPrueba = @IdPrueba";

            using var comando =
                new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue(
                "@IdPrueba",
                idPrueba
            );

            return comando.ExecuteNonQuery() > 0;
        }

        // =====================================================
        // CONTAR
        // =====================================================
        public int ContarPruebas()
        {
            using var conexion =
                ConexionBD.ObtenerConexion();

            conexion.Open();

            string query =
                "SELECT COUNT(*) FROM Pruebas";

            using var comando =
                new SqlCommand(query, conexion);

            return (int)comando.ExecuteScalar();
        }
    }
}
