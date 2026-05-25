namespace MathAdminApp
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            try
            {
                ApplicationConfiguration.Initialize();

                Application.Run(
                    new Presentacion.FormLogin()
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.ToString(),
                    "ERROR REAL",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}