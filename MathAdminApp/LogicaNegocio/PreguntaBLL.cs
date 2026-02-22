// ============================================================
// Capa de Logica de Negocio: PreguntaBLL
// Logica de negocio para operaciones de preguntas
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    /// <summary>
    /// Clase de logica de negocio para la entidad Pregunta.
    /// </summary>
    public class PreguntaBLL
    {
        private readonly PreguntaDAL _dal = new();

        public List<Pregunta> ObtenerPorExamen(int examenId) => _dal.ObtenerPorExamen(examenId);

        public bool Agregar(Pregunta pregunta)
        {
            if (string.IsNullOrWhiteSpace(pregunta.Texto))
                throw new ArgumentException("El texto de la pregunta es obligatorio.");
            if (string.IsNullOrWhiteSpace(pregunta.RespuestaCorrecta))
                throw new ArgumentException("La respuesta correcta es obligatoria.");
            if (pregunta.Tipo == "Multiple")
            {
                if (string.IsNullOrWhiteSpace(pregunta.OpcionA) ||
                    string.IsNullOrWhiteSpace(pregunta.OpcionB))
                    throw new ArgumentException("Las opciones A y B son obligatorias para opcion multiple.");
            }

            return _dal.Agregar(pregunta);
        }

        public bool Eliminar(int id) => _dal.Eliminar(id);
    }
}
