// ============================================================
// Capa de Logica de Negocio: TemaBLL
// Logica de negocio para operaciones de temas
// ============================================================
using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    internal class TemaBLL
    {
        private readonly TemaDAO _dal = new();

        /// <summary>
        /// Obtener todos los temas
        /// </summary>
        public List<Tema> ObtenerTemas()
        {
            return _dal.ObtenerTemas();
        }

        /// <summary>
        /// Obtener tema por ID
        /// </summary>
        public Tema? ObtenerPorId(int idTema)
        {
            if (idTema <= 0)
                throw new ArgumentException(
                    "ID de tema inválido.");

            return _dal.ObtenerPorId(idTema);
        }

        /// <summary>
        /// Obtener temas por proyecto
        /// </summary>
        public List<Tema> ObtenerPorProyecto(int idProyecto)
        {
            if (idProyecto <= 0)
                throw new ArgumentException(
                    "ID de proyecto inválido.");

            return _dal.ObtenerPorProyecto(idProyecto);
        }

        /// <summary>
        /// Agregar tema
        /// </summary>
        public bool AgregarTema(Tema tema)
        {
            if (string.IsNullOrWhiteSpace(tema.Nombre))
                throw new ArgumentException(
                    "El nombre del tema es obligatorio.");

            if (string.IsNullOrWhiteSpace(tema.Descripcion))
                throw new ArgumentException(
                    "La descripción es obligatoria.");

            if (tema.IdProyecto <= 0)
                throw new ArgumentException(
                    "El proyecto es obligatorio.");

            return _dal.Agregar(tema);
        }

        /// <summary>
        /// Actualizar tema
        /// </summary>
        public bool ActualizarTema(Tema tema)
        {
            if (tema.IdTema <= 0)
                throw new ArgumentException(
                    "Tema inválido.");

            if (string.IsNullOrWhiteSpace(tema.Nombre))
                throw new ArgumentException(
                    "El nombre del tema es obligatorio.");

            if (string.IsNullOrWhiteSpace(tema.Descripcion))
                throw new ArgumentException(
                    "La descripción es obligatoria.");

            if (tema.IdProyecto <= 0)
                throw new ArgumentException(
                    "El proyecto es obligatorio.");

            return _dal.Actualizar(tema);
        }

        /// <summary>
        /// Eliminar tema
        /// </summary>
        public bool EliminarTema(int idTema)
        {
            if (idTema <= 0)
                throw new ArgumentException(
                    "ID inválido.");

            return _dal.Eliminar(idTema);
        }

        /// <summary>
        /// Contar temas
        /// </summary>
        public int ContarTemas()
        {
            return _dal.ContarTemas();
        }
    }
}