// ============================================================
// Capa de Acceso a Datos: RolDAO
// Adaptado al modelo Rol
// ============================================================
using MathAdminApp.Modelos;
using System.Data.SqlClient;

namespace MathAdminApp.AccesoDatos
{
    internal class RolDAO
    {
        /// <summary>
        /// Obtener todos los roles
        /// </summary>
        public List<Rol> ObtenerRoles()
        {
            List<Rol> lista = new List<Rol>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idRol,
                    nombre
                FROM Roles
                ORDER BY nombre";

            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Rol
                {
                    IdRol = lector.GetInt32(0),
                    Nombre = lector.GetString(1)
                });
            }

            return lista;
        }

        /// <summary>
        /// Obtener rol por ID
        /// </summary>
        public Rol? ObtenerPorId(int idRol)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idRol,
                    nombre
                FROM Roles
                WHERE idRol = @IdRol";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdRol", idRol);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new Rol
                {
                    IdRol = lector.GetInt32(0),
                    Nombre = lector.GetString(1)
                };
            }

            return null;
        }

        /// <summary>
        /// Obtener rol por nombre
        /// </summary>
        public Rol? ObtenerPorNombre(string nombre)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idRol,
                    nombre
                FROM Roles
                WHERE UPPER(nombre) = UPPER(@Nombre)";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Nombre", nombre);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new Rol
                {
                    IdRol = lector.GetInt32(0),
                    Nombre = lector.GetString(1)
                };
            }

            return null;
        }

        /// <summary>
        /// Agregar rol
        /// </summary>
        public bool Agregar(Rol rol)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                INSERT INTO Roles
                (
                    nombre
                )
                VALUES
                (
                    @Nombre
                )";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Nombre", rol.Nombre);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Actualizar rol
        /// </summary>
        public bool Actualizar(Rol rol)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                UPDATE Roles
                SET
                    nombre = @Nombre
                WHERE idRol = @IdRol";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdRol", rol.IdRol);
            comando.Parameters.AddWithValue("@Nombre", rol.Nombre);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Eliminar rol
        /// </summary>
        public bool Eliminar(int idRol)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                DELETE FROM Roles
                WHERE idRol = @IdRol";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdRol", idRol);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Contar roles
        /// </summary>
        public int ContarRoles()
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Roles";

            using var comando = new SqlCommand(query, conexion);

            return (int)comando.ExecuteScalar();
        }
    }
}