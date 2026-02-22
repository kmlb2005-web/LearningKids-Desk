// ============================================================
// Modelo: Pregunta
// Representa una pregunta dentro de un examen
// ============================================================

namespace MathAdminApp.Modelos
{
    /// <summary>
    /// Clase que representa una pregunta de examen.
    /// Puede ser de opcion multiple o abierta.
    /// </summary>
    public class Pregunta
    {
        /// <summary>Identificador unico de la pregunta</summary>
        public int Id { get; set; }

        /// <summary>ID del examen al que pertenece</summary>
        public int ExamenId { get; set; }

        /// <summary>Texto de la pregunta</summary>
        public string Texto { get; set; } = string.Empty;

        /// <summary>Tipo de pregunta: "Multiple" o "Abierta"</summary>
        public string Tipo { get; set; } = "Multiple";

        /// <summary>Opcion A (solo para opcion multiple)</summary>
        public string OpcionA { get; set; } = string.Empty;

        /// <summary>Opcion B (solo para opcion multiple)</summary>
        public string OpcionB { get; set; } = string.Empty;

        /// <summary>Opcion C (solo para opcion multiple)</summary>
        public string OpcionC { get; set; } = string.Empty;

        /// <summary>Opcion D (solo para opcion multiple)</summary>
        public string OpcionD { get; set; } = string.Empty;

        /// <summary>Respuesta correcta (letra o texto)</summary>
        public string RespuestaCorrecta { get; set; } = string.Empty;
    }
}
