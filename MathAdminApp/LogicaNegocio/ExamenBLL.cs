// ============================================================
// Capa de Logica de Negocio: ExamenBLL
// Logica de negocio para operaciones de examenes
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    /// <summary>
    /// Clase de logica de negocio para la entidad Examen.
    /// </summary>
    public class ExamenBLL
    {
        private readonly ExamenDAL _dal = new();

        public List<Examen> ObtenerPorUnidad(int? unidadId = null) => _dal.ObtenerPorUnidad(unidadId);

        public bool Agregar(Examen examen)
        {
            if (string.IsNullOrWhiteSpace(examen.Nombre))
                throw new ArgumentException("El nombre del examen es obligatorio.");
            if (examen.UnidadId <= 0)
                throw new ArgumentException("Debe seleccionar una unidad.");

            return _dal.Agregar(examen);
        }

        public bool Eliminar(int id) => _dal.Eliminar(id);

        public int ContarExamenes() => _dal.ContarExamenes();
    }
}
