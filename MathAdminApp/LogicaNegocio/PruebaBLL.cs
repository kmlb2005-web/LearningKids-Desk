// ============================================================
// Capa de Lógica de Negocio: PruebaBLL
// Usa PruebaDAO para acceso a datos
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    public class PruebaBLL
    {
        private readonly PruebaDAO _dao = new PruebaDAO();

        // =====================================================
        // OBTENER POR TEMA
        // =====================================================
        public List<Prueba> ObtenerPorTema(int? idTema = null)
        {
            if (idTema.HasValue && idTema.Value <= 0)
                return new List<Prueba>();

            return _dao.ObtenerPorTema(idTema);
        }

        public List<Prueba> ObtenerPorDocente(int idDocente, int? idTema = null)
        {
            if (idDocente <= 0)
                return new List<Prueba>();

            if (idTema.HasValue && idTema.Value <= 0)
                return new List<Prueba>();

            return _dao.ObtenerPorDocente(idDocente, idTema);
        }

        public List<Prueba> ObtenerVisibles(Usuario usuarioActual, int? idTema = null)
        {
            return usuarioActual.IdRol == 1
                ? ObtenerPorTema(idTema)
                : ObtenerPorDocente(usuarioActual.IdUsuario, idTema);
        }

        // =====================================================
        // AGREGAR
        // =====================================================
        public bool Agregar(Prueba prueba)
        {
            if (prueba == null)
                return false;

            if (string.IsNullOrWhiteSpace(prueba.Titulo))
                return false;

            if (prueba.IdTema <= 0)
                return false;

            if (prueba.CreadoPor <= 0)
                return false;

            return _dao.Agregar(prueba);
        }

        // =====================================================
        // ELIMINAR
        // =====================================================
        public bool Eliminar(int idPrueba)
        {
            if (idPrueba <= 0)
                return false;

            return _dao.Eliminar(idPrueba);
        }

        // =====================================================
        // CONTAR
        // =====================================================
        public int Contar()
        {
            return _dao.ContarPruebas();
        }
    }
}
