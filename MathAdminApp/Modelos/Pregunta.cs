namespace MathAdminApp.Modelos
{
    public class Pregunta
    {
        public int IdPregunta { get; set; }

        public string Texto { get; set; } = string.Empty;

        public int IdPrueba { get; set; }

        public bool Activo { get; set; } = true;
    }
}
