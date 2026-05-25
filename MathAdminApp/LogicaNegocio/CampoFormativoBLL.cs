// ============================================================
// Capa de Logica de Negocio: CampoFormativoBLL
// Logica para campos formativos
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    public class CampoFormativoBLL
    {
        private readonly CampoFormativoDAO _dao = new();

        // =====================================================
        // OBTENER TODOS
        // =====================================================

        public List<CampoFormativo> ObtenerTodos()
        {
            return _dao.ObtenerTodos();
        }

        // =====================================================
        // OBTENER POR ID
        // =====================================================

        public CampoFormativo? ObtenerPorId(
            int idCampo)
        {
            if (idCampo <= 0)
            {
                throw new Exception(
                    "ID invalido."
                );
            }

            return _dao.ObtenerPorId(idCampo);
        }

        // =====================================================
        // AGREGAR
        // =====================================================

        public bool Agregar(
            CampoFormativo campo)
        {
            if (string.IsNullOrWhiteSpace(
                campo.Nombre))
            {
                throw new Exception(
                    "Ingrese un nombre."
                );
            }

            return _dao.Agregar(campo);
        }

        // =====================================================
        // EDITAR
        // =====================================================

        public bool Editar(
            CampoFormativo campo)
        {
            if (campo.IdCampo <= 0)
            {
                throw new Exception(
                    "ID invalido."
                );
            }

            if (string.IsNullOrWhiteSpace(
                campo.Nombre))
            {
                throw new Exception(
                    "Ingrese un nombre."
                );
            }

            return _dao.Actualizar(campo);
        }

        // =====================================================
        // ELIMINAR
        // =====================================================

        public bool Eliminar(
            int idCampo)
        {
            if (idCampo <= 0)
            {
                throw new Exception(
                    "ID invalido."
                );
            }

            return _dao.Eliminar(idCampo);
        }

        // =====================================================
        // CONTAR
        // =====================================================

        public int Contar()
        {
            return _dao.Contar();
        }
    }
}