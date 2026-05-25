// ============================================================
// Capa de Logica de Negocio: AlumnoBLL
// Logica de negocio para operaciones de alumnos
// ============================================================
using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    internal class AlumnoBLL
    {
        private readonly AlumnoDAO _dal = new();

        /// <summary>
        /// Obtener todos los alumnos
        /// </summary>
        public List<Alumno> ObtenerAlumnos()
        {
            return _dal.ObtenerAlumnos();
        }

        /// <summary>
        /// Obtener alumno por ID
        /// </summary>
        public Alumno? ObtenerPorId(int idAlumno)
        {
            if (idAlumno <= 0)
                throw new ArgumentException(
                    "ID de alumno inválido.");

            return _dal.ObtenerPorId(idAlumno);
        }

        /// <summary>
        /// Agregar alumno
        /// </summary>
        public bool AgregarAlumno(Alumno alumno)
        {
            if (alumno.IdAlumno <= 0)
                throw new ArgumentException(
                    "El ID del alumno es inválido.");

            if (alumno.IdTutor.HasValue &&
                alumno.IdTutor <= 0)
            {
                throw new ArgumentException(
                    "El tutor es inválido.");
            }

            if (alumno.Grado.HasValue &&
                alumno.Grado <= 0)
            {
                throw new ArgumentException(
                    "El grado es inválido.");
            }

            return _dal.Agregar(alumno);
        }

        /// <summary>
        /// Actualizar alumno
        /// </summary>
        public bool ActualizarAlumno(Alumno alumno)
        {
            if (alumno.IdAlumno <= 0)
                throw new ArgumentException(
                    "El ID del alumno es inválido.");

            if (alumno.IdTutor.HasValue &&
                alumno.IdTutor <= 0)
            {
                throw new ArgumentException(
                    "El tutor es inválido.");
            }

            if (alumno.Grado.HasValue &&
                alumno.Grado <= 0)
            {
                throw new ArgumentException(
                    "El grado es inválido.");
            }

            return _dal.Actualizar(alumno);
        }

        /// <summary>
        /// Eliminar alumno
        /// </summary>
        public bool EliminarAlumno(int idAlumno)
        {
            if (idAlumno <= 0)
                throw new ArgumentException(
                    "ID inválido.");

            return _dal.Eliminar(idAlumno);
        }

        /// <summary>
        /// Contar alumnos
        /// </summary>
        public int ContarAlumnos()
        {
            return _dal.ContarAlumnos();
        }
    }
}