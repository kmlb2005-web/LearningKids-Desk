// ============================================================
// Capa de Acceso a Datos: ResultadoDAL
// Operaciones de lectura para la tabla ResultadosExamen
// ============================================================

using System.Data.SqlClient;
using MathAdminApp.Modelos;

namespace MathAdminApp.AccesoDatos
{
    /// <summary>
    /// Clase de acceso a datos para la entidad ResultadoExamen.
    /// Solo lectura, ya que el administrador no modifica resultados.
    /// </summary>
    public class ResultadoDAL
    {
        /// <summary>
        /// Obtiene los resultados de un alumno especifico.
        /// </summary>
        public List<ResultadoExamen> ObtenerPorAlumno(int usuarioId)
        {
            var lista = new List<ResultadoExamen>();
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"SELECT r.Id, r.UsuarioId, u.Nombre AS NombreAlumno, 
                                    r.ExamenId, e.Nombre AS NombreExamen, 
                                    un.Nombre AS NombreUnidad,
                                    r.Calificacion, r.FechaPresentacion
                             FROM ResultadosExamen r
                             INNER JOIN Usuarios u ON r.UsuarioId = u.Id
                             INNER JOIN Examenes e ON r.ExamenId = e.Id
                             INNER JOIN Unidades un ON e.UnidadId = un.Id
                             WHERE r.UsuarioId = @UsuarioId
                             ORDER BY r.FechaPresentacion DESC";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@UsuarioId", usuarioId);

            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(new ResultadoExamen
                {
                    Id = lector.GetInt32(0),
                    UsuarioId = lector.GetInt32(1),
                    NombreAlumno = lector.GetString(2),
                    ExamenId = lector.GetInt32(3),
                    NombreExamen = lector.GetString(4),
                    NombreUnidad = lector.GetString(5),
                    Calificacion = lector.GetDecimal(6),
                    FechaPresentacion = lector.GetDateTime(7)
                });
            }
            return lista;
        }

        /// <summary>
        /// Obtiene todos los resultados de todos los alumnos.
        /// </summary>
        public List<ResultadoExamen> ObtenerTodos()
        {
            var lista = new List<ResultadoExamen>();
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"SELECT r.Id, r.UsuarioId, u.Nombre AS NombreAlumno, 
                                    r.ExamenId, e.Nombre AS NombreExamen, 
                                    un.Nombre AS NombreUnidad,
                                    r.Calificacion, r.FechaPresentacion
                             FROM ResultadosExamen r
                             INNER JOIN Usuarios u ON r.UsuarioId = u.Id
                             INNER JOIN Examenes e ON r.ExamenId = e.Id
                             INNER JOIN Unidades un ON e.UnidadId = un.Id
                             ORDER BY u.Nombre, r.FechaPresentacion DESC";

            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(new ResultadoExamen
                {
                    Id = lector.GetInt32(0),
                    UsuarioId = lector.GetInt32(1),
                    NombreAlumno = lector.GetString(2),
                    ExamenId = lector.GetInt32(3),
                    NombreExamen = lector.GetString(4),
                    NombreUnidad = lector.GetString(5),
                    Calificacion = lector.GetDecimal(6),
                    FechaPresentacion = lector.GetDateTime(7)
                });
            }
            return lista;
        }
    }
}
