// ============================================================
// Modelo: Examen
// Representa un examen asociado a una unidad
// ============================================================

namespace MathAdminApp.Modelos
{
    /// <summary>
    /// Clase que representa un examen.
    /// Cada examen pertenece a una unidad y contiene preguntas.
    /// </summary>
    public class Examen
    {
        /// <summary>Identificador unico del examen</summary>
        public int Id { get; set; }

        /// <summary>Nombre o titulo del examen</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>ID de la unidad a la que pertenece este examen</summary>
        public int UnidadId { get; set; }

        /// <summary>Nombre de la unidad (para mostrar en tablas)</summary>
        public string NombreUnidad { get; set; } = string.Empty;

        /// <summary>Fecha de creacion del examen</summary>
        public DateTime FechaCreacion { get; set; } = DateTime.Now;

        /// <summary>Indica si el examen esta activo</summary>
        public bool Activo { get; set; } = true;
    }
}
