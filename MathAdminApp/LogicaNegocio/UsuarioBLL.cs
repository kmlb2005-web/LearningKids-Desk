// ============================================================
// Capa de Logica de Negocio: UsuarioBLL
// Logica de negocio para operaciones de usuarios
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    /// <summary>
    /// Clase de logica de negocio para la entidad Usuario.
    /// Contiene validaciones y reglas de negocio.
    /// </summary>
    public class UsuarioBLL
    {
        private readonly UsuarioDAL _dal = new();

        /// <summary>
        /// Valida credenciales y retorna el usuario si es correcto.
        /// </summary>
        public Usuario? IniciarSesion(string nombreUsuario, string contrasena)
        {
            if (string.IsNullOrWhiteSpace(nombreUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(contrasena))
                throw new ArgumentException("La contrasena es obligatoria.");

            return _dal.ValidarLogin(nombreUsuario.Trim(), contrasena);
        }

        /// <summary>
        /// Obtiene la lista de todos los alumnos.
        /// </summary>
        public List<Usuario> ObtenerAlumnos() => _dal.ObtenerAlumnos();

        /// <summary>
        /// Agrega un nuevo alumno con validaciones.
        /// </summary>
        public bool AgregarAlumno(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(usuario.Correo))
                throw new ArgumentException("El correo es obligatorio.");
            if (string.IsNullOrWhiteSpace(usuario.NombreUsuario))
                throw new ArgumentException("El nombre de usuario es obligatorio.");
            if (string.IsNullOrWhiteSpace(usuario.Contrasena))
                throw new ArgumentException("La contrasena es obligatoria.");

            usuario.Rol = "Alumno";
            return _dal.Agregar(usuario);
        }

        /// <summary>
        /// Actualiza los datos de un alumno.
        /// </summary>
        public bool ActualizarAlumno(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException("El nombre es obligatorio.");
            if (string.IsNullOrWhiteSpace(usuario.Correo))
                throw new ArgumentException("El correo es obligatorio.");

            return _dal.Actualizar(usuario);
        }

        /// <summary>
        /// Desactiva un alumno.
        /// </summary>
        public bool DesactivarAlumno(int id) => _dal.Desactivar(id);

        /// <summary>
        /// Cuenta el total de alumnos activos.
        /// </summary>
        public int ContarAlumnos() => _dal.ContarAlumnos();
    }
}
