// ============================================================// Presentacion: ControlResultados (UserControl)// Vista moderna y colorida para resultados// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;
using MathAdminApp.Presentacion;

namespace MathAdminApp.Presentacion
{
    public class ControlResultados : UserControl
    {
        private ComboBox cmbAlumno = null!; private DataGridView dgvResultados = null!; private Label lblResumen = null!;

        private readonly ResultadoBLL _resultadoBll = new();
        private readonly UsuarioBLL _usuarioBll = new();

        private List<Usuario> _alumnos = new();

        public ControlResultados()
        {
            this.DoubleBuffered = true;

            InicializarComponentes();
            CargarAlumnos();
        }

        // =====================================================
        // DISEÑO VISUAL
        // =====================================================

        private void InicializarComponentes()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Dock = DockStyle.Fill;

            // =====================================================
            // TITULO PRINCIPAL
            // =====================================================

            Label lblTitulo = new Label
            {
                Text = "📊 Gestión de Resultados",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 44, 99),
                AutoSize = true,
                Location = new Point(40, 25)
            };

            Label lblSubtitulo = new Label
            {
                Text = "Consulta y analiza los resultados de los alumnos ✨",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(90, 110, 140),
                AutoSize = true,
                Location = new Point(40, 100)
            };

            // =====================================================
            // TARJETA PRINCIPAL
            // =====================================================

            Panel cardPrincipal = new Panel
            {
                BackColor = Color.White,
                Location = new Point(25, 140),
                Size = new Size(1150, 620),
                BorderStyle = BorderStyle.None
            };

            // =====================================================
            // PANEL SUPERIOR
            // =====================================================

            Panel panelSuperior = new Panel
            {
                Size = new Size(1100, 100),
                Location = new Point(20, 15),
                BackColor = Color.Transparent
            };

            // =====================================================
            // LABEL COMBOBOX
            // =====================================================

