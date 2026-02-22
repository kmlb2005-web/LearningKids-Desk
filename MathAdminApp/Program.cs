// ============================================================
// MathAdminApp - Aplicacion Administrativa de Matematicas
// Para alumnos de 6to de primaria
// Punto de entrada de la aplicacion
// ============================================================

namespace MathAdminApp
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal de la aplicacion.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();
            Application.Run(new Presentacion.FormLogin());
        }
    }
}
