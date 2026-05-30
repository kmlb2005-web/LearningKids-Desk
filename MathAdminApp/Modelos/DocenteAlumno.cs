namespace MathAdminApp.Modelos
{
    public class DocenteAlumno
    {
        public int Id { get; set; }

        public int IdDocente { get; set; }

        public int IdAlumno { get; set; }

        public bool Activo { get; set; } = true;
    }
}
