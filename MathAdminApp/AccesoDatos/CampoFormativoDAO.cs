// ============================================================
// Capa de Acceso a Datos: CampoFormativoDAL
// CRUD para la tabla CamposFormativos
// ============================================================

using System.Data.SqlClient;
using MathAdminApp.Modelos;

namespace MathAdminApp.AccesoDatos
{
    public class CampoFormativoDAO
    {
        /// <summary>
        /// Obtener todos los campos formativos
        /// </summary>
        public List<CampoFormativo> ObtenerTodos()
        {
            List<CampoFormativo> lista = new List<CampoFormativo>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT 
                    idCampo,
                    nombre
                FROM CamposFormativos
                WHERE ISNULL(activo, 1) = 1
                ORDER BY nombre";

            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new CampoFormativo
                {
                    IdCampo = lector.GetInt32(0),
                    Nombre = lector.GetString(1)
                });
            }

            return lista;
        }

        /// <summary>
        /// Obtener un campo formativo por ID
        /// </summary>
        public CampoFormativo? ObtenerPorId(int idCampo)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT 
                    idCampo,
                    nombre
                FROM CamposFormativos
                WHERE idCampo = @IdCampo
                  AND ISNULL(activo, 1) = 1";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdCampo", idCampo);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new CampoFormativo
                {
                    IdCampo = lector.GetInt32(0),
                    Nombre = lector.GetString(1)
                };
            }

            return null;
        }

        /// <summary>
        /// Agregar nuevo campo formativo
        /// </summary>
        public bool Agregar(CampoFormativo campo)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                INSERT INTO CamposFormativos
                (
                    nombre,
                    activo
                )
                VALUES
                (
                    @Nombre,
                    1
                )";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Nombre", campo.Nombre);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Actualizar campo formativo
        /// </summary>
        public bool Actualizar(CampoFormativo campo)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                UPDATE CamposFormativos
                SET
                    nombre = @Nombre
                WHERE idCampo = @IdCampo";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Nombre", campo.Nombre);
            comando.Parameters.AddWithValue("@IdCampo", campo.IdCampo);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Eliminar campo formativo
        /// </summary>
        public bool Eliminar(int idCampo)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                UPDATE CamposFormativos
                SET activo = 0
                WHERE idCampo = @IdCampo";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdCampo", idCampo);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Contar campos formativos
        /// </summary>
        public int Contar()
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "SELECT COUNT(*) FROM CamposFormativos WHERE ISNULL(activo, 1) = 1";

            using var comando = new SqlCommand(query, conexion);

            return (int)comando.ExecuteScalar();
        }
    }
}
