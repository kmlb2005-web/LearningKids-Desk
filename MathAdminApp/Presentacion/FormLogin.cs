// ============================================================
// Presentacion: FormLogin
// Diseño moderno estilo Learning Kids
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;
using System.IO;

namespace MathAdminApp.Presentacion
{
    public class FormLogin : Form
    {
        // =========================================================
        // CONTROLES
        // =========================================================

        private Label lblTitulo = null!;
        private Label lblSubtitulo = null!;
        private Label lblUsuario = null!;
        private Label lblContrasena = null!;

        private TextBox txtUsuario = null!;
        private TextBox txtContrasena = null!;

        private Button btnIngresar = null!;

        private Panel panelIzquierdo = null!;
        private Panel panelDerecho = null!;

        // =========================================================
        // CONSTRUCTOR
        // =========================================================

        public FormLogin()
        {
            InicializarComponentes();
        }

        // =========================================================
        // DISEÑO DEL LOGIN
        // =========================================================

        private void InicializarComponentes()
        {
            // =====================================================
            // FORMULARIO PRINCIPAL
            // =====================================================

            this.Text = "LearningKids - Inicio de Sesión";
            this.Size = new Size(1100, 650);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Fondo principal
            this.BackColor = Color.FromArgb(240, 248, 255);

            // =====================================================
            // PANEL IZQUIERDO
            // =====================================================

            panelIzquierdo = new Panel
            {
                Dock = DockStyle.Left,
                Width = 450,
                BackColor = Color.FromArgb(223, 242, 255)
            };

            // =====================================================
            // LOGO LEARNING KIDS
            // =====================================================

            PictureBox picLogo = new PictureBox
            {
                Image = Image.FromFile(
                    Path.Combine(Application.StartupPath, "Resources", "Logo.png")
                ),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(300, 120),
                Location = new Point(70, 25),
                BackColor = Color.Transparent
            };

            // =====================================================
            // NUBE SUPERIOR
            // =====================================================

            PictureBox nube1 = new PictureBox
            {
                Image = Image.FromFile(@"Resources/nube1.png"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(140, 80),
                Location = new Point(10, 130),
                BackColor = Color.Transparent
            };

            // =====================================================
            // ROBOT PRINCIPAL
            // =====================================================

            PictureBox robot = new PictureBox
            {
                Image = Image.FromFile(@"Resources/Louz.png"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(320, 320),
                Location = new Point(60, 170),
                BackColor = Color.Transparent
            };

            // =====================================================
            // ESTRELLA
            // =====================================================

            PictureBox estrella = new PictureBox
            {
                Image = Image.FromFile(@"Resources/estrella.png"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(70, 70),
                Location = new Point(340, 120),
                BackColor = Color.Transparent
            };

            // =====================================================
            // NUMEROS Y ABC
            // =====================================================

            PictureBox numeros = new PictureBox
            {
                Image = Image.FromFile(@"Resources/numeros.png"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(190, 70),
                Location = new Point(120, 500),
                BackColor = Color.Transparent
            };

            // =====================================================
            // NUBE INFERIOR
            // =====================================================

            PictureBox nube2 = new PictureBox
            {
                Image = Image.FromFile(@"Resources/nube2.png"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Size = new Size(180, 90),
                Location = new Point(250, 540),
                BackColor = Color.Transparent
            };

            // =====================================================
            // AGREGAR CONTROLES PANEL IZQUIERDO
            // =====================================================

            panelIzquierdo.Controls.Add(picLogo);
            panelIzquierdo.Controls.Add(nube1);
            panelIzquierdo.Controls.Add(robot);
            panelIzquierdo.Controls.Add(estrella);
            panelIzquierdo.Controls.Add(numeros);
            panelIzquierdo.Controls.Add(nube2);

            // =====================================================
            // PANEL DERECHO
            // =====================================================

            panelDerecho = new Panel
            {
                Size = new Size(500, 520),
                BackColor = Color.White,
                Location = new Point(520, 55)
            };

            // =====================================================
            // TITULO
            // =====================================================

            lblTitulo = new Label
            {
                Text = "¡Bienvenido!",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(40, 50, 90),

                Width = panelDerecho.Width,
                Height = 50,

                TextAlign = ContentAlignment.MiddleCenter,

                Location = new Point(0, 50)
            };

            // =====================================================
            // SUBTITULO
            // =====================================================

            lblSubtitulo = new Label
            {
                Text = "Inicia sesión para continuar 💙",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(120, 120, 140),

                Width = panelDerecho.Width,
                Height = 30,

                TextAlign = ContentAlignment.MiddleCenter,

                Location = new Point(0, 100)
            };

            // =====================================================
            // LABEL USUARIO
            // =====================================================

            lblUsuario = new Label
            {
                Text = "Usuario",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 80),
                AutoSize = true,
                Location = new Point(65, 170)
            };

            // =====================================================
            // TEXTBOX USUARIO
            // =====================================================

            txtUsuario = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(65, 200),
                Size = new Size(350, 40),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(248, 250, 255),
                ForeColor = Color.FromArgb(60, 60, 80)
            };

            // =====================================================
            // LABEL CONTRASEÑA
            // =====================================================

            lblContrasena = new Label
            {
                Text = "Contraseña",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(60, 60, 80),
                AutoSize = true,
                Location = new Point(65, 280)
            };

            // =====================================================
            // TEXTBOX CONTRASEÑA
            // =====================================================

            txtContrasena = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(65, 310),
                Size = new Size(350, 40),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.FromArgb(248, 250, 255),
                ForeColor = Color.FromArgb(60, 60, 80),
                UseSystemPasswordChar = true
            };

            // =====================================================
            // BOTON INGRESAR
            // =====================================================

            btnIngresar = new Button
            {
                Text = "Ingresar",
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                Size = new Size(350, 52),
                Location = new Point(65, 400),

                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,

                BackColor = Color.FromArgb(94, 168, 255),
                ForeColor = Color.White
            };

            btnIngresar.FlatAppearance.BorderSize = 0;

            // Hover moderno
            btnIngresar.MouseEnter += (s, e) =>
            {
                btnIngresar.BackColor = Color.FromArgb(120, 185, 255);
            };

            btnIngresar.MouseLeave += (s, e) =>
            {
                btnIngresar.BackColor = Color.FromArgb(94, 168, 255);
            };

            btnIngresar.Click += BtnIngresar_Click;

            // =====================================================
            // ENTER PARA INGRESAR
            // =====================================================

            txtContrasena.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter)
                {
                    BtnIngresar_Click(s, e);
                }
            };

            // =====================================================
            // AGREGAR CONTROLES PANEL DERECHO
            // =====================================================

            panelDerecho.Controls.Add(lblTitulo);
            panelDerecho.Controls.Add(lblSubtitulo);

            panelDerecho.Controls.Add(lblUsuario);
            panelDerecho.Controls.Add(txtUsuario);

            panelDerecho.Controls.Add(lblContrasena);
            panelDerecho.Controls.Add(txtContrasena);

            panelDerecho.Controls.Add(btnIngresar);

            // =====================================================
            // AGREGAR PANELES AL FORMULARIO
            // =====================================================

            this.Controls.Add(panelDerecho);
            this.Controls.Add(panelIzquierdo);

            // =====================================================
            // FOCO INICIAL
            // =====================================================

            this.ActiveControl = txtUsuario;
        }

