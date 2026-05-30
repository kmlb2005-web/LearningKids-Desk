// ============================================================
// Capa de Acceso a Datos: Conexion a SQL Server
// Clase encargada de gestionar la conexion a la base de datos
// ============================================================

using System.Data.SqlClient;

namespace MathAdminApp.AccesoDatos
{
    /// <summary>
    /// Clase estatica para gestionar la cadena de conexion a SQL Server.
    /// Modifica la cadena de conexion segun tu configuracion local.
    /// </summary>
    public static class ConexionBD
    {
        // =====================================================
        // IMPORTANTE: Modifica esta cadena de conexion segun
        // tu configuracion de SQL Server local.
        // =====================================================
        private static readonly string _cadenaConexion =
            @"Data Source=KMLB; Initial Catalog=LearningKidsDB; Integrated Security=True; TrustServerCertificate=True;";

        /// <summary>
        /// Obtiene la cadena de conexion configurada.
        /// </summary>
        public static string CadenaConexion => _cadenaConexion;

        /// <summary>
        /// Crea y retorna una nueva conexion a SQL Server.
        /// </summary>
        public static SqlConnection ObtenerConexion()
        {
            return new SqlConnection(_cadenaConexion);
        }

        /// <summary>
        /// Prueba la conexion a la base de datos.
        /// Retorna true si la conexion es exitosa.
        /// </summary>
        public static bool ProbarConexion()
        {
            try
            {
                using var conexion = ObtenerConexion();
                conexion.Open();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static void AsegurarColumnasSoftDelete()
        {
            string[] tablas =
            [
                "Usuarios",
                "Alumnos",
                "Tutores",
                "Roles",
                "CamposFormativos",
                "Proyectos",
                "Temas",
                "Pruebas",
                "Preguntas",
                "Respuestas",
                "Resultados",
                "DocenteAlumno"
            ];

            using var conexion = ObtenerConexion();
            conexion.Open();

            foreach (string tabla in tablas)
            {
                string constraint = $"DF_{tabla}_activo";
                string query = $@"
                    IF OBJECT_ID(N'dbo.{tabla}', N'U') IS NOT NULL
                       AND COL_LENGTH(N'dbo.{tabla}', N'activo') IS NULL
                    BEGIN
                        ALTER TABLE dbo.{tabla}
                        ADD activo bit NOT NULL
                        CONSTRAINT {constraint} DEFAULT (1)
                    END";

                using var comando = new SqlCommand(query, conexion);
                comando.ExecuteNonQuery();
            }
        }
    }
}