            Label lblSeleccionar = new Label
            {
                Text = "Seleccionar alumno",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 44, 99),
                AutoSize = true,
                Location = new Point(0, 0)
            };

            // =====================================================
            // COMBOBOX
            // =====================================================

            cmbAlumno = new ComboBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(0, 35),
                Size = new Size(420, 35),

                DropDownStyle = ComboBoxStyle.DropDownList,

                FlatStyle = FlatStyle.Flat,

                BackColor = Color.FromArgb(248, 250, 255),

                ForeColor = Color.FromArgb(50, 50, 50)
            };

            cmbAlumno.SelectedIndexChanged += CmbAlumno_SelectedIndexChanged;

            // =====================================================
            // TARJETA RESUMEN
            // =====================================================

            Panel panelResumen = new Panel
            {
                BackColor = Color.FromArgb(237, 233, 254),
                Location = new Point(470, 25),
                Size = new Size(420, 50)
            };

            lblResumen = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                ForeColor = Color.FromArgb(109, 40, 217),
                AutoSize = true,
                Location = new Point(15, 15)
            };

            panelResumen.Controls.Add(lblResumen);

            // =====================================================
            // AGREGAR AL PANEL SUPERIOR
            // =====================================================

            panelSuperior.Controls.Add(lblSeleccionar);
            panelSuperior.Controls.Add(cmbAlumno);
            panelSuperior.Controls.Add(panelResumen);

            // =====================================================
            // TABLA
            // =====================================================

            dgvResultados = new DataGridView
            {
                Location = new Point(20, 130),
                Size = new Size(1100, 460),

                BackgroundColor = Color.White,

                BorderStyle = BorderStyle.None,

                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,

                ReadOnly = true,
                MultiSelect = false,

                SelectionMode = DataGridViewSelectionMode.FullRowSelect,

                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,

                RowHeadersVisible = false,

                Font = new Font("Segoe UI", 10),

                GridColor = Color.FromArgb(230, 230, 230)
            };

            dgvResultados.EnableHeadersVisualStyles = false;

            // =====================================================
            // HEADER TABLA
            // =====================================================

            dgvResultados.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;

            dgvResultados.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(219, 234, 254);

            dgvResultados.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(15, 44, 99);

            dgvResultados.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 11, FontStyle.Bold);

            dgvResultados.ColumnHeadersDefaultCellStyle.SelectionBackColor =
                Color.FromArgb(219, 234, 254);

            dgvResultados.ColumnHeadersDefaultCellStyle.SelectionForeColor =
                Color.FromArgb(15, 44, 99);

            dgvResultados.ColumnHeadersHeight = 45;

            // =====================================================
            // FILAS TABLA
            // =====================================================

            dgvResultados.DefaultCellStyle.BackColor =
                Color.White;

            dgvResultados.DefaultCellStyle.ForeColor =
                Color.FromArgb(60, 60, 60);

            dgvResultados.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(191, 219, 254);

            dgvResultados.DefaultCellStyle.SelectionForeColor =
                Color.Black;

            dgvResultados.AlternatingRowsDefaultCellStyle.BackColor =
                Color.FromArgb(248, 250, 255);

            dgvResultados.RowTemplate.Height = 38;

            // =====================================================
            // AGREGAR A TARJETA PRINCIPAL
            // =====================================================

            cardPrincipal.Controls.Add(panelSuperior);
            cardPrincipal.Controls.Add(dgvResultados);

            // =====================================================
            // AGREGAR AL USERCONTROL
            // =====================================================

            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblSubtitulo);
            this.Controls.Add(cardPrincipal);
        }

        // =====================================================
        // CARGAR ALUMNOS
        // =====================================================

        private void CargarAlumnos()
        {
            try
            {
                _alumnos = _usuarioBll.ObtenerAlumnos();

                cmbAlumno.Items.Clear();

                cmbAlumno.Items.Add("-- Todos los alumnos --");

                foreach (var a in _alumnos)
                {
                    cmbAlumno.Items.Add(
                        $"{a.Nombre} ({a.Grado})");
                }

                cmbAlumno.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar alumnos:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // EVENTO COMBOBOX
        // =====================================================

        private void CmbAlumno_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            try
            {
                List<ResultadoExamen> resultados;

                if (cmbAlumno.SelectedIndex <= 0)
                {
                    resultados = _resultadoBll.ObtenerTodos();

                    lblResumen.Text = "📘 Mostrando resultados generales";
                }
                else
                {
                    var alumno =
                        _alumnos[cmbAlumno.SelectedIndex - 1];

                    resultados =
                        _resultadoBll.ObtenerPorAlumno(alumno.Id);

                    // =====================================================
                    // PROMEDIO
                    // =====================================================

                    if (resultados.Count > 0)
                    {
                        var promedio =
                            resultados.Average(
                                r => (double)r.Calificacion);

                        lblResumen.Text =
                            $"⭐ Promedio: {promedio:F1}   |   📚 Exámenes: {resultados.Count}";
                    }
                    else
                    {
                        lblResumen.Text =
                            "⚠ Este alumno no tiene resultados registrados.";
                    }
                }

                dgvResultados.DataSource = null;
                dgvResultados.DataSource = resultados;

                // =====================================================
                // CONFIGURAR COLUMNAS
                // =====================================================

                if (dgvResultados.Columns.Contains("Id"))
                    dgvResultados.Columns["Id"].Visible = false;

                if (dgvResultados.Columns.Contains("UsuarioId"))
                    dgvResultados.Columns["UsuarioId"].Visible = false;

                if (dgvResultados.Columns.Contains("ExamenId"))
                    dgvResultados.Columns["ExamenId"].Visible = false;

                if (dgvResultados.Columns.Contains("NombreAlumno"))
                    dgvResultados.Columns["NombreAlumno"].HeaderText =
                        "Alumno";

                if (dgvResultados.Columns.Contains("NombreExamen"))
                    dgvResultados.Columns["NombreExamen"].HeaderText =
                        "Examen";

                if (dgvResultados.Columns.Contains("NombreUnidad"))
                    dgvResultados.Columns["NombreUnidad"].HeaderText =
                        "Unidad";

                if (dgvResultados.Columns.Contains("Calificacion"))
                    dgvResultados.Columns["Calificacion"].HeaderText =
                        "Calificación";

                if (dgvResultados.Columns.Contains("FechaPresentacion"))
                    dgvResultados.Columns["FechaPresentacion"].HeaderText =
                        "Fecha";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar resultados:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
    }

}