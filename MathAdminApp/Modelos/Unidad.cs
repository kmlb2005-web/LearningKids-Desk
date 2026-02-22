// ============================================================
// Modelo: Unidad
// Representa una unidad tematica del curso de matematicas
// ============================================================

namespace MathAdminApp.Modelos
{
    /// <summary>
    /// Clase que representa una unidad tematica.
    /// Cada unidad contiene examenes asociados.
    /// </summary>
    public class Unidad
    {
        /// <summary>Identificador unico de la unidad</summary>
        public int Id { get; set; }

        /// <summary>Nombre de la unidad (ej: "Fracciones")</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Descripcion detallada de la unidad</summary>
        public string Descripcion { get; set; } = string.Empty;

        /// <summary>Numero de orden de la unidad en el curso</summary>
        public int NumeroUnidad { get; set; }

        /// <summary>Indica si la unidad esta activa</summary>
        public bool Activa { get; set; } = true;
    }
}
