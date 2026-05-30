namespace MathAdminApp.Modelos
{
    public class Alumno
    {
        public int IdAlumno { get; set; }

        public int? IdTutor { get; set; }

        public int? Grado { get; set; }

        public bool Activo { get; set; } = true;
    }
}
