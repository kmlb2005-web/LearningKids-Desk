// ============================================================
// Capa de Logica de Negocio: RolBLL
// Logica de negocio para operaciones de roles
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    internal class RolBLL
    {
        private readonly RolDAO _dal = new();

        /// <summary>
        /// Obtener todos los roles
        /// </summary>
        public List<Rol> ObtenerRoles()
        {
            return _dal.ObtenerRoles();
        }

        /// <summary>
        /// Obtener rol por ID
        /// </summary>
        public Rol? ObtenerPorId(int idRol)
        {
            if (idRol <= 0)
                throw new ArgumentException(
                    "ID de rol inválido.");

            return _dal.ObtenerPorId(idRol);
        }

        /// <summary>
        /// Obtener rol por nombre
        /// </summary>
        public Rol? ObtenerPorNombre(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException(
                    "El nombre del rol es obligatorio.");

            return _dal.ObtenerPorNombre(nombre.Trim());
        }

        /// <summary>
        /// Agregar rol
        /// </summary>
        public bool AgregarRol(Rol rol)
        {
            if (string.IsNullOrWhiteSpace(rol.Nombre))
                throw new ArgumentException(
                    "El nombre del rol es obligatorio.");

            return _dal.Agregar(rol);
        }

        /// <summary>
        /// Actualizar rol
        /// </summary>
        public bool ActualizarRol(Rol rol)
        {
            if (rol.IdRol <= 0)
                throw new ArgumentException(
                    "Rol inválido.");

            if (string.IsNullOrWhiteSpace(rol.Nombre))
                throw new ArgumentException(
                    "El nombre del rol es obligatorio.");

            return _dal.Actualizar(rol);
        }

        /// <summary>
        /// Eliminar rol
        /// </summary>
        public bool EliminarRol(int idRol)
        {
            if (idRol <= 0)
                throw new ArgumentException(
                    "ID inválido.");

            return _dal.Eliminar(idRol);
        }

        /// <summary>
        /// Contar roles
        /// </summary>
        public int ContarRoles()
        {
            return _dal.ContarRoles();
        }
    }
}