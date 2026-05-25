namespace MathAdminApp.Modelos
{
    public class Respuesta
    {
        public int IdRespuesta { get; set; }

        public string Texto { get; set; } = string.Empty;

        public bool EsCorrecta { get; set; }

        public int IdPregunta { get; set; }
    }
}
