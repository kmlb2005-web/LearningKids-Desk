using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    public class CampoBLL
    {
        private static List<CampoFormativo> _campos =
            new List<CampoFormativo>();

        // =====================================================
        // OBTENER TODOS
        // =====================================================

        public List<CampoFormativo> ObtenerTodos()
        {
            return _campos;
        }

        // =====================================================
        // AGREGAR
        // =====================================================

        public void Agregar(
            CampoFormativo campo
        )
        {
            if (string.IsNullOrWhiteSpace(campo.Nombre))
            {
                throw new Exception(
                    "Ingrese un nombre."
                );
            }

            campo.IdCampo =
                _campos.Count + 1;

            _campos.Add(campo);
        }

        // =====================================================
        // EDITAR
        // =====================================================

        public void Editar(
            CampoFormativo campo
        )
        {
            var existente =
                _campos.FirstOrDefault(x =>
                    x.IdCampo == campo.IdCampo
                );

            if (existente != null)
            {
                existente.Nombre =
                    campo.Nombre;
            }
        }

        // =====================================================
        // ELIMINAR
        // =====================================================

        public void Eliminar(
            int id
        )
        {
            var campo =
                _campos.FirstOrDefault(x =>
                    x.IdCampo == id
                );

            if (campo != null)
            {
                _campos.Remove(campo);
            }
        }
    }
}