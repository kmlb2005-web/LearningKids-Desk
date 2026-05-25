namespace MathAdminApp.Modelos
{
    public class Tema
    {
        public int idTema { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public int idProyecto { get; set; }
        public string nombreProyecto { get; set; } = string.Empty;
    }
}