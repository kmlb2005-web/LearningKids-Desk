// ============================================================
// Capa de Acceso a Datos: ProyectoDAL
// CRUD para la tabla Proyectos
// ============================================================

using MathAdminApp.Modelos;
using System.Data.SqlClient;

namespace MathAdminApp.AccesoDatos
{
    public class ProyectoDAO
    {
        /// <summary>
        /// Obtener todos los proyectos
        /// </summary>
        public List<Proyecto> ObtenerTodos()
        {
            var lista = new List<Proyecto>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT 
                    idProyecto,
                    nombre,
                    descripcion,
                    grado,
                    idCampo,
                    creadoPor
                FROM Proyectos
                ORDER BY nombre";

            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Proyecto
                {
                    idProyecto = lector.GetInt32(0),
                    nombre = lector.GetString(1),
                    descripcion = lector.GetString(2),
                    grado = lector.GetInt32(3),
                    idCampo = lector.GetInt32(4),
                    creadoPor = lector.GetInt32(5)
                });
            }

            return lista;
        }

        /// <summary>
        /// Obtener por ID
        /// </summary>
        public Proyecto? ObtenerPorId(int idProyecto)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT 
                    idProyecto,
                    nombre,
                    descripcion,
                    grado,
                    idCampo,
                    creadoPor
                FROM Proyectos
                WHERE idProyecto = @IdProyecto";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@IdProyecto", idProyecto);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new Proyecto
                {
                    idProyecto = lector.GetInt32(0),
                    nombre = lector.GetString(1),
                    descripcion = lector.GetString(2),
                    grado = lector.GetInt32(3),
                    idCampo = lector.GetInt32(4),
                    creadoPor = lector.GetInt32(5)
                };
            }

            return null;
        }

        /// <summary>
        /// Agregar proyecto
        /// </summary>
        public bool Agregar(Proyecto proyecto)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                INSERT INTO Proyectos
                (
                    nombre,
                    descripcion,
                    grado,
                    idCampo,
                    creadoPor
                )
                VALUES
                (
                    @Nombre,
                    @Descripcion,
                    @Grado,
                    @IdCampo,
                    @CreadoPor
                )";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Nombre", proyecto.nombre);
            comando.Parameters.AddWithValue("@Descripcion", proyecto.descripcion);
            comando.Parameters.AddWithValue("@Grado", proyecto.grado);
            comando.Parameters.AddWithValue("@IdCampo", proyecto.idCampo);
            comando.Parameters.AddWithValue("@CreadoPor", proyecto.creadoPor);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Actualizar proyecto
        /// </summary>
        public bool Actualizar(Proyecto proyecto)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                UPDATE Proyectos
                SET 
                    nombre = @Nombre,
                    descripcion = @Descripcion,
                    grado = @Grado,
                    idCampo = @IdCampo,
                    creadoPor = @CreadoPor
                WHERE idProyecto = @IdProyecto";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Nombre", proyecto.nombre);
            comando.Parameters.AddWithValue("@Descripcion", proyecto.descripcion);
            comando.Parameters.AddWithValue("@Grado", proyecto.grado);
            comando.Parameters.AddWithValue("@IdCampo", proyecto.idCampo);
            comando.Parameters.AddWithValue("@CreadoPor", proyecto.creadoPor);
            comando.Parameters.AddWithValue("@IdProyecto", proyecto.idProyecto);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Eliminar proyecto
        /// </summary>
        public bool Eliminar(int idProyecto)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                DELETE FROM Proyectos
                WHERE idProyecto = @IdProyecto";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@IdProyecto", idProyecto);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Contar proyectos
        /// </summary>
        public int Contar()
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Proyectos";

            using var comando = new SqlCommand(query, conexion);

            return (int)comando.ExecuteScalar();
        }
    }
}
