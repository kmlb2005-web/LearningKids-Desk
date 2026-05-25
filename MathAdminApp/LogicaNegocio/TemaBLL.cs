using System.Data.SqlClient;
using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    public class TemaBLL
    {
        // =====================================================
        // OBTENER TODOS
        // =====================================================
        public List<Tema> ObtenerTodos()
        {
            List<Tema> lista = new();

            using SqlConnection conexion =
                ConexionBD.ObtenerConexion();

            string query = @"
                SELECT 
                    t.idTema,
                    t.nombre,
                    t.descripcion,
                    t.idProyecto,
                    p.nombre AS nombreProyecto
                FROM Temas t
                INNER JOIN Proyectos p 
                    ON p.idProyecto = t.idProyecto";

            SqlCommand cmd =
                new SqlCommand(query, conexion);

            conexion.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Tema
                {
                    idTema =
                        Convert.ToInt32(reader["idTema"]),

                    nombre =
                        reader["nombre"].ToString()!,

                    descripcion =
                        reader["descripcion"].ToString()!,

                    idProyecto =
                        Convert.ToInt32(reader["idProyecto"]),

                    nombreProyecto =
                        reader["nombreProyecto"].ToString()!
                });
            }

            return lista;
        }

        // =====================================================
        // OBTENER POR PROYECTO
        // =====================================================
        public List<Tema> ObtenerPorProyecto(int idProyecto)
        {
            List<Tema> lista = new();

            using SqlConnection conexion =
                ConexionBD.ObtenerConexion();

            string query = @"
                SELECT 
                    t.idTema,
                    t.nombre,
                    t.descripcion,
                    t.idProyecto,
                    p.nombre AS nombreProyecto
                FROM Temas t
                INNER JOIN Proyectos p 
                    ON p.idProyecto = t.idProyecto
                WHERE t.idProyecto = @idProyecto";

            SqlCommand cmd =
                new SqlCommand(query, conexion);

            cmd.Parameters.AddWithValue(
                "@idProyecto", idProyecto);

            conexion.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                lista.Add(new Tema
                {
                    idTema =
                        Convert.ToInt32(reader["idTema"]),

                    nombre =
                        reader["nombre"].ToString()!,

                    descripcion =
                        reader["descripcion"].ToString()!,

                    idProyecto =
                        Convert.ToInt32(reader["idProyecto"]),

                    nombreProyecto =
                        reader["nombreProyecto"].ToString()!
                });
            }

            return lista;
        }

        // =====================================================
        // INSERTAR
        // =====================================================
        public void Insertar(Tema tema)
        {
            using SqlConnection conexion =
                ConexionBD.ObtenerConexion();

            string query = @"
                INSERT INTO Temas 
                    (nombre, descripcion, idProyecto)
                VALUES 
                    (@nombre, @descripcion, @idProyecto)";

            SqlCommand cmd =
                new SqlCommand(query, conexion);

            cmd.Parameters.AddWithValue(
                "@nombre", tema.nombre);

            cmd.Parameters.AddWithValue(
                "@descripcion", tema.descripcion);

            cmd.Parameters.AddWithValue(
                "@idProyecto", tema.idProyecto);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        // =====================================================
        // ACTUALIZAR
        // =====================================================
        public void Actualizar(Tema tema)
        {
            using SqlConnection conexion =
                ConexionBD.ObtenerConexion();

            string query = @"
                UPDATE Temas SET
                    nombre      = @nombre,
                    descripcion = @descripcion,
                    idProyecto  = @idProyecto
                WHERE idTema = @idTema";

            SqlCommand cmd =
                new SqlCommand(query, conexion);

            cmd.Parameters.AddWithValue(
                "@nombre", tema.nombre);

            cmd.Parameters.AddWithValue(
                "@descripcion", tema.descripcion);

            cmd.Parameters.AddWithValue(
                "@idProyecto", tema.idProyecto);

            cmd.Parameters.AddWithValue(
                "@idTema", tema.idTema);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }

        // =====================================================
        // ELIMINAR
        // =====================================================
        public void Eliminar(int idTema)
        {
            using SqlConnection conexion =
                ConexionBD.ObtenerConexion();

            string query = @"
                DELETE FROM Temas 
                WHERE idTema = @id";

            SqlCommand cmd =
                new SqlCommand(query, conexion);

            cmd.Parameters.AddWithValue("@id", idTema);

            conexion.Open();
            cmd.ExecuteNonQuery();
        }
    }
}