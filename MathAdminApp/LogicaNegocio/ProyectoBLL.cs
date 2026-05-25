using System.Data.SqlClient;
using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    public class ProyectoBLL
    {
        // =====================================================
        // OBTENER TODOS
        // =====================================================
        public List<Proyecto> ObtenerTodos()
        {
            List<Proyecto> lista = new();

            using SqlConnection conexion =
                ConexionBD.ObtenerConexion();

            string query = @"
                SELECT 
                    idProyecto,
                    nombre,
                    descripcion,
                    unidad,
                    fechaCreacion
                FROM Proyectos";

            SqlCommand cmd =
                new SqlCommand(query, conexion);

            conexion.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Proyecto
                {
                    idProyecto =
                        Convert.ToInt32(reader["idProyecto"]),

                    nombre =
                        reader["nombre"].ToString()!,

                    descripcion =
                        reader["descripcion"].ToString()!,

                    unidad =
                        Convert.ToInt32(reader["unidad"]),

                    fechaCreacion =
                        Convert.ToDateTime(reader["fechaCreacion"])
                });
            }

            return lista;
        }

        // =====================================================
        // INSERTAR
        // =====================================================
        public void Insertar(Proyecto proyecto)
        {
            using SqlConnection conexion =
                ConexionBD.ObtenerConexion();

            string query = @"
                INSERT INTO Proyectos 
                    (nombre, descripcion, unidad)
                VALUES 
                    (@nombre, @descripcion, @unidad)";

            SqlCommand cmd =
                new SqlCommand(query, conexion);

            cmd.Parameters.AddWithValue(
                "@nombre", proyecto.nombre);

            cmd.Parameters.AddWithValue(
                "@descripcion", proyecto.descripcion);

            cmd.Parameters.AddWithValue(
                "@unidad", proyecto.unidad);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        // =====================================================
        // ACTUALIZAR
        // =====================================================

        public void Actualizar(Proyecto proyecto)
        {
            using SqlConnection conexion =
                ConexionBD.ObtenerConexion();

            string query = @"
        UPDATE Proyectos
        SET
            nombre = @nombre,
            descripcion = @descripcion,
            unidad = @unidad
        WHERE idProyecto = @id";

            SqlCommand cmd =
                new SqlCommand(query, conexion);

            cmd.Parameters.AddWithValue(
                "@id",
                proyecto.idProyecto
            );

            cmd.Parameters.AddWithValue(
                "@nombre",
                proyecto.nombre
            );

            cmd.Parameters.AddWithValue(
                "@descripcion",
                proyecto.descripcion
            );

            cmd.Parameters.AddWithValue(
                "@unidad",
                proyecto.unidad
            );

            conexion.Open();

            cmd.ExecuteNonQuery();
        }

        // =====================================================
        // ELIMINAR
        // =====================================================
        public void Eliminar(int idProyecto)
        {
            using SqlConnection conexion =
                ConexionBD.ObtenerConexion();

            string query = @"
                DELETE FROM Proyectos 
                WHERE idProyecto = @id";

            SqlCommand cmd =
                new SqlCommand(query, conexion);

            cmd.Parameters.AddWithValue(
                "@id", idProyecto);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        // =====================================================
        // OBTENER CAMPOS (ya no se usa, pero por si acaso)
        // =====================================================
        public List<(int id, string nombre)> ObtenerCampos()
        {
            List<(int, string)> lista = new();

            using SqlConnection conexion =
                ConexionBD.ObtenerConexion();

            string query =
                @"SELECT idCampo, nombre FROM CamposFormativos";

            SqlCommand cmd =
                new SqlCommand(query, conexion);

            conexion.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add((
                    Convert.ToInt32(reader["idCampo"]),
                    reader["nombre"].ToString()!
                ));
            }

            return lista;
        }
    }
}