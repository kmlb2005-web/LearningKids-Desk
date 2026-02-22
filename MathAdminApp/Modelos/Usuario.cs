// ============================================================
// Modelo: Usuario
// Representa a un usuario del sistema (alumno o administrador)
// ============================================================

namespace MathAdminApp.Modelos
{
    /// <summary>
    /// Clase que representa un usuario en el sistema.
    /// Puede ser un alumno o un administrador.
    /// </summary>
    public class Usuario
    {
        /// <summary>Identificador unico del usuario</summary>
        public int Id { get; set; }

        /// <summary>Nombre completo del usuario</summary>
        public string Nombre { get; set; } = string.Empty;

        /// <summary>Correo electronico del usuario</summary>
        public string Correo { get; set; } = string.Empty;

        /// <summary>Nombre de usuario para iniciar sesion</summary>
        public string NombreUsuario { get; set; } = string.Empty;

        /// <summary>Contrasena del usuario</summary>
        public string Contrasena { get; set; } = string.Empty;

        /// <summary>Grado escolar del alumno (ej: "6to")</summary>
        public string Grado { get; set; } = string.Empty;

        /// <summary>Rol del usuario: "Administrador" o "Alumno"</summary>
        public string Rol { get; set; } = "Alumno";

        /// <summary>Indica si el usuario esta activo en el sistema</summary>
        public bool Activo { get; set; } = true;

        /// <summary>Fecha de creacion del registro</summary>
        public DateTime FechaCreacion { get; set; } = DateTime.Now;
    }
}
