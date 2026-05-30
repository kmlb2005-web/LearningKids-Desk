using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    public class BitacoraSistemaBLL
    {
        private readonly BitacoraSistemaDAO _dao = new();

        public void Registrar(
            Usuario? usuario,
            string modulo,
            string accion,
            string detalle)
        {
            try
            {
                _dao.Registrar(new BitacoraSistema
                {
                    Fecha = DateTime.Now,
                    IdUsuario = usuario?.IdUsuario ?? 0,
                    Usuario = usuario?.Nombre ?? "Sistema",
                    Modulo = modulo,
                    Accion = accion,
                    Detalle = detalle
                });
            }
            catch
            {
                // La bitacora no debe bloquear la operacion principal.
            }
        }

        public List<BitacoraSistema> ObtenerDelDia(DateTime fecha)
        {
            DateTime desde = fecha.Date;
            DateTime hasta = desde.AddDays(1);

            return _dao.ObtenerPorRango(desde, hasta);
        }

        public List<BitacoraSistema> ObtenerUltimosDias(int dias)
        {
            DateTime desde = DateTime.Today.AddDays(-Math.Max(0, dias - 1));
            DateTime hasta = DateTime.Today.AddDays(1);

            return _dao.ObtenerPorRango(desde, hasta);
        }
    }
}
