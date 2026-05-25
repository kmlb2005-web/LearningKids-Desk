// ============================================================
// Capa de Logica de Negocio: PreguntaBLL
// Logica para preguntas
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    public class PreguntaBLL
    {
        private readonly PreguntaDAO _dao = new();

        // =====================================================
        // OBTENER POR PRUEBA
        // =====================================================

        public List<Pregunta> ObtenerPorPrueba(
            int idPrueba)
        {
            if (idPrueba <= 0)
            {
                throw new Exception(
                    "ID de prueba invalido."
                );
            }

            return _dao.ObtenerPorPrueba(
                idPrueba
            );
        }

        // =====================================================
        // AGREGAR
        // =====================================================

        public bool Agregar(
            Pregunta pregunta)
        {
            if (string.IsNullOrWhiteSpace(
                pregunta.Texto))
            {
                throw new Exception(
                    "Ingrese una pregunta."
                );
            }

            if (pregunta.IdPrueba <= 0)
            {
                throw new Exception(
                    "Seleccione una prueba."
                );
            }

            return _dao.Agregar(
                pregunta
            );
        }

        // =====================================================
        // EDITAR
        // =====================================================

        public bool Editar(
            Pregunta pregunta)
        {
            if (pregunta.IdPregunta <= 0)
            {
                throw new Exception(
                    "ID invalido."
                );
            }

            if (string.IsNullOrWhiteSpace(
                pregunta.Texto))
            {
                throw new Exception(
                    "Ingrese una pregunta."
                );
            }

            if (pregunta.IdPrueba <= 0)
            {
                throw new Exception(
                    "Seleccione una prueba."
                );
            }

            return _dao.Actualizar(
                pregunta
            );
        }

        // =====================================================
        // ELIMINAR
        // =====================================================

        public bool Eliminar(
            int idPregunta)
        {
            if (idPregunta <= 0)
            {
                throw new Exception(
                    "ID invalido."
                );
            }

            return _dao.Eliminar(
                idPregunta
            );
        }
    }
}