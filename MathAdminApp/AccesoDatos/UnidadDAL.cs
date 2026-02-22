// ============================================================
// Capa de Acceso a Datos: UnidadDAL
// Operaciones CRUD para la tabla Unidades
// ============================================================

using System.Data.SqlClient;
using MathAdminApp.Modelos;

namespace MathAdminApp.AccesoDatos
{
    /// <summary>
    /// Clase de acceso a datos para la entidad Unidad.
    /// </summary>
    public class UnidadDAL
    {
        /// <summary>
        /// Obtiene todas las unidades ordenadas por numero.
        /// </summary>
        public List<Unidad> ObtenerTodas()
        {
            var lista = new List<Unidad>();
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "SELECT Id, Nombre, Descripcion, NumeroUnidad, Activa FROM Unidades ORDER BY NumeroUnidad";
            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();
            while (lector.Read())
            {
                lista.Add(new Unidad
                {
                    Id = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Descripcion = lector.GetString(2),
                    NumeroUnidad = lector.GetInt32(3),
                    Activa = lector.GetBoolean(4)
                });
            }
            return lista;
        }

        /// <summary>
        /// Agrega una nueva unidad.
        /// </summary>
        public bool Agregar(Unidad unidad)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"INSERT INTO Unidades (Nombre, Descripcion, NumeroUnidad, Activa) 
                             VALUES (@Nombre, @Descripcion, @NumeroUnidad, 1)";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@Nombre", unidad.Nombre);
            comando.Parameters.AddWithValue("@Descripcion", unidad.Descripcion);
            comando.Parameters.AddWithValue("@NumeroUnidad", unidad.NumeroUnidad);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Actualiza una unidad existente.
        /// </summary>
        public bool Actualizar(Unidad unidad)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"UPDATE Unidades 
                             SET Nombre = @Nombre, 
                                 Descripcion = @Descripcion, 
                                 NumeroUnidad = @NumeroUnidad 
                             WHERE Id = @Id";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@Nombre", unidad.Nombre);
            comando.Parameters.AddWithValue("@Descripcion", unidad.Descripcion);
            comando.Parameters.AddWithValue("@NumeroUnidad", unidad.NumeroUnidad);
            comando.Parameters.AddWithValue("@Id", unidad.Id);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Elimina una unidad de la base de datos.
        /// </summary>
        public bool Eliminar(int id)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "DELETE FROM Unidades WHERE Id = @Id";
            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@Id", id);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Obtiene el total de unidades activas.
        /// </summary>
        public int ContarUnidades()
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Unidades WHERE Activa = 1";
            using var comando = new SqlCommand(query, conexion);
            return (int)comando.ExecuteScalar();
        }
    }
}
