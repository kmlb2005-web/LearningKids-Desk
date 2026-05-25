namespace MathAdminApp.Modelos
{
    public class Proyecto
    {
        public int idProyecto { get; set; }
        public string nombre { get; set; } = string.Empty;
        public string descripcion { get; set; } = string.Empty;
        public int grado { get; set; }
        public int idCampo { get; set; }
        public int creadoPor { get; set; }
    }
}
