namespace MathAdminApp.Modelos
{
    public class Tema
    {
        public int IdTema { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Descripcion { get; set; } = string.Empty;

        public int IdProyecto { get; set; }

        public bool Activo { get; set; } = true;
    }
}
