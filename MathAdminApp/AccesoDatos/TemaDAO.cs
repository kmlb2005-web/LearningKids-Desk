// ============================================================
// Capa de Acceso a Datos: TemaDAO
// Adaptado al modelo Tema
// ============================================================
using MathAdminApp.Modelos;
using System.Data.SqlClient;

namespace MathAdminApp.AccesoDatos
{
    internal class TemaDAO
    {
        /// <summary>
        /// Obtener todos los temas
        /// </summary>
        public List<Tema> ObtenerTemas()
        {
            List<Tema> lista = new List<Tema>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idTema,
                    ISNULL(nombre, ''),
                    ISNULL(descripcion, ''),
                    ISNULL(idProyecto, 0)
                FROM Temas
                WHERE ISNULL(activo, 1) = 1
                ORDER BY nombre";

            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Tema
                {
                    IdTema = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Descripcion = lector.GetString(2),
                    IdProyecto = lector.GetInt32(3)
                });
            }

            return lista;
        }

        public List<Tema> ObtenerPorDocente(int idDocente)
        {
            List<Tema> lista = new List<Tema>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    t.idTema,
                    ISNULL(t.nombre, ''),
                    ISNULL(t.descripcion, ''),
                    ISNULL(t.idProyecto, 0)
                FROM Temas t
                INNER JOIN Proyectos p
                    ON p.idProyecto = t.idProyecto
                WHERE p.creadoPor = @IdDocente
                  AND ISNULL(t.activo, 1) = 1
                  AND ISNULL(p.activo, 1) = 1
                ORDER BY t.nombre";

            using var comando = new SqlCommand(query, conexion);
            comando.Parameters.AddWithValue("@IdDocente", idDocente);

            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Tema
                {
                    IdTema = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Descripcion = lector.GetString(2),
                    IdProyecto = lector.GetInt32(3)
                });
            }

            return lista;
        }

        /// <summary>
        /// Obtener tema por ID
        /// </summary>
        public Tema? ObtenerPorId(int idTema)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idTema,
                    ISNULL(nombre, ''),
                    ISNULL(descripcion, ''),
                    ISNULL(idProyecto, 0)
                FROM Temas
                WHERE idTema = @IdTema
                  AND ISNULL(activo, 1) = 1";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdTema", idTema);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new Tema
                {
                    IdTema = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Descripcion = lector.GetString(2),
                    IdProyecto = lector.GetInt32(3)
                };
            }

            return null;
        }

        /// <summary>
        /// Obtener temas por proyecto
        /// </summary>
        public List<Tema> ObtenerPorProyecto(int idProyecto)
        {
            List<Tema> lista = new List<Tema>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    idTema,
                    ISNULL(nombre, ''),
                    ISNULL(descripcion, ''),
                    ISNULL(idProyecto, 0)
                FROM Temas
                WHERE idProyecto = @IdProyecto
                  AND ISNULL(activo, 1) = 1
                ORDER BY nombre";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdProyecto", idProyecto);

            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new Tema
                {
                    IdTema = lector.GetInt32(0),
                    Nombre = lector.GetString(1),
                    Descripcion = lector.GetString(2),
                    IdProyecto = lector.GetInt32(3)
                });
            }

            return lista;
        }

        /// <summary>
        /// Agregar tema
        /// </summary>
        public bool Agregar(Tema tema)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                INSERT INTO Temas
                (
                    nombre,
                    descripcion,
                    idProyecto,
                    activo
                )
                VALUES
                (
                    @Nombre,
                    @Descripcion,
                    @IdProyecto,
                    1
                )";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Nombre", tema.Nombre);
            comando.Parameters.AddWithValue("@Descripcion", tema.Descripcion);
            comando.Parameters.AddWithValue("@IdProyecto", tema.IdProyecto);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Actualizar tema
        /// </summary>
        public bool Actualizar(Tema tema)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                UPDATE Temas
                SET
                    nombre = @Nombre,
                    descripcion = @Descripcion,
                    idProyecto = @IdProyecto
                WHERE idTema = @IdTema";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdTema", tema.IdTema);
            comando.Parameters.AddWithValue("@Nombre", tema.Nombre);
            comando.Parameters.AddWithValue("@Descripcion", tema.Descripcion);
            comando.Parameters.AddWithValue("@IdProyecto", tema.IdProyecto);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Eliminar tema
        /// </summary>
        public bool Eliminar(int idTema)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                UPDATE Temas
                SET activo = 0
                WHERE idTema = @IdTema";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdTema", idTema);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Contar temas
        /// </summary>
        public int ContarTemas()
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "SELECT COUNT(*) FROM Temas WHERE ISNULL(activo, 1) = 1";

            using var comando = new SqlCommand(query, conexion);

            return (int)comando.ExecuteScalar();
        }
    }
}
