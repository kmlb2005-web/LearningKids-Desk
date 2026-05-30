namespace MathAdminApp.Modelos
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string Nombre { get; set; } = string.Empty;

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public int IdRol { get; set; }

        public bool Activo { get; set; } = true;
    }
}
