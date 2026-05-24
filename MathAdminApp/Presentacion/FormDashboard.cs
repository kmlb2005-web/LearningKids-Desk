// ============================================================
// FORM DASHBOARD MODERNO - LEARNING KIDS
// ============================================================

using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class FormDashboard : Form
    {
        // =====================================================
        // CONTROLES
        // =====================================================

        private Panel panelMenu = null!;
        private Panel panelContenido = null!;
        private Panel panelSuperior = null!;

        private Label lblTituloPagina = null!;

        private Button btnDashboard = null!;
        private Button btnUsuarios = null!;
        private Button btnCampos = null!;
        private Button btnProyectos = null!;
        private Button btnTemas = null!;
        private Button btnExamenes = null!;
        private Button btnPreguntas = null!;
        private Button btnResultados = null!;
        private Button btnCerrarSesion = null!;

        private readonly Usuario _usuarioActual;

        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public FormDashboard(Usuario usuario)
        {
            _usuarioActual = usuario;

            InicializarComponentes();

            MostrarDashboard();
        }

        // =====================================================
        // DISEÑO GENERAL
        // =====================================================

        private void InicializarComponentes()
        {
            // =================================================
            // FORM
            // =================================================

            this.Text =
                "LearningKids - Panel de Administración";

            this.WindowState =
                FormWindowState.Maximized;

            this.BackColor =
                Color.FromArgb(245, 250, 255);

            this.MinimumSize =
                new Size(1400, 850);

            // =================================================
            // MENU
            // =================================================

            panelMenu = new Panel
            {
                Dock = DockStyle.Left,

                Width = 300,

                BackColor =
                    Color.FromArgb(223, 242, 255)
            };

            // =================================================
            // LOGO
            // =================================================

            PictureBox picLogo = new PictureBox
            {
                Image =
                    Image.FromFile("Resources/Logo.png"),

                SizeMode =
                    PictureBoxSizeMode.Zoom,

                Size =
                    new Size(220, 100),

                Location =
                    new Point(35, 30),

                BackColor =
                    Color.Transparent
            };

            // =================================================
            // LABEL ADMIN
            // =================================================

            Label lblAdmin = new Label
            {
                Text = "🛡️ Administrador",

                Font =
                    new Font("Segoe UI", 11),

                ForeColor =
                    Color.FromArgb(70, 90, 120),

                AutoSize = true,

                Location =
                    new Point(70, 150)
            };

            // =================================================
            // BOTONES MENU
            // =================================================

            btnDashboard =
                CrearBotonMenu(
                    "🏠   Inicio",
                    220
                );

            btnUsuarios =
                CrearBotonMenu(
                    "👥   Usuarios",
                    290
                );

            btnCampos =
                CrearBotonMenu(
                    "📖   Campos",
                    360
                );

            btnProyectos =
                CrearBotonMenu(
                    "📂   Proyectos",
                    430
                );

            btnTemas =
                CrearBotonMenu(
                    "📚   Temas",
                    500
                );

            btnExamenes =
                CrearBotonMenu(
                    "📝   Pruebas",
                    570
                );

            btnPreguntas =
                CrearBotonMenu(
                    "❓   Preguntas",
                    640
                );

            btnResultados =
                CrearBotonMenu(
                    "📊   Resultados",
                    710
                );

            // =================================================
            // ROBOT
            // =================================================

            PictureBox robot = new PictureBox
            {
                Image =
                    Image.FromFile("Resources/Louz.png"),

                SizeMode =
                    PictureBoxSizeMode.Zoom,

                Size =
                    new Size(170, 170),

                Location =
                    new Point(60, 760),

                BackColor =
                    Color.Transparent
            };

            // =================================================
            // BOTON CERRAR
            // =================================================

            btnCerrarSesion = new Button
            {
                Text = "↩  Cerrar Sesión",

                Font =
                    new Font(
                        "Segoe UI",
                        11,
                        FontStyle.Bold
                    ),

                ForeColor =
                    Color.FromArgb(255, 80, 120),

                BackColor =
                    Color.White,

                FlatStyle =
                    FlatStyle.Flat,

                Size =
                    new Size(220, 55),

                Location =
                    new Point(35, 940),

                Cursor =
                    Cursors.Hand
            };

            btnCerrarSesion.FlatAppearance.BorderSize = 0;

            btnCerrarSesion.Click +=
                BtnCerrarSesion_Click;

            // =================================================
            // EVENTOS
            // =================================================

            btnDashboard.Click +=
                (s, e) => MostrarDashboard();

            btnUsuarios.Click +=
                (s, e) => MostrarUsuarios();

            btnCampos.Click +=
                (s, e) => MostrarCampos();

            btnProyectos.Click +=
                (s, e) => MostrarProyectos();

            btnTemas.Click +=
                (s, e) => MostrarTemas();

            btnExamenes.Click +=
                (s, e) => MostrarExamenes();

            btnPreguntas.Click +=
                (s, e) => MostrarPreguntas();

            btnResultados.Click +=
                (s, e) => MostrarResultados();

            // =================================================
            // AGREGAR MENU
            // =================================================

            panelMenu.Controls.Add(picLogo);

            panelMenu.Controls.Add(lblAdmin);

            panelMenu.Controls.Add(btnDashboard);

            panelMenu.Controls.Add(btnUsuarios);

            panelMenu.Controls.Add(btnCampos);

            panelMenu.Controls.Add(btnProyectos);

            panelMenu.Controls.Add(btnTemas);

            panelMenu.Controls.Add(btnExamenes);

            panelMenu.Controls.Add(btnPreguntas);

            panelMenu.Controls.Add(btnResultados);

            panelMenu.Controls.Add(robot);

            panelMenu.Controls.Add(btnCerrarSesion);

            // =================================================
            // PANEL SUPERIOR
            // =================================================

            panelSuperior = new Panel
            {
                Dock = DockStyle.Top,

                Height = 120,

                BackColor =
                    Color.FromArgb(245, 250, 255)
            };

            lblTituloPagina = new Label
            {
                Text =
                    $"¡Hola, {_usuarioActual.Nombre}! 👋",

                Font =
                    new Font(
                        "Segoe UI",
                        28,
                        FontStyle.Bold
                    ),

                ForeColor =
                    Color.FromArgb(20, 35, 80),

                AutoSize = true,

                Location =
                    new Point(60, 25)
            };

            Label lblSubtitulo = new Label
            {
                Text =
                    "Gestiona el aprendizaje de los niños desde aquí ✨",

                Font =
                    new Font("Segoe UI", 14),

                ForeColor =
                    Color.FromArgb(100, 120, 150),

                AutoSize = true,

                Location =
                    new Point(65, 75)
            };

            panelSuperior.Controls.Add(lblTituloPagina);

            panelSuperior.Controls.Add(lblSubtitulo);

            // =================================================
            // PANEL CONTENIDO
            // =================================================

            panelContenido = new Panel
            {
                Dock = DockStyle.Fill,

                BackColor =
                    Color.FromArgb(245, 250, 255),

                Padding =
                    new Padding(40, 20, 40, 20)
            };

            // =================================================
            // AGREGAR FORM
            // =================================================

            this.Controls.Add(panelContenido);

            this.Controls.Add(panelSuperior);

            this.Controls.Add(panelMenu);
        }

        // =====================================================
        // BOTONES MENU
        // =====================================================

        private Button CrearBotonMenu(
            string texto,
            int y
        )
        {
            Button btn = new Button
            {
                Text = texto,

                Font =
                    new Font(
                        "Segoe UI",
                        13,
                        FontStyle.Bold
                    ),

                ForeColor =
                    Color.FromArgb(30, 50, 90),

                BackColor =
                    Color.White,

                FlatStyle =
                    FlatStyle.Flat,

                Size =
                    new Size(250, 55),

                Location =
                    new Point(25, y),

                Cursor =
                    Cursors.Hand,

                TextAlign =
                    ContentAlignment.MiddleLeft
            };

            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor =
                    Color.FromArgb(94, 168, 255);

                btn.ForeColor =
                    Color.White;
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor =
                    Color.White;

                btn.ForeColor =
                    Color.FromArgb(30, 50, 90);
            };

            return btn;
        }

        // =====================================================
        // DASHBOARD
        // =====================================================

        private void MostrarDashboard()
        {
            panelContenido.Controls.Clear();

            Panel card1 = CrearTarjeta(
                "👦 Total Alumnos",
                "120",
                Color.FromArgb(66, 133, 244),
                new Point(20, 20)
            );

            Panel card2 = CrearTarjeta(
                "📖 Campos",
                "4",
                Color.FromArgb(155, 89, 182),
                new Point(420, 20)
            );

            Panel card3 = CrearTarjeta(
                "📂 Proyectos",
                "12",
                Color.FromArgb(52, 199, 89),
                new Point(820, 20)
            );

            Panel card4 = CrearTarjeta(
                "📚 Temas",
                "30",
                Color.FromArgb(255, 159, 67),
                new Point(20, 250)
            );

            Panel card5 = CrearTarjeta(
                "📝 Pruebas",
                "25",
                Color.FromArgb(255, 99, 132),
                new Point(420, 250)
            );

            panelContenido.Controls.Add(card1);

            panelContenido.Controls.Add(card2);

            panelContenido.Controls.Add(card3);

            panelContenido.Controls.Add(card4);

            panelContenido.Controls.Add(card5);
        }

        // =====================================================
        // TARJETAS
        // =====================================================

        private Panel CrearTarjeta(
            string titulo,
            string valor,
            Color color,
            Point ubicacion
        )
        {
            Panel panel = new Panel
            {
                Size =
                    new Size(350, 180),

                Location =
                    ubicacion,

                BackColor =
                    Color.White
            };

            Panel barra = new Panel
            {
                Dock = DockStyle.Top,

                Height = 5,

                BackColor = color
            };

            Label lblTitulo = new Label
            {
                Text = titulo,

                Font =
                    new Font(
                        "Segoe UI",
                        15,
                        FontStyle.Bold
                    ),

                ForeColor =
                    Color.FromArgb(60, 70, 100),

                AutoSize = true,

                Location =
                    new Point(30, 35)
            };

            Label lblValor = new Label
            {
                Text = valor,

                Font =
                    new Font(
                        "Segoe UI",
                        34,
                        FontStyle.Bold
                    ),

                ForeColor = color,

                AutoSize = true,

                Location =
                    new Point(30, 80)
            };

            panel.Controls.Add(barra);

            panel.Controls.Add(lblTitulo);

            panel.Controls.Add(lblValor);

            return panel;
        }

        // =====================================================
        // VISTAS
        // =====================================================

        private void MostrarUsuarios()
        {
            panelContenido.Controls.Clear();

            ControlUsuarios control =
                new ControlUsuarios
                {
                    Dock = DockStyle.Fill
                };

            panelContenido.Controls.Add(control);
        }

        private void MostrarCampos()
        {
            panelContenido.Controls.Clear();

            ControlCampos control =new ControlCampos
                {
                    Dock = DockStyle.Fill
                };

            panelContenido.Controls.Add(control);
        }

        private void MostrarProyectos()
        {
            panelContenido.Controls.Clear();

            ControlProyectos control =
                new ControlProyectos
                {
                    Dock = DockStyle.Fill
                };

            panelContenido.Controls.Add(control);
        }

        private void MostrarTemas()
        {
            panelContenido.Controls.Clear();

            ControlTemas control =
                new ControlTemas
                {
                    Dock = DockStyle.Fill
                };

            panelContenido.Controls.Add(control);
        }

        private void MostrarExamenes()
        {
            panelContenido.Controls.Clear();

            ControlExamenes control =
                new ControlExamenes
                {
                    Dock = DockStyle.Fill
                };

            panelContenido.Controls.Add(control);
        }

        private void MostrarPreguntas()
        {
            panelContenido.Controls.Clear();

            ControlPreguntas control =
                new ControlPreguntas
                {
                    Dock = DockStyle.Fill
                };

            panelContenido.Controls.Add(control);
        }

        private void MostrarResultados()
        {
            panelContenido.Controls.Clear();

            ControlResultados control =
                new ControlResultados
                {
                    Dock = DockStyle.Fill
                };

            panelContenido.Controls.Add(control);
        }

        // =====================================================
        // CERRAR SESION
        // =====================================================

        private void BtnCerrarSesion_Click(
            object? sender,
            EventArgs e
        )
        {
            DialogResult resultado =
                MessageBox.Show(
                    "¿Desea cerrar sesión?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (resultado == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}