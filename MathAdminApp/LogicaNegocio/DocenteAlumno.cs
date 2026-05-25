// ============================================================
// Capa de Logica de Negocio: DocenteAlumnoBLL
// Logica para relaciones Docente-Alumno
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    public class DocenteAlumnoBLL
    {
        private readonly DocenteAlumnoDAO _dao = new();

        // =====================================================
        // OBTENER TODOS
        // =====================================================

        public List<DocenteAlumno> ObtenerTodos()
        {
            return _dao.ObtenerTodos();
        }

        // =====================================================
        // OBTENER POR ID
        // =====================================================

        public DocenteAlumno? ObtenerPorId(
            int id)
        {
            if (id <= 0)
            {
                throw new Exception(
                    "ID invalido."
                );
            }

            return _dao.ObtenerPorId(id);
        }

        // =====================================================
        // OBTENER POR DOCENTE
        // =====================================================

        public List<DocenteAlumno> ObtenerPorDocente(
            int idDocente)
        {
            if (idDocente <= 0)
            {
                throw new Exception(
                    "ID de docente invalido."
                );
            }

            return _dao.ObtenerPorDocente(idDocente);
        }

        // =====================================================
        // AGREGAR
        // =====================================================

        public bool Agregar(
            DocenteAlumno relacion)
        {
            if (relacion.IdDocente <= 0)
            {
                throw new Exception(
                    "Seleccione un docente."
                );
            }

            if (relacion.IdAlumno <= 0)
            {
                throw new Exception(
                    "Seleccione un alumno."
                );
            }

            return _dao.Agregar(relacion);
        }

        // =====================================================
        // EDITAR
        // =====================================================

        public bool Editar(
            DocenteAlumno relacion)
        {
            if (relacion.Id <= 0)
            {
                throw new Exception(
                    "ID invalido."
                );
            }

            if (relacion.IdDocente <= 0)
            {
                throw new Exception(
                    "Seleccione un docente."
                );
            }

            if (relacion.IdAlumno <= 0)
            {
                throw new Exception(
                    "Seleccione un alumno."
                );
            }

            return _dao.Actualizar(relacion);
        }

        // =====================================================
        // ELIMINAR
        // =====================================================

        public bool Eliminar(
            int id)
        {
            if (id <= 0)
            {
                throw new Exception(
                    "ID invalido."
                );
            }

            return _dao.Eliminar(id);
        }

        // =====================================================
        // CONTAR
        // =====================================================

        public int Contar()
        {
            return _dao.Contar();
        }
    }
}