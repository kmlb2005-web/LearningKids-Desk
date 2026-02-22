// ============================================================
// Modelo: ResultadoExamen
// Representa el resultado de un alumno en un examen
// ============================================================

namespace MathAdminApp.Modelos
{
    /// <summary>
    /// Clase que representa el resultado de un alumno en un examen.
    /// </summary>
    public class ResultadoExamen
    {
        /// <summary>Identificador unico del resultado</summary>
        public int Id { get; set; }

        /// <summary>ID del alumno que presento el examen</summary>
        public int UsuarioId { get; set; }

        /// <summary>Nombre del alumno (para mostrar en tablas)</summary>
        public string NombreAlumno { get; set; } = string.Empty;

        /// <summary>ID del examen presentado</summary>
        public int ExamenId { get; set; }

        /// <summary>Nombre del examen (para mostrar en tablas)</summary>
        public string NombreExamen { get; set; } = string.Empty;

        /// <summary>Nombre de la unidad del examen</summary>
        public string NombreUnidad { get; set; } = string.Empty;

        /// <summary>Calificacion obtenida (0 a 100)</summary>
        public decimal Calificacion { get; set; }

        /// <summary>Fecha en que se presento el examen</summary>
        public DateTime FechaPresentacion { get; set; } = DateTime.Now;
    }
}
