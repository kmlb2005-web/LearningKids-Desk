// ============================================================
// Presentacion: FormLogin
// Pantalla de inicio de sesion del administrador
// ============================================================

using MathAdminApp.LogicaNegocio;

namespace MathAdminApp.Presentacion
{
    /// <summary>
    /// Formulario de inicio de sesion.
    /// Valida credenciales y permite acceso al sistema.
    /// </summary>
    public class FormLogin : Form
    {
        // --- Controles de la interfaz ---
        private PictureBox picLogo = null!;
        private Label lblTitulo = null!;
        private Label lblSubtitulo = null!;
        private Label lblUsuario = null!;
        private Label lblContrasena = null!;
        private TextBox txtUsuario = null!;
        private TextBox txtContrasena = null!;
        private Button btnIngresar = null!;
        private Panel panelIzquierdo = null!;
        private Panel panelDerecho = null!;
        private Label lblBienvenida = null!;

        public FormLogin()
        {
            InicializarComponentes();
        }

        /// <summary>
        /// Configura todos los controles visuales del formulario.
        /// </summary>
        private void InicializarComponentes()
        {
            // --- Configuracion del formulario ---
            this.Text = "LearningKids - Inicio de Sesion";
            this.Size = new Size(800, 480);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.BackColor = Color.Black;

            // --- Panel izquierdo decorativo (amarillo pastel) ---
            panelIzquierdo = new Panel
            {
                Dock = DockStyle.Left,
                Width = 320,
                BackColor = Color.FromArgb(255, 249, 196) // Amarillo pastel suave
            };

            PictureBox picBienvenida = new PictureBox
            {
                Image = Properties.Resources.LearningKidsLogo,
                SizeMode = PictureBoxSizeMode.Zoom,
                Dock = DockStyle.Fill
            };
            panelIzquierdo.Controls.Add(picBienvenida);

            // --- Panel derecho (formulario de login) ---
            panelDerecho = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(50, 60, 50, 40)
            };

            lblTitulo = new Label
            {
                Text = "Iniciar Sesion",
                Font = new Font("Segoe UI", 20, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                AutoSize = true,
                Location = new Point(50, 60)
            };

            lblSubtitulo = new Label
            {
                Text = "Ingrese sus datos de administrador",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(130, 130, 130),
                AutoSize = true,
                Location = new Point(50, 100)
            };

            lblUsuario = new Label
            {
                Text = "Usuario",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(50, 150)
            };

            txtUsuario = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(50, 175),
                Size = new Size(360, 30),
                BorderStyle = BorderStyle.FixedSingle
            };

            lblContrasena = new Label
            {
                Text = "Contraseña",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(50, 220)
            };

            txtContrasena = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(50, 245),
                Size = new Size(360, 30),
                BorderStyle = BorderStyle.FixedSingle,
                UseSystemPasswordChar = true
            };

            btnIngresar = new Button
            {
                Text = "Ingresar",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 235, 59), // Amarillo más intenso
                ForeColor = Color.Black, // Negro para que contraste mejor con amarillo
                FlatStyle = FlatStyle.Flat,
                Size = new Size(360, 45),
                Location = new Point(50, 310),
                Cursor = Cursors.Hand
            };
            btnIngresar.FlatAppearance.BorderSize = 0;
            btnIngresar.Click += BtnIngresar_Click;

            // Permitir Enter para ingresar
            txtContrasena.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter) BtnIngresar_Click(s, e);
            };

            // Agregar controles al panel derecho
            panelDerecho.Controls.Add(lblTitulo);
            panelDerecho.Controls.Add(lblSubtitulo);
            panelDerecho.Controls.Add(lblUsuario);
            panelDerecho.Controls.Add(txtUsuario);
            panelDerecho.Controls.Add(lblContrasena);
            panelDerecho.Controls.Add(txtContrasena);
            panelDerecho.Controls.Add(btnIngresar);

            // Agregar paneles al formulario
            this.Controls.Add(panelDerecho);
            this.Controls.Add(panelIzquierdo);

            // Foco inicial
            this.ActiveControl = txtUsuario;
        }

        /// <summary>
        /// Evento del boton "Ingresar".
        /// Valida credenciales y abre el Dashboard si son correctas.
        /// </summary>
        private void BtnIngresar_Click(object? sender, EventArgs e)
        {
            try
            {
                var bll = new UsuarioBLL();
                var usuario = bll.IniciarSesion(txtUsuario.Text, txtContrasena.Text);

                if (usuario == null)
                {
                    MessageBox.Show("Usuario o contrasena incorrectos.",
                        "Error de acceso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (usuario.Rol != "Administrador")
                {
                    MessageBox.Show("Solo los administradores pueden acceder a esta aplicacion.",
                        "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Abrir el Dashboard y ocultar el login
                this.Hide();
                var dashboard = new FormDashboard(usuario);
                dashboard.FormClosed += (s, args) => this.Close();
                dashboard.Show();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validacion", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con la base de datos:\n{ex.Message}",
                    "Error de conexion", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
