// ============================================================
// Presentacion: FormDashboard
// Pantalla principal del administrador con menu de navegacion
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    /// <summary>
    /// Formulario principal del administrador.
    /// Contiene el menu lateral y el area de contenido.
    /// </summary>
    public class FormDashboard : Form
    {
        // --- Controles de la interfaz ---
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

        // Contadores del dashboard
        private Label lblTotalAlumnos = null!;
        private Label lblTotalUnidades = null!;
        private Label lblTotalExamenes = null!;
        private Panel panelEstadisticas = null!;

        private readonly Usuario _usuarioActual;

        public FormDashboard(Usuario usuario)
        {
            _usuarioActual = usuario;
            InicializarComponentes();
            MostrarDashboard();
        }

        /// <summary>
        /// Configura todos los controles del formulario.
        /// </summary>
        private void InicializarComponentes()
        {
            // --- Configuracion del formulario ---
            this.Text = "MathAdmin - Panel de Administracion";
            this.Size = new Size(1100, 700);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.MinimumSize = new Size(900, 600);
            this.BackColor = Color.FromArgb(240, 242, 245);

            // --- Panel superior ---
            panelSuperior = new Panel
            {
                Dock = DockStyle.Top,
                Height = 60,
                BackColor = Color.White,
                Padding = new Padding(20, 0, 20, 0)
            };

            lblTituloPagina = new Label
            {
                Text = "Dashboard",
                Font = new Font("Segoe UI", 16, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                AutoSize = true,
                Location = new Point(230, 15)
            };

            lblUsuarioActual = new Label
            {
                Text = $"Administrador: {_usuarioActual.Nombre}",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(100, 100, 100),
                AutoSize = true,
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                Location = new Point(800, 20)
            };

            panelSuperior.Controls.Add(lblTituloPagina);
            panelSuperior.Controls.Add(lblUsuarioActual);

            // --- Panel menu lateral ---
            panelMenu = new Panel
            {
                Dock = DockStyle.Left,
                Width = 210,
                BackColor = Color.FromArgb(47, 54, 82), // Azul oscuro
                Padding = new Padding(0, 10, 0, 10)
            };

            // Logo / Titulo del menu
            var lblLogo = new Label
            {
                Text = "MathAdmin",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 50,
                Padding = new Padding(0, 10, 0, 0)
            };

            var lblVersion = new Label
            {
                Text = "v1.0 - Administrador",
                Font = new Font("Segoe UI", 8),
                ForeColor = Color.FromArgb(150, 160, 180),
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 25
            };

            // Botones del menu
            btnDashboard = CrearBotonMenu("  Inicio", 0);
            btnUsuarios = CrearBotonMenu("  Usuarios", 1);
            btnUnidades = CrearBotonMenu("  Unidades", 2);
            btnExamenes = CrearBotonMenu("  Examenes", 3);
            btnPreguntas = CrearBotonMenu("  Preguntas", 4);
            btnResultados = CrearBotonMenu("  Resultados", 5);
            btnCerrarSesion = CrearBotonMenu("  Cerrar Sesion", 7);
            btnCerrarSesion.ForeColor = Color.FromArgb(255, 150, 150);

            // Eventos de clic del menu
            btnDashboard.Click += (s, e) => { MostrarDashboard(); ActualizarTitulo("Dashboard"); };
            btnUsuarios.Click += (s, e) => { MostrarUsuarios(); ActualizarTitulo("Gestion de Usuarios"); };
            btnUnidades.Click += (s, e) => { MostrarUnidades(); ActualizarTitulo("Gestion de Unidades"); };
            btnExamenes.Click += (s, e) => { MostrarExamenes(); ActualizarTitulo("Gestion de Examenes"); };
            btnPreguntas.Click += (s, e) => { MostrarPreguntas(); ActualizarTitulo("Gestion de Preguntas"); };
            btnResultados.Click += (s, e) => { MostrarResultados(); ActualizarTitulo("Resultados y Avances"); };
            btnCerrarSesion.Click += BtnCerrarSesion_Click;

            // Agregar botones al menu (orden inverso por Dock = Top)
            panelMenu.Controls.Add(btnCerrarSesion);
            btnCerrarSesion.Dock = DockStyle.Bottom;

            var panelBotones = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                Padding = new Padding(0, 10, 0, 0),
                AutoScroll = true
            };
            panelBotones.Controls.Add(btnDashboard);
            panelBotones.Controls.Add(btnUsuarios);
            panelBotones.Controls.Add(btnUnidades);
            panelBotones.Controls.Add(btnExamenes);
            panelBotones.Controls.Add(btnPreguntas);
            panelBotones.Controls.Add(btnResultados);

            panelMenu.Controls.Add(panelBotones);
            panelMenu.Controls.Add(lblVersion);
            panelMenu.Controls.Add(lblLogo);

            // --- Panel de contenido principal ---
            panelContenido = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(240, 242, 245),
                Padding = new Padding(20)
            };

            // Agregar al formulario
            this.Controls.Add(panelContenido);
            this.Controls.Add(panelSuperior);
            this.Controls.Add(panelMenu);
        }

        /// <summary>
        /// Crea un boton estilizado para el menu lateral.
        /// </summary>
        private Button CrearBotonMenu(string texto, int indice)
        {
            var btn = new Button
            {
                Text = texto,
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(200, 210, 220),
                BackColor = Color.FromArgb(47, 54, 82),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(210, 42),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(15, 0, 0, 0),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            btn.FlatAppearance.MouseOverBackColor = Color.FromArgb(63, 71, 105);

            return btn;
        }

        /// <summary>
        /// Actualiza el titulo de la barra superior.
        /// </summary>
        private void ActualizarTitulo(string titulo)
        {
            lblTituloPagina.Text = titulo;
        }

        /// <summary>
        /// Limpia el panel de contenido para cargar una nueva vista.
        /// </summary>
        private void LimpiarContenido()
        {
            panelContenido.Controls.Clear();
        }

        // ==============================================================
        // VISTA: Dashboard con estadisticas
        // ==============================================================
        private void MostrarDashboard()
        {
            LimpiarContenido();
            ActualizarTitulo("Dashboard");

            panelEstadisticas = new Panel
            {
                Dock = DockStyle.Top,
                Height = 150,
                Padding = new Padding(0, 10, 0, 10)
            };

            // Tarjeta: Total Alumnos
            var tarjetaAlumnos = CrearTarjetaEstadistica("Total Alumnos", "0",
                Color.FromArgb(63, 81, 181), new Point(0, 0));

            // Tarjeta: Total Unidades
            var tarjetaUnidades = CrearTarjetaEstadistica("Total Unidades", "0",
                Color.FromArgb(0, 150, 136), new Point(250, 0));

            // Tarjeta: Examenes Creados
            var tarjetaExamenes = CrearTarjetaEstadistica("Examenes Creados", "0",
                Color.FromArgb(156, 39, 176), new Point(500, 0));

            panelEstadisticas.Controls.Add(tarjetaAlumnos);
            panelEstadisticas.Controls.Add(tarjetaUnidades);
            panelEstadisticas.Controls.Add(tarjetaExamenes);

            // Mensaje de bienvenida
            var lblBienvenida = new Label
            {
                Text = $"Bienvenido(a), {_usuarioActual.Nombre}.\nDesde aqui puedes gestionar usuarios, unidades, examenes y mas.",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(0, 170),
                MaximumSize = new Size(700, 0)
            };

            panelContenido.Controls.Add(lblBienvenida);
            panelContenido.Controls.Add(panelEstadisticas);

            // Cargar estadisticas desde la BD
            CargarEstadisticas(tarjetaAlumnos, tarjetaUnidades, tarjetaExamenes);
        }

        /// <summary>
        /// Crea una tarjeta visual para mostrar un dato estadistico.
        /// </summary>
        private Panel CrearTarjetaEstadistica(string titulo, string valor, Color color, Point ubicacion)
        {
            var panel = new Panel
            {
                Size = new Size(230, 120),
                Location = ubicacion,
                BackColor = Color.White,
                Padding = new Padding(15)
            };

            // Barra de color superior
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
                ForeColor = Color.FromArgb(120, 120, 120),
                AutoSize = true,
                Location = new Point(15, 20)
            };

            var lblValor = new Label
            {
                Text = valor,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = color,
                AutoSize = true,
                Location = new Point(15, 50),
                Tag = titulo // Para identificar la tarjeta al actualizar
            };

            panel.Controls.Add(lblValor);
            panel.Controls.Add(lblTitulo);
            panel.Controls.Add(barra);

            return panel;
        }

        /// <summary>
        /// Carga las estadisticas desde la base de datos.
        /// </summary>
        private void CargarEstadisticas(Panel tarjetaAlumnos, Panel tarjetaUnidades, Panel tarjetaExamenes)
        {
            try
            {
                var usuarioBll = new UsuarioBLL();
                var unidadBll = new UnidadBLL();
                var examenBll = new ExamenBLL();

                // Buscar los labels de valor dentro de cada tarjeta
                foreach (Control ctrl in tarjetaAlumnos.Controls)
                    if (ctrl is Label lbl && lbl.Tag?.ToString() == "Total Alumnos")
                        lbl.Text = usuarioBll.ContarAlumnos().ToString();

                foreach (Control ctrl in tarjetaUnidades.Controls)
                    if (ctrl is Label lbl && lbl.Tag?.ToString() == "Total Unidades")
                        lbl.Text = unidadBll.ContarUnidades().ToString();

                foreach (Control ctrl in tarjetaExamenes.Controls)
                    if (ctrl is Label lbl && lbl.Tag?.ToString() == "Examenes Creados")
                        lbl.Text = examenBll.ContarExamenes().ToString();
            }
            catch
            {
                // Si no hay conexion, mostrar 0
            }
        }

        // ==============================================================
        // VISTA: Gestion de Usuarios
        // ==============================================================
        private void MostrarUsuarios()
        {
            LimpiarContenido();

            var controlUsuarios = new ControlUsuarios
            {
                Dock = DockStyle.Fill
            };
            panelContenido.Controls.Add(controlUsuarios);
        }

        // ==============================================================
        // VISTA: Gestion de Unidades
        // ==============================================================
        private void MostrarUnidades()
        {
            LimpiarContenido();

            var controlUnidades = new ControlUnidades
            {
                Dock = DockStyle.Fill
            };
            panelContenido.Controls.Add(controlUnidades);
        }

        // ==============================================================
        // VISTA: Gestion de Examenes
        // ==============================================================
        private void MostrarExamenes()
        {
            LimpiarContenido();

            var controlExamenes = new ControlExamenes
            {
                Dock = DockStyle.Fill
            };
            panelContenido.Controls.Add(controlExamenes);
        }

        // ==============================================================
        // VISTA: Gestion de Preguntas
        // ==============================================================
        private void MostrarPreguntas()
        {
            LimpiarContenido();

            var controlPreguntas = new ControlPreguntas
            {
                Dock = DockStyle.Fill
            };
            panelContenido.Controls.Add(controlPreguntas);
        }

        // ==============================================================
        // VISTA: Resultados y Avances
        // ==============================================================
        private void MostrarResultados()
        {
            LimpiarContenido();

            var controlResultados = new ControlResultados
            {
                Dock = DockStyle.Fill
            };
            panelContenido.Controls.Add(controlResultados);
        }

        /// <summary>
        /// Cierra sesion y vuelve al formulario de login.
        /// </summary>
        private void BtnCerrarSesion_Click(object? sender, EventArgs e)
        {
            var resultado = MessageBox.Show("Desea cerrar sesion?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                this.Close();
            }
        }
    }
}
