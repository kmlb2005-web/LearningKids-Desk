// ============================================================
// Capa de Acceso a Datos: DocenteAlumnoDAL
// CRUD para la tabla DocenteAlumno
// ============================================================

using System.Data.SqlClient;
using MathAdminApp.Modelos;

namespace MathAdminApp.AccesoDatos
{
    public class DocenteAlumnoDAO
    {
        /// <summary>
        /// Obtener todas las relaciones docente-alumno
        /// </summary>
        public List<DocenteAlumno> ObtenerTodos()
        {
            List<DocenteAlumno> lista = new List<DocenteAlumno>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    id,
                    idDocente,
                    idAlumno
                FROM DocenteAlumno
                ORDER BY id";

            using var comando = new SqlCommand(query, conexion);
            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new DocenteAlumno
                {
                    Id = lector.GetInt32(0),
                    IdDocente = lector.GetInt32(1),
                    IdAlumno = lector.GetInt32(2)
                });
            }

            return lista;
        }

        /// <summary>
        /// Obtener una relación por ID
        /// </summary>
        public DocenteAlumno? ObtenerPorId(int id)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    id,
                    idDocente,
                    idAlumno
                FROM DocenteAlumno
                WHERE id = @Id";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            using var lector = comando.ExecuteReader();

            if (lector.Read())
            {
                return new DocenteAlumno
                {
                    Id = lector.GetInt32(0),
                    IdDocente = lector.GetInt32(1),
                    IdAlumno = lector.GetInt32(2)
                };
            }

            return null;
        }

        /// <summary>
        /// Obtener alumnos asignados a un docente
        /// </summary>
        public List<DocenteAlumno> ObtenerPorDocente(int idDocente)
        {
            List<DocenteAlumno> lista = new List<DocenteAlumno>();

            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                SELECT
                    id,
                    idDocente,
                    idAlumno
                FROM DocenteAlumno
                WHERE idDocente = @IdDocente";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdDocente", idDocente);

            using var lector = comando.ExecuteReader();

            while (lector.Read())
            {
                lista.Add(new DocenteAlumno
                {
                    Id = lector.GetInt32(0),
                    IdDocente = lector.GetInt32(1),
                    IdAlumno = lector.GetInt32(2)
                });
            }

            return lista;
        }

        /// <summary>
        /// Asignar alumno a docente
        /// </summary>
        public bool Agregar(DocenteAlumno relacion)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                INSERT INTO DocenteAlumno
                (
                    idDocente,
                    idAlumno
                )
                VALUES
                (
                    @IdDocente,
                    @IdAlumno
                )";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdDocente", relacion.IdDocente);
            comando.Parameters.AddWithValue("@IdAlumno", relacion.IdAlumno);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Actualizar relación docente-alumno
        /// </summary>
        public bool Actualizar(DocenteAlumno relacion)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                UPDATE DocenteAlumno
                SET
                    idDocente = @IdDocente,
                    idAlumno = @IdAlumno
                WHERE id = @Id";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@IdDocente", relacion.IdDocente);
            comando.Parameters.AddWithValue("@IdAlumno", relacion.IdAlumno);
            comando.Parameters.AddWithValue("@Id", relacion.Id);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Eliminar relación docente-alumno
        /// </summary>
        public bool Eliminar(int id)
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = @"
                DELETE FROM DocenteAlumno
                WHERE id = @Id";

            using var comando = new SqlCommand(query, conexion);

            comando.Parameters.AddWithValue("@Id", id);

            return comando.ExecuteNonQuery() > 0;
        }

        /// <summary>
        /// Contar relaciones docente-alumno
        /// </summary>
        public int Contar()
        {
            using var conexion = ConexionBD.ObtenerConexion();
            conexion.Open();

            string query = "SELECT COUNT(*) FROM DocenteAlumno";

            using var comando = new SqlCommand(query, conexion);

            return (int)comando.ExecuteScalar();
        }
    }
}