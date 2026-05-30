namespace MathAdminApp.Modelos
{
    public class Resultado
    {
        public int IdResultado { get; set; }

        public int IdAlumno { get; set; }

        public int IdPrueba { get; set; }

        public decimal Calificacion { get; set; }

        public DateTime Fecha { get; set; }

        public bool Activo { get; set; } = true;
    }
}
