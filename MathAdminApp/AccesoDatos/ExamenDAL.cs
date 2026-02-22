// ============================================================
// Capa de Acceso a Datos: ExamenDAL
// Operaciones CRUD para la tabla Examenes
// ============================================================

using System.Data.SqlClient;
using MathAdminApp.Modelos;

namespace MathAdminApp.AccesoDatos
{
    /// <summary>
    /// Clase de acceso a datos para la entidad Examen.
    /// </summary>
    public class ExamenDAL
    {
        /// <summary>
        /// Obtiene todos los examenes, opcionalmente filtrados por unidad.
        /// </summary>
        public List<Examen> ObtenerPorUnidad(int? unidadId = null)
        {
            var lista = new List<Examen>();
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"SELECT e.Id, e.Nombre, e.UnidadId, u.Nombre AS NombreUnidad, e.FechaCreacion, e.Activo 
                             FROM Examenes e 
                             INNER JOIN Unidades u ON e.UnidadId = u.Id";

            if (unidadId.HasValue)
                query += " WHERE e.UnidadId = @UnidadId";

            query += " ORDER BY u.NumeroUnidad, e.Nombre";

            using var comando = new SqlCommand(query, conexion);
            if (unidadId.HasValue)
                comando.Parameters.AddWithValue("@UnidadId", unidadId.Value);

            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(new Examen
                {
                    Id = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    UnidadId = lector.GetInt32(2),
                    NombreUnidad = lector.GetString(3),
                    FechaCreacion = lector.GetDateTime(4),
                    Activo = lector.GetBoolean(5)
                });
            }
            return lista;
        }

        /// <summary>
        /// Agrega un nuevo examen.
        /// </summary>
        public bool Agregar(Examen examen)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"INSERT INTO Examenes (Nombre, UnidadId, FechaCreacion, Activo) 
                             VALUES (@Nombre, @UnidadId, GETDATE(), 1)";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@Nombre", examen.Nombre);
            comando.Parameters.AddWithValue("@UnidadId", examen.UnidadId);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Elimina un examen de la base de datos.
        /// </summary>
        public bool Eliminar(int id)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "DELETE FROM Examenes WHERE Id = @Id";
            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@Id", id);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Cuenta el total de examenes.
        /// </summary>
        public int ContarExamenes()
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Examenes WHERE Activo = 1";
            using var comando = new SqlCommand(query, conexion);
            return (int)comando.ExecuteScalar();
        }
    }
}
