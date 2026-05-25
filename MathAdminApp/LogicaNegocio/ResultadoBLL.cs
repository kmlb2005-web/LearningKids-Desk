// ============================================================
// Capa de Logica de Negocio: ResultadoBLL
// Logica de negocio para operaciones de resultados
// ============================================================
using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    /// <summary>
    /// Clase de logica de negocio para resultados
    /// </summary>
    public class ResultadoBLL
    {
        private readonly ResultadoDAO _dal = new();

        /// <summary>
        /// Obtener todos los resultados
        /// </summary>
        public List<Resultado> ObtenerResultados()
        {
            return _dal.ObtenerResultados();
        }

        /// <summary>
        /// Obtener resultado por ID
        /// </summary>
        public Resultado? ObtenerPorId(int idResultado)
        {
            if (idResultado <= 0)
                throw new ArgumentException(
                    "ID de resultado inválido.");

            return _dal.ObtenerPorId(idResultado);
        }

        /// <summary>
        /// Obtener resultados por alumno
        /// </summary>
        public List<Resultado> ObtenerPorAlumno(int idAlumno)
        {
            if (idAlumno <= 0)
                throw new ArgumentException(
                    "ID de alumno inválido.");

            return _dal.ObtenerPorAlumno(idAlumno);
        }

        /// <summary>
        /// Agregar resultado
        /// </summary>
        public bool AgregarResultado(Resultado resultado)
        {
            if (resultado.IdAlumno <= 0)
                throw new ArgumentException(
                    "El alumno es obligatorio.");

            if (resultado.IdPrueba <= 0)
                throw new ArgumentException(
                    "La prueba es obligatoria.");

            if (resultado.Calificacion < 0)
                throw new ArgumentException(
                    "La calificación no puede ser negativa.");

            if (resultado.Fecha == DateTime.MinValue)
                throw new ArgumentException(
                    "La fecha es inválida.");

            return _dal.Agregar(resultado);
        }

        /// <summary>
        /// Actualizar resultado
        /// </summary>
        public bool ActualizarResultado(Resultado resultado)
        {
            if (resultado.IdResultado <= 0)
                throw new ArgumentException(
                    "Resultado inválido.");

            if (resultado.IdAlumno <= 0)
                throw new ArgumentException(
                    "El alumno es obligatorio.");

            if (resultado.IdPrueba <= 0)
                throw new ArgumentException(
                    "La prueba es obligatoria.");

            if (resultado.Calificacion < 0)
                throw new ArgumentException(
                    "La calificación no puede ser negativa.");

            if (resultado.Fecha == DateTime.MinValue)
                throw new ArgumentException(
                    "La fecha es inválida.");

            return _dal.Actualizar(resultado);
        }

        /// <summary>
        /// Eliminar resultado
        /// </summary>
        public bool EliminarResultado(int idResultado)
        {
            if (idResultado <= 0)
                throw new ArgumentException(
                    "ID inválido.");

            return _dal.Eliminar(idResultado);
        }

        /// <summary>
        /// Contar resultados
        /// </summary>
        public int ContarResultados()
        {
            return _dal.ContarResultados();
        }
    }
}