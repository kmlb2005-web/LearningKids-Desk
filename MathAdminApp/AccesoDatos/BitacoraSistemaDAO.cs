using MathAdminApp.Modelos;
using System.Data.SqlClient;

namespace MathAdminApp.AccesoDatos
{
    internal class BitacoraSistemaDAO
    {
        public void Registrar(BitacoraSistema entrada)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            AsegurarTabla(conexion);

            string query = @"
                INSERT INTO BitacoraSistema
                (
                    fecha,
                    idUsuario,
                    usuario,
                    modulo,
                    accion,
                    detalle
                )
                VALUES
                (
                    @Fecha,
                    @IdUsuario,
                    @Usuario,
                    @Modulo,
                    @Accion,
                    @Detalle
                )";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@Fecha", entrada.Fecha);
            comando.Parameters.AddWithValue("@IdUsuario", entrada.IdUsuario);
            comando.Parameters.AddWithValue("@Usuario", entrada.Usuario);
            comando.Parameters.AddWithValue("@Modulo", entrada.Modulo);
            comando.Parameters.AddWithValue("@Accion", entrada.Accion);
            comando.Parameters.AddWithValue("@Detalle", entrada.Detalle);

            comando.ExecuteNonQuery();
        }

        public List<BitacoraSistema> ObtenerPorRango(DateTime desde, DateTime hasta)
        {
            List<BitacoraSistema> lista = new();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            AsegurarTabla(conexion);

            string query = @"
                SELECT
                    idBitacora,
                    fecha,
                    ISNULL(idUsuario, 0),
                    ISNULL(usuario, ''),
                    ISNULL(modulo, ''),
                    ISNULL(accion, ''),
                    ISNULL(detalle, '')
                FROM BitacoraSistema
                WHERE fecha >= @Desde
                  AND fecha < @Hasta
                ORDER BY fecha DESC";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@Desde", desde);
            comando.Parameters.AddWithValue("@Hasta", hasta);

            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new BitacoraSistema
                {
                    IdBitacora = lector.GetInt32(0),
                    Fecha = lector.GetDateTime(1),
                    IdUsuario = lector.GetInt32(2),
                    Usuario = lector.GetString(3),
                    Modulo = lector.GetString(4),
                    Accion = lector.GetString(5),
                    Detalle = lector.GetString(6)
                });
            }

            return lista;
        }

        private static void AsegurarTabla(SqlConnection conexion)
        {
            string query = @"
                IF OBJECT_ID('dbo.BitacoraSistema', 'U') IS NULL
                BEGIN
                    CREATE TABLE dbo.BitacoraSistema
                    (
                        idBitacora INT IDENTITY(1,1) PRIMARY KEY,
                        fecha DATETIME NOT NULL,
                        idUsuario INT NULL,
                        usuario NVARCHAR(150) NULL,
                        modulo NVARCHAR(80) NOT NULL,
                        accion NVARCHAR(50) NOT NULL,
                        detalle NVARCHAR(600) NULL
                    );
                END";

            using var comando = new SqlCommand(query, conexion);
            comando.ExecuteNonQuery();
        }
    }
}
