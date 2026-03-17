using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;
using System.Drawing.Drawing2D;

namespace MathAdminApp.Presentacion
{
    public class FormDashboard : Form
    {
        private Panel panelMenu = null!;
        private Panel panelContenido = null!;
        private Panel panelSuperior = null!;
        private Label lblTituloPagina = null!;
        private Label lblUsuarioActual = null!;

        private Button btnDashboard = null!;
        private Button btnUsuarios = null!;
        private Button btnUnidades = null!;
        private Button btnExamenes = null!;
        private Button btnPreguntas = null!;
        private Button btnResultados = null!;
        private Button btnCerrarSesion = null!;

        private Panel panelEstadisticas = null!;

        private readonly Usuario _usuarioActual;

        public FormDashboard(Usuario usuario)
        {
            _usuarioActual = usuario;
            InicializarComponentes();
            MostrarDashboard();
        }

        private void InicializarComponentes()
        {
            this.Text = "LearningsKids - Panel de Administracion";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(900, 600);
            this.BackColor = Color.FromArgb(240, 242, 245);

            // PANEL SUPERIOR
            panelSuperior = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White
            };

            lblTituloPagina = new Label
            {
                Text = "Learning Kids Admin",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                Location = new Point(230, 15),
                AutoSize = true
            };

            lblUsuarioActual = new Label
            {
                Text = $"⚙ Administrador: {_usuarioActual.Nombre}",
                Font = new Font("Segoe UI", 10),
                AutoSize = true,
                Location = new Point(820, 20)
            };

            panelSuperior.Controls.Add(lblTituloPagina);
            panelSuperior.Controls.Add(lblUsuarioActual);

            // MENU LATERAL
            panelMenu = new Panel
            {
                Dock = DockStyle.Left,
                Width = 220,
                BackColor = Color.FromArgb(235, 235, 235),
                Padding = new Padding(0, 20, 0, 20)
            };

            var lblLogo = new Label
            {
                Text = "🏫 Learning Kids",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60
            };

            var lblRol = new Label
            {
                Text = "⚙ Administrador",
                Font = new Font("Segoe UI", 9),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 25
            };

            btnDashboard = CrearBotonMenu("🏠   Inicio");
            btnUsuarios = CrearBotonMenu("👥   Usuarios");
            btnUnidades = CrearBotonMenu("📚   Temas");
            btnExamenes = CrearBotonMenu("📝   Examenes");
            btnPreguntas = CrearBotonMenu("❓   Preguntas");
            btnResultados = CrearBotonMenu("📊   Resultados");
            btnCerrarSesion = CrearBotonMenu("🚪   Cerrar Sesion");

            btnDashboard.Click += (s, e) =>
            {
                ActivarBoton(btnDashboard);
                MostrarDashboard();
                ActualizarTitulo("Learning Kids Admin");
            };

            btnUsuarios.Click += (s, e) =>
            {
                ActivarBoton(btnUsuarios);
                MostrarUsuarios();
                ActualizarTitulo("Gestion de Usuarios");
            };

            btnUnidades.Click += (s, e) =>
            {
                ActivarBoton(btnUnidades);
                MostrarUnidades();
                ActualizarTitulo("Gestion de Unidades");
            };

            btnExamenes.Click += (s, e) =>
            {
                ActivarBoton(btnExamenes);
                MostrarExamenes();
                ActualizarTitulo("Gestion de Examenes");
            };

            btnPreguntas.Click += (s, e) =>
            {
                ActivarBoton(btnPreguntas);
                MostrarPreguntas();
                ActualizarTitulo("Gestion de Preguntas");
            };

            btnResultados.Click += (s, e) =>
            {
                ActivarBoton(btnResultados);
                MostrarResultados();
                ActualizarTitulo("Resultados y Avances");
            };

            btnCerrarSesion.Click += BtnCerrarSesion_Click;

            var panelBotones = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true
            };

            panelBotones.Controls.Add(btnDashboard);
            panelBotones.Controls.Add(btnUsuarios);
            panelBotones.Controls.Add(btnUnidades);
            panelBotones.Controls.Add(btnExamenes);
            panelBotones.Controls.Add(btnPreguntas);
            panelBotones.Controls.Add(btnResultados);

            btnCerrarSesion.Dock = DockStyle.Bottom;

            panelMenu.Controls.Add(btnCerrarSesion);
            panelMenu.Controls.Add(panelBotones);
            panelMenu.Controls.Add(lblRol);
            panelMenu.Controls.Add(lblLogo);