        // =========================================================
        // LOGIN
        // =========================================================

        // =========================================================
        // LOGIN
        // =========================================================

        private void BtnIngresar_Click(object? sender, EventArgs e)
        {
            try
            {
                UsuarioBLL bll = new UsuarioBLL();

                Usuario? usuario = bll.IniciarSesion(
                    txtUsuario.Text,
                    txtContrasena.Text
                );

                // =============================================
                // VALIDAR USUARIO
                // =============================================

                if (usuario == null)
                {
                    MessageBox.Show(
                        "Usuario o contraseña incorrectos.",
                        "Error de acceso",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    txtContrasena.Clear();
                    txtUsuario.Focus();

                    return;
                }

                // =============================================
                // VALIDAR ROL
                // =============================================
                // Ajusta los IDs según tu BD:
                // 1 = Administrador
                // 2 = Docente
                // 3 = Alumno

                if (usuario.IdRol == 3)
                {
                    MessageBox.Show(
                        "Solo Administradores y Docentes pueden acceder.",
                        "Acceso denegado",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning
                    );

                    return;
                }

                // =============================================
                // ABRIR DASHBOARD
                // =============================================

                this.Hide();

                FormDashboard dashboard =
                    new FormDashboard(usuario);

                dashboard.FormClosed +=
                    (s, args) => this.Close();

                dashboard.Show();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}