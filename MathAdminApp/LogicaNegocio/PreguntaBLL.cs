// ============================================================
// Capa de Logica de Negocio: PreguntaBLL
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    public class PreguntaBLL
    {
        private readonly PreguntaDAL _dal = new();

        public List<Pregunta> ObtenerPorExamen(
            int examenId
        ) => _dal.ObtenerPorExamen(examenId);

        // =====================================================
        // AGREGAR
        // =====================================================

        public bool Agregar(Pregunta pregunta)
        {
            ValidarPregunta(pregunta);

            return _dal.Agregar(pregunta);
        }

        // =====================================================
        // ACTUALIZAR
        // =====================================================

        public bool Actualizar(Pregunta pregunta)
        {
            ValidarPregunta(pregunta);

            return _dal.Actualizar(pregunta);
        }

        // =====================================================
        // ELIMINAR
        // =====================================================

        public bool Eliminar(int id)
            => _dal.Eliminar(id);

        // =====================================================
        // VALIDAR
        // =====================================================

        private void ValidarPregunta(
            Pregunta pregunta
        )
        {
            if (string.IsNullOrWhiteSpace(
                pregunta.Texto))
            {
                throw new ArgumentException(
                    "El texto de la pregunta es obligatorio."
                );
            }

            if (string.IsNullOrWhiteSpace(
                pregunta.RespuestaCorrecta))
            {
                throw new ArgumentException(
                    "La respuesta correcta es obligatoria."
                );
            }

            if (pregunta.Tipo == "Multiple")
            {
                if (string.IsNullOrWhiteSpace(
                        pregunta.OpcionA)
                    ||
                    string.IsNullOrWhiteSpace(
                        pregunta.OpcionB))
                {
                    throw new ArgumentException(
                        "Las opciones A y B son obligatorias."
                    );
                }
            }
        }
    }
}