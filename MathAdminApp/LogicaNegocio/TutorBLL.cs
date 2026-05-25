// ============================================================
// Capa de Logica de Negocio: TutorBLL
// Logica de negocio para operaciones de tutores
// ============================================================
using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    internal class TutorBLL
    {
        private readonly TutorDAO _dal = new();

        /// <summary>
        /// Obtener todos los tutores
        /// </summary>
        public List<Tutor> ObtenerTutores()
        {
            return _dal.ObtenerTutores();
        }

        /// <summary>
        /// Obtener tutor por ID
        /// </summary>
        public Tutor? ObtenerPorId(int idTutor)
        {
            if (idTutor <= 0)
                throw new ArgumentException(
                    "ID de tutor inválido.");

            return _dal.ObtenerPorId(idTutor);
        }

        /// <summary>
        /// Obtener tutor por correo
        /// </summary>
        public Tutor? ObtenerPorCorreo(string correo)
        {
            if (string.IsNullOrWhiteSpace(correo))
                throw new ArgumentException(
                    "El correo es obligatorio.");

            return _dal.ObtenerPorCorreo(correo.Trim());
        }

        /// <summary>
        /// Agregar tutor
        /// </summary>
        public bool AgregarTutor(Tutor tutor)
        {
            if (string.IsNullOrWhiteSpace(tutor.Nombre))
                throw new ArgumentException(
                    "El nombre del tutor es obligatorio.");

            if (string.IsNullOrWhiteSpace(tutor.Correo))
                throw new ArgumentException(
                    "El correo es obligatorio.");

            return _dal.Agregar(tutor);
        }

        /// <summary>
        /// Actualizar tutor
        /// </summary>
        public bool ActualizarTutor(Tutor tutor)
        {
            if (tutor.IdTutor <= 0)
                throw new ArgumentException(
                    "Tutor inválido.");

            if (string.IsNullOrWhiteSpace(tutor.Nombre))
                throw new ArgumentException(
                    "El nombre del tutor es obligatorio.");

            if (string.IsNullOrWhiteSpace(tutor.Correo))
                throw new ArgumentException(
                    "El correo es obligatorio.");

            return _dal.Actualizar(tutor);
        }

        /// <summary>
        /// Eliminar tutor
        /// </summary>
        public bool EliminarTutor(int idTutor)
        {
            if (idTutor <= 0)
                throw new ArgumentException(
                    "ID inválido.");

            return _dal.Eliminar(idTutor);
        }

        /// <summary>
        /// Contar tutores
        /// </summary>
        public int ContarTutores()
        {
            return _dal.ContarTutores();
        }
    }
}