// ============================================================
// FORM DASHBOARD MODERNO - LEARNING KIDS
// ============================================================

using LiveChartsCore;
using LiveChartsCore.Measure;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.SkiaSharpView.Painting;
using LiveChartsCore.SkiaSharpView.WinForms;
using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;
using SkiaSharp;

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
            // LABEL ADMIN/DOCENTE
            // =================================================

            string rolTexto =_usuarioActual.IdRol == 1? "🛡️ Administrador": "👨‍🏫 Docente";

            Label lblAdmin = new Label
            {
                Text = rolTexto,

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

                Height = 145,

                BackColor =
                    Color.FromArgb(245, 250, 255)
            };

            lblTituloPagina = new Label
            {
                Text =
                    $"¡Hola, {_usuarioActual.Nombre}!",

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
                    new Point(60, 18)
            };

            Label lblSubtitulo = new Label
            {
                Text =
                    "Gestiona el aprendizaje de los niños desde aquí",

                Font =
                    new Font("Segoe UI", 14),

                ForeColor =
                    Color.FromArgb(100, 120, 150),

                AutoSize = true,

                Location =
                    new Point(65, 88)
            };

            panelSuperior.Controls.Add(lblTituloPagina);

            panelSuperior.Controls.Add(lblSubtitulo);

            // =================================================
            // PANEL CONTENIDO
            // =================================================

            panelContenido = new Panel
            {
                Dock = DockStyle.Fill,

                AutoScroll = true,

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

            DashboardDatos datos = ObtenerDatosDashboard();

            if (_usuarioActual.IdRol != 1)
            {
                MostrarDashboardDocente(datos);
                return;
            }

            TableLayoutPanel layout = new()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(20),
                ColumnCount = 3,
                RowCount = 3
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 170));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 53));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 47));

            layout.Controls.Add(
                CrearTarjetaDashboard(
                    "Total alumnos",
                    datos.TotalAlumnos.ToString(),
                    "Usuarios activos para seguimiento",
                    Color.FromArgb(66, 133, 244)),
                0,
                0
            );

            layout.Controls.Add(
                CrearTarjetaDashboard(
                    "Total pruebas",
                    datos.TotalPruebas.ToString(),
                    "Evaluaciones registradas",
                    Color.FromArgb(255, 99, 132)),
                1,
                0
            );

            layout.Controls.Add(
                CrearTarjetaDashboard(
                    "Proyectos activos",
                    datos.TotalProyectos.ToString(),
                    "Proyectos disponibles",
                    Color.FromArgb(52, 199, 89)),
                2,
                0
            );

            Panel panelRendimiento = CrearPanelGrafica(
                "Grafica rendimiento",
                CrearGraficaRendimiento(datos.Resultados)
            );

            layout.Controls.Add(panelRendimiento, 0, 1);
            layout.SetColumnSpan(panelRendimiento, 2);

            layout.Controls.Add(
                CrearPanelGrafica(
                    "Proyectos por grado",
                    CrearGraficaProyectosPorGrado(datos.Proyectos)),
                2,
                1
            );

            layout.Controls.Add(
                CrearPanelGrafica(
                    "Pruebas por tema",
                    CrearGraficaPruebasPorTema(datos.Pruebas, datos.Temas)),
                0,
                2
            );

            Panel panelDistribucion = CrearPanelGrafica(
                "Distribucion general",
                CrearGraficaDistribucion(datos)
            );

            layout.Controls.Add(panelDistribucion, 1, 2);
            layout.SetColumnSpan(panelDistribucion, 2);

            panelContenido.Controls.Add(layout);
        }

        private DashboardDatos ObtenerDatosDashboard()
        {
            UsuarioBLL usuarioBLL = new();
            ProyectoBLL proyectoBLL = new();
            PruebaBLL pruebaBLL = new();
            TemaBLL temaBLL = new();
            ResultadoBLL resultadoBLL = new();

            List<Usuario> alumnos = _usuarioActual.IdRol == 1
                ? usuarioBLL.ObtenerAlumnos()
                : usuarioBLL.ObtenerAlumnosPorDocente(_usuarioActual.IdUsuario);

            List<Proyecto> proyectos =
                proyectoBLL.ObtenerVisibles(_usuarioActual);

            List<Prueba> pruebas =
                pruebaBLL.ObtenerVisibles(_usuarioActual);

            List<Tema> temas =
                temaBLL.ObtenerVisibles(_usuarioActual);

            List<Resultado> resultados =
                resultadoBLL.ObtenerVisibles(_usuarioActual);

            return new DashboardDatos(
                alumnos.Count,
                pruebas.Count,
                proyectos.Count,
                alumnos,
                temas,
                pruebas,
                proyectos,
                resultados
            );
        }

        private void MostrarDashboardDocente(DashboardDatos datos)
        {
            decimal promedioGeneral = datos.Resultados.Count == 0
                ? 0
                : datos.Resultados.Average(r => r.Calificacion);

            int alumnosEvaluados = datos.Resultados
                .Select(r => r.IdAlumno)
                .Distinct()
                .Count();

            int pendientes = Math.Max(0, datos.TotalAlumnos - alumnosEvaluados);

            TableLayoutPanel layout = new()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(20),
                ColumnCount = 4,
                RowCount = 3
            };

            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 25));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 170));
            layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 135));
            layout.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            layout.Controls.Add(
                CrearTarjetaDashboard(
                    "Mis alumnos",
                    datos.TotalAlumnos.ToString(),
                    "Alumnos asignados",
                    Color.FromArgb(66, 133, 244)),
                0,
                0
            );

            layout.Controls.Add(
                CrearTarjetaDashboard(
                    "Pruebas",
                    datos.TotalPruebas.ToString(),
                    "Disponibles para aplicar",
                    Color.FromArgb(255, 99, 132)),
                1,
                0
            );

            layout.Controls.Add(
                CrearTarjetaDashboard(
                    "Resultados",
                    datos.Resultados.Count.ToString(),
                    "Registros capturados",
                    Color.FromArgb(22, 160, 133)),
                2,
                0
            );

            layout.Controls.Add(
                CrearTarjetaDashboard(
                    "Promedio",
                    promedioGeneral.ToString("0.0"),
                    "Rendimiento general",
                    Color.FromArgb(155, 89, 182)),
                3,
                0
            );

            Panel resumen = CrearPanelDocenteResumen(
                alumnosEvaluados,
                pendientes,
                datos.TotalPruebas
            );

            layout.Controls.Add(resumen, 0, 1);
            layout.SetColumnSpan(resumen, 4);

            Panel tabla = CrearPanelAlumnosDocente(datos);
            layout.Controls.Add(tabla, 0, 2);
            layout.SetColumnSpan(tabla, 4);

            panelContenido.Controls.Add(layout);
        }

        private Panel CrearPanelDocenteResumen(
            int alumnosEvaluados,
            int pendientes,
            int pruebasDisponibles)
        {
            Panel panel = new()
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                Padding = new Padding(24, 16, 24, 16),
                BackColor = Color.White
            };

            Label lblTitulo = new()
            {
                Text = "Resumen del grupo",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 90),
                AutoSize = false,
                Location = new Point(24, 15),
                Size = new Size(320, 30)
            };

            TableLayoutPanel metricas = new()
            {
                Location = new Point(24, 55),
                Size = new Size(940, 50),
                ColumnCount = 3,
                RowCount = 1
            };

            metricas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            metricas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.33F));
            metricas.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 33.34F));

            metricas.Controls.Add(
                CrearMiniDato("Con resultados", alumnosEvaluados.ToString(), Color.FromArgb(22, 160, 133)),
                0,
                0
            );

            metricas.Controls.Add(
                CrearMiniDato("Pendientes", pendientes.ToString(), Color.FromArgb(255, 159, 67)),
                1,
                0
            );

            metricas.Controls.Add(
                CrearMiniDato("Pruebas disponibles", pruebasDisponibles.ToString(), Color.FromArgb(66, 133, 244)),
                2,
                0
            );

            panel.Controls.Add(metricas);
            panel.Controls.Add(lblTitulo);

            return panel;
        }

        private Panel CrearMiniDato(string titulo, string valor, Color color)
        {
            Panel panel = new()
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(0, 0, 18, 0),
                BackColor = Color.White
            };

            Label lblValor = new()
            {
                Text = valor,
                Font = new Font("Segoe UI", 18, FontStyle.Bold),
                ForeColor = color,
                AutoSize = false,
                Location = new Point(0, 0),
                Size = new Size(70, 40)
            };

            Label lblTitulo = new()
            {
                Text = titulo,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(85, 100, 135),
                AutoSize = false,
                Location = new Point(78, 9),
                Size = new Size(210, 28)
            };

            panel.Controls.Add(lblTitulo);
            panel.Controls.Add(lblValor);

            return panel;
        }

        private Panel CrearPanelAlumnosDocente(DashboardDatos datos)
        {
            Panel panel = new()
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                Padding = new Padding(22, 58, 22, 22),
                BackColor = Color.White
            };

            Label lblTitulo = new()
            {
                Text = "Seguimiento de alumnos",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 90),
                AutoSize = false,
                Location = new Point(22, 18),
                Size = new Size(420, 30)
            };

            DataGridView tabla = new()
            {
                Dock = DockStyle.Fill,
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11),
                BackColor = Color.White,
                ForeColor = Color.FromArgb(20, 35, 80),
                GridColor = Color.FromArgb(230, 235, 245),
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                RowHeadersVisible = false,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ColumnHeadersHeight = 48,
                RowTemplate = { Height = 42 }
            };

            tabla.EnableHeadersVisualStyles = false;
            tabla.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(240, 247, 255);
            tabla.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(20, 35, 90);
            tabla.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 11, FontStyle.Bold);
            tabla.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(220, 235, 255);
            tabla.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(20, 35, 80);

            tabla.Columns.Add("Alumno", "Alumno");
            tabla.Columns.Add("PruebasRealizadas", "Pruebas realizadas");
            tabla.Columns.Add("Promedio", "Promedio");
            tabla.Columns.Add("UltimoResultado", "Ultimo resultado");

            foreach (Usuario alumno in datos.Alumnos.OrderBy(a => a.Nombre))
            {
                List<Resultado> resultadosAlumno = datos.Resultados
                    .Where(r => r.IdAlumno == alumno.IdUsuario)
                    .ToList();

                string promedio = resultadosAlumno.Count == 0
                    ? "Sin datos"
                    : resultadosAlumno.Average(r => r.Calificacion).ToString("0.0");

                string ultimaFecha = resultadosAlumno.Count == 0
                    ? "Pendiente"
                    : resultadosAlumno.Max(r => r.Fecha).ToString("dd/MM/yyyy");

                tabla.Rows.Add(
                    alumno.Nombre,
                    resultadosAlumno.Count,
                    promedio,
                    ultimaFecha
                );
            }

            if (tabla.Rows.Count == 0)
            {
                tabla.Rows.Add(
                    "No hay alumnos asignados",
                    "-",
                    "-",
                    "-"
                );
            }

            panel.Controls.Add(tabla);
            panel.Controls.Add(lblTitulo);

            return panel;
        }

        private Panel CrearTarjetaDashboard(
            string titulo,
            string valor,
            string subtitulo,
            Color color)
        {
            Panel panel = new()
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                Padding = new Padding(0),
                BackColor = Color.White
            };

            Panel barra = new()
            {
                Dock = DockStyle.Top,
                Height = 5,
                BackColor = color
            };

            TableLayoutPanel contenido = new()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(24, 14, 24, 14),
                RowCount = 3,
                ColumnCount = 1
            };

            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 34));
            contenido.RowStyles.Add(new RowStyle(SizeType.Absolute, 58));
            contenido.RowStyles.Add(new RowStyle(SizeType.Percent, 100));

            contenido.Controls.Add(
                new Label
                {
                    Text = titulo,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 13, FontStyle.Bold),
                    ForeColor = Color.FromArgb(45, 58, 96),
                    TextAlign = ContentAlignment.MiddleLeft
                },
                0,
                0
            );

            contenido.Controls.Add(
                new Label
                {
                    Text = valor,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 28, FontStyle.Bold),
                    ForeColor = color,
                    TextAlign = ContentAlignment.MiddleLeft
                },
                0,
                1
            );

            contenido.Controls.Add(
                new Label
                {
                    Text = subtitulo,
                    Dock = DockStyle.Fill,
                    Font = new Font("Segoe UI", 10),
                    ForeColor = Color.FromArgb(105, 118, 150),
                    TextAlign = ContentAlignment.TopLeft
                },
                0,
                2
            );

            panel.Controls.Add(contenido);
            panel.Controls.Add(barra);

            return panel;
        }

        private Panel CrearPanelGrafica(string titulo, Control grafica)
        {
            Panel panel = new()
            {
                Dock = DockStyle.Fill,
                Margin = new Padding(10),
                Padding = new Padding(22, 54, 22, 22),
                BackColor = Color.White
            };

            Label lblTitulo = new()
            {
                Text = titulo,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 90),
                AutoSize = false,
                Location = new Point(22, 16),
                Size = new Size(360, 28)
            };

            grafica.Dock = DockStyle.Fill;

            panel.Controls.Add(grafica);
            panel.Controls.Add(lblTitulo);

            return panel;
        }

        private CartesianChart CrearGraficaRendimiento(List<Resultado> resultados)
        {
            var datos = resultados
                .GroupBy(r => r.Fecha.Date)
                .OrderBy(g => g.Key)
                .TakeLast(7)
                .Select(g => new
                {
                    Fecha = g.Key.ToString("dd/MM"),
                    Promedio = (double)g.Average(r => r.Calificacion)
                })
                .ToList();

            if (datos.Count == 0)
            {
                datos.Add(new { Fecha = "Sin datos", Promedio = 0D });
            }

            return new CartesianChart
            {
                Series = new ISeries[]
                {
                    new LineSeries<double>
                    {
                        Values = datos.Select(d => d.Promedio).ToArray(),
                        Name = "Promedio",
                        GeometrySize = 12,
                        Stroke = new SolidColorPaint(new SKColor(66, 133, 244), 3),
                        Fill = null
                    }
                },
                XAxes = new[]
                {
                    new Axis
                    {
                        Labels = datos.Select(d => d.Fecha).ToArray(),
                        LabelsPaint = new SolidColorPaint(new SKColor(70, 90, 120))
                    }
                },
                YAxes = new[]
                {
                    new Axis
                    {
                        MinLimit = 0,
                        MaxLimit = 10,
                        LabelsPaint = new SolidColorPaint(new SKColor(70, 90, 120))
                    }
                },
                LegendPosition = LegendPosition.Hidden
            };
        }

        private PieChart CrearGraficaProyectosPorGrado(List<Proyecto> proyectos)
        {
            var datos = proyectos
                .GroupBy(p => p.grado)
                .OrderBy(g => g.Key)
                .Select(g => new { Grado = $"Grado {g.Key}", Total = (double)g.Count() })
                .ToList();

            if (datos.Count == 0)
            {
                datos.Add(new { Grado = "Sin datos", Total = 0D });
            }

            return new PieChart
            {
                Series = datos.Select(d =>
                    new PieSeries<double>
                    {
                        Name = d.Grado,
                        Values = new[] { d.Total },
                        DataLabelsSize = 12,
                        DataLabelsPaint = new SolidColorPaint(SKColors.White)
                    }).ToArray(),
                LegendPosition = LegendPosition.Right
            };
        }

        private CartesianChart CrearGraficaPruebasPorTema(
            List<Prueba> pruebas,
            List<Tema> temas)
        {
            var datos = pruebas
                .GroupBy(p => p.IdTema)
                .Select(g => new
                {
                    Tema = temas.FirstOrDefault(t => t.IdTema == g.Key)?.Nombre
                        ?? $"Tema {g.Key}",
                    Total = (double)g.Count()
                })
                .OrderByDescending(d => d.Total)
                .Take(6)
                .ToList();

            if (datos.Count == 0)
            {
                datos.Add(new { Tema = "Sin datos", Total = 0D });
            }

            return new CartesianChart
            {
                Series = new ISeries[]
                {
                    new RowSeries<double>
                    {
                        Values = datos.Select(d => d.Total).ToArray(),
                        Name = "Pruebas",
                        Fill = new SolidColorPaint(new SKColor(255, 99, 132))
                    }
                },
                YAxes = new[]
                {
                    new Axis
                    {
                        Labels = datos.Select(d => AcortarTexto(d.Tema, 14)).ToArray(),
                        LabelsPaint = new SolidColorPaint(new SKColor(70, 90, 120))
                    }
                },
                XAxes = new[]
                {
                    new Axis
                    {
                        MinLimit = 0,
                        LabelsPaint = new SolidColorPaint(new SKColor(70, 90, 120))
                    }
                },
                LegendPosition = LegendPosition.Hidden
            };
        }

        private PieChart CrearGraficaDistribucion(DashboardDatos datos)
        {
            var valores = new[]
            {
                new { Nombre = "Alumnos", Total = (double)datos.TotalAlumnos },
                new { Nombre = "Pruebas", Total = (double)datos.TotalPruebas },
                new { Nombre = "Proyectos", Total = (double)datos.TotalProyectos },
                new { Nombre = "Temas", Total = (double)datos.Temas.Count }
            };

            if (valores.All(v => v.Total == 0))
            {
                valores = new[]
                {
                    new { Nombre = "Sin datos", Total = 1D }
                };
            }

            return new PieChart
            {
                Series = valores.Select(v =>
                    new PieSeries<double>
                    {
                        Name = v.Nombre,
                        Values = new[] { v.Total },
                        DataLabelsSize = 12,
                        DataLabelsPaint = new SolidColorPaint(SKColors.White)
                    }).ToArray(),
                LegendPosition = LegendPosition.Right
            };
        }

        private static string AcortarTexto(string texto, int maximo)
        {
            return texto.Length <= maximo
                ? texto
                : $"{texto[..maximo]}...";
        }

        private sealed record DashboardDatos(
            int TotalAlumnos,
            int TotalPruebas,
            int TotalProyectos,
            List<Usuario> Alumnos,
            List<Tema> Temas,
            List<Prueba> Pruebas,
            List<Proyecto> Proyectos,
            List<Resultado> Resultados
        );

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
                new ControlUsuarios(_usuarioActual)
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
                new ControlProyectos(_usuarioActual)
                {
                    Dock = DockStyle.Fill
                };

            panelContenido.Controls.Add(control);
        }

        private void MostrarTemas()
        {
            panelContenido.Controls.Clear();

            ControlTemas control =
                new ControlTemas(_usuarioActual)
                {
                    Dock = DockStyle.Fill
                };

            panelContenido.Controls.Add(control);
        }

        private void MostrarExamenes()
        {
            panelContenido.Controls.Clear();

            ControlPruebas control =
                new ControlPruebas(_usuarioActual)
                {
                    Dock = DockStyle.Fill
                };

            panelContenido.Controls.Add(control);
        }

        private void MostrarPreguntas()
        {
            panelContenido.Controls.Clear();

            ControlPreguntas control =
                new ControlPreguntas(_usuarioActual)
                {
                    Dock = DockStyle.Fill
                };

            panelContenido.Controls.Add(control);
        }

        private void MostrarResultados()
        {
            panelContenido.Controls.Clear();

            ControlResultados control =
                new ControlResultados(_usuarioActual)
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