            panelContenido = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20),
                BackColor = Color.FromArgb(240, 242, 245)
            };

            this.Controls.Add(panelContenido);
            this.Controls.Add(panelSuperior);
            this.Controls.Add(panelMenu);
        }
        private Button CrearBotonMenu(string texto)
        {
            var btn = new Button
            {
                Text = texto,
                Font = new Font("Segoe UI Emoji", 11),
                Size = new Size(190, 45),
                TextAlign = ContentAlignment.MiddleLeft,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.FromArgb(245, 245, 245),
                ForeColor = Color.FromArgb(60, 60, 60),
                Margin = new Padding(10, 6, 10, 6),
                Padding = new Padding(10, 0, 0, 0),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(220, 220, 220);

            btn.Paint += (s, e) => RedondearControl(btn, 20);

            return btn;
        }

        private void ActivarBoton(Button boton)
        {
            foreach (Control ctrl in panelMenu.Controls)
            {
                if (ctrl is FlowLayoutPanel panel)
                {
                    foreach (Control c in panel.Controls)
                    {
                        if (c is Button b)
                        {
                            b.BackColor = Color.Transparent;
                            b.ForeColor = Color.FromArgb(70, 70, 70);
                        }
                    }
                }
            }

            boton.BackColor = Color.FromArgb(235, 220, 190);
            boton.ForeColor = Color.FromArgb(120, 90, 0);
        }
        private void RedondearControl(Control control, int radio)
        {
            GraphicsPath path = new GraphicsPath();

            path.StartFigure();
            path.AddArc(new Rectangle(0, 0, radio, radio), 180, 90);
            path.AddArc(new Rectangle(control.Width - radio, 0, radio, radio), 270, 90);
            path.AddArc(new Rectangle(control.Width - radio, control.Height - radio, radio, radio), 0, 90);
            path.AddArc(new Rectangle(0, control.Height - radio, radio, radio), 90, 90);
            path.CloseFigure();

            control.Region = new Region(path);
        }

        private void ActualizarTitulo(string titulo)
        {
            lblTituloPagina.Text = titulo;
        }

        private void LimpiarContenido()
        {
            panelContenido.Controls.Clear();
        }

        private void MostrarDashboard()
        {
            LimpiarContenido();

            panelEstadisticas = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150
            };

            var tarjetaAlumnos = CrearTarjetaEstadistica("Total Alumnos", "3",
                Color.FromArgb(255, 179, 0), new Point(0, 0));

            var tarjetaUnidades = CrearTarjetaEstadistica("Total Unidades", "5",
                Color.FromArgb(255, 202, 40), new Point(250, 0));

            var tarjetaExamenes = CrearTarjetaEstadistica("Examenes Creados", "0",
                Color.FromArgb(255, 235, 59), new Point(500, 0));

            panelEstadisticas.Controls.Add(tarjetaAlumnos);
            panelEstadisticas.Controls.Add(tarjetaUnidades);
            panelEstadisticas.Controls.Add(tarjetaExamenes);

            var lblBienvenida = new Label
            {
                Text = $"Bienvenido(a), {_usuarioActual.Nombre}.\nDesde aqui puedes gestionar usuarios, unidades, examenes y mas.",
                Font = new Font("Segoe UI", 12),
                AutoSize = true,
                Location = new Point(0, 170)
            };

            panelContenido.Controls.Add(lblBienvenida);
            panelContenido.Controls.Add(panelEstadisticas);
        }

        private Panel CrearTarjetaEstadistica(string titulo, string valor, Color color, Point ubicacion)
        {
            var panel = new Panel
            {
                Size = new Size(230, 120),
                Location = ubicacion,
                BackColor = Color.White
            };

            var barra = new Panel
            {
                Dock = DockStyle.Top,
                Height = 4,
                BackColor = color
            };

            var lblTitulo = new Label
            {
                Text = titulo,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.Gray,
                Location = new Point(15, 20),
                AutoSize = true
            };

            var lblValor = new Label
            {
                Text = valor,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = color,
                Location = new Point(15, 50),
                AutoSize = true
            };

            panel.Controls.Add(lblValor);
            panel.Controls.Add(lblTitulo);
            panel.Controls.Add(barra);

            return panel;
        }

        private void MostrarUsuarios()
        {
            LimpiarContenido();
            panelContenido.Controls.Add(new ControlUsuarios { Dock = DockStyle.Fill });
        }

        private void MostrarUnidades()
        {
            LimpiarContenido();
            panelContenido.Controls.Add(new ControlUnidades { Dock = DockStyle.Fill });
        }

        private void MostrarExamenes()
        {
            LimpiarContenido();
            panelContenido.Controls.Add(new ControlExamenes { Dock = DockStyle.Fill });
        }

        private void MostrarPreguntas()
        {
            LimpiarContenido();
            panelContenido.Controls.Add(new ControlPreguntas { Dock = DockStyle.Fill });
        }

        private void MostrarResultados()
        {
            LimpiarContenido();
            panelContenido.Controls.Add(new ControlResultados { Dock = DockStyle.Fill });
        }

        private void BtnCerrarSesion_Click(object? sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Desea cerrar sesion?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
                this.Close();
        }
    }
}