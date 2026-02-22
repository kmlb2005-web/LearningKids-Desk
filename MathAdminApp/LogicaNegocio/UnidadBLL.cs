// ============================================================
// Capa de Logica de Negocio: UnidadBLL
// Logica de negocio para operaciones de unidades
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    /// <summary>
    /// Clase de logica de negocio para la entidad Unidad.
    /// </summary>
    public class UnidadBLL
    {
        private readonly UnidadDAL _dal = new();

        public List<Unidad> ObtenerTodas() => _dal.ObtenerTodas();

        public bool Agregar(Unidad unidad)
        {
            if (string.IsNullOrWhiteSpace(unidad.Nombre))
                throw new ArgumentException("El nombre de la unidad es obligatorio.");
            if (unidad.NumeroUnidad <= 0)
                throw new ArgumentException("El numero de unidad debe ser mayor a 0.");

            return _dal.Agregar(unidad);
        }

        public bool Actualizar(Unidad unidad)
        {
            if (string.IsNullOrWhiteSpace(unidad.Nombre))
                throw new ArgumentException("El nombre de la unidad es obligatorio.");
            if (unidad.NumeroUnidad <= 0)
                throw new ArgumentException("El numero de unidad debe ser mayor a 0.");

            return _dal.Actualizar(unidad);
        }

        public bool Eliminar(int id) => _dal.Eliminar(id);

        public int ContarUnidades() => _dal.ContarUnidades();
    }
}
