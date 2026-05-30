namespace MathAdminApp.Modelos
{
    public class BitacoraSistema
    {
        public int IdBitacora { get; set; }

        public DateTime Fecha { get; set; }

        public int IdUsuario { get; set; }

        public string Usuario { get; set; } = string.Empty;

        public string Modulo { get; set; } = string.Empty;

        public string Accion { get; set; } = string.Empty;

        public string Detalle { get; set; } = string.Empty;
    }
}
