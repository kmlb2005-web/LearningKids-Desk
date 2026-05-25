// ============================================================
// Capa de Logica de Negocio: UsuarioBLL
// Logica de negocio para operaciones de usuarios
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    /// <summary>
    /// Clase de logica de negocio para usuarios
    /// </summary>
    public class UsuarioBLL
    {
        private readonly UsuarioDAO _dao = new();

        /// <summary>
        /// Validar inicio de sesion
        /// </summary>
        public Usuario? IniciarSesion(
            string username,
            string password)
        {
            if (string.IsNullOrWhiteSpace(username))
                throw new ArgumentException(
                    "El usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException(
                    "La contraseña es obligatoria.");

            return _dao.ValidarLogin(
                username.Trim(),
                password.Trim());
        }

        /// <summary>
        /// Obtener todos los usuarios
        /// </summary>
        public List<Usuario> ObtenerUsuarios()
        {
            return _dao.ObtenerUsuarios();
        }

        /// <summary>
        /// Obtener solo alumnos
        /// </summary>
        public List<Usuario> ObtenerAlumnos()
        {
            return _dao.ObtenerAlumnos();
        }

        /// <summary>
        /// Agregar usuario
        /// </summary>
        public bool AgregarUsuario(Usuario usuario)
        {
            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException(
                    "El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Username))
                throw new ArgumentException(
                    "El usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Password))
                throw new ArgumentException(
                    "La contraseña es obligatoria.");

            if (usuario.IdRol <= 0)
                throw new ArgumentException(
                    "Debe seleccionar un rol.");

            return _dao.Agregar(usuario);
        }

        /// <summary>
        /// Actualizar usuario
        /// </summary>
        public bool ActualizarUsuario(
            Usuario usuario)
        {
            if (usuario.IdUsuario <= 0)
                throw new ArgumentException(
                    "Usuario inválido.");

            if (string.IsNullOrWhiteSpace(usuario.Nombre))
                throw new ArgumentException(
                    "El nombre es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Username))
                throw new ArgumentException(
                    "El usuario es obligatorio.");

            if (string.IsNullOrWhiteSpace(usuario.Password))
                throw new ArgumentException(
                    "La contraseña es obligatoria.");

            return _dao.Actualizar(usuario);
        }

        /// <summary>
        /// Eliminar usuario
        /// </summary>
        public bool EliminarUsuario(
            int idUsuario)
        {
            if (idUsuario <= 0)
                throw new ArgumentException(
                    "ID inválido.");

            return _dao.Eliminar(idUsuario);
        }

        /// <summary>
        /// Total alumnos
        /// </summary>
        public int ContarAlumnos()
        {
            return _dao.ContarAlumnos();
        }
    }
}