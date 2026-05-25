// ============================================================
// Capa de Lógica de Negocio: ProyectoBLL
// Usa ProyectoDAL para operaciones CRUD
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    public class ProyectoBLL
    {
        private readonly ProyectoDAO _dal = new ProyectoDAO();

        /// <summary>
        /// Obtener todos los proyectos
        /// </summary>
        public List<Proyecto> ObtenerTodos()
        {
            return _dal.ObtenerTodos();
        }

        /// <summary>
        /// Obtener proyecto por ID
        /// </summary>
        public Proyecto? ObtenerPorId(int idProyecto)
        {
            if (idProyecto <= 0)
                return null;

            return _dal.ObtenerPorId(idProyecto);
        }

        /// <summary>
        /// Crear proyecto
        /// </summary>
        public bool Crear(Proyecto proyecto)
        {
            if (proyecto == null)
                return false;

            if (string.IsNullOrWhiteSpace(proyecto.nombre))
                return false;

            if (proyecto.grado <= 0)
                return false;

            if (proyecto.idCampo <= 0)
                return false;

            if (proyecto.creadoPor <= 0)
                proyecto.creadoPor = 1;

            return _dal.Agregar(proyecto);
        }

        /// <summary>
        /// Actualizar proyecto
        /// </summary>
        public bool Actualizar(Proyecto proyecto)
        {
            if (proyecto == null)
                return false;

            if (proyecto.idProyecto <= 0)
                return false;

            if (string.IsNullOrWhiteSpace(proyecto.nombre))
                return false;

            if (proyecto.grado <= 0 || proyecto.idCampo <= 0)
                return false;

            if (proyecto.creadoPor <= 0)
                proyecto.creadoPor = 1;

            return _dal.Actualizar(proyecto);
        }

        /// <summary>
        /// Eliminar proyecto
        /// </summary>
        public bool Eliminar(int idProyecto)
        {
            if (idProyecto <= 0)
                return false;

            return _dal.Eliminar(idProyecto);
        }

        /// <summary>
        /// Contar proyectos
        /// </summary>
        public int Contar()
        {
            return _dal.Contar();
        }
    }
}
