using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class ControlResultados : UserControl
    {
        private ComboBox cmbAlumno = null!;
        private DataGridView dgvResultados = null!;

        private Button btnEditar = null!;

        private readonly ResultadoBLL _resultadoBll = new();
        private readonly UsuarioBLL _usuarioBll = new();

        private List<Usuario> _alumnos = new();

        public ControlResultados()
        {
            InicializarComponentes();

            CargarAlumnos();
        }

        private void InicializarComponentes()
        {
            this.BackColor =
                Color.FromArgb(245, 250, 255);

            // =====================================================
            // TITULO
            // =====================================================

            Label lblTitulo = new Label
            {
                Text = "📊 Gestión de Resultados",

                Font = new Font(
                    "Segoe UI",
                    22,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(20, 35, 80),

                AutoSize = true,

                Location = new Point(20, 20)
            };

            // =====================================================
            // SUBTITULO
            // =====================================================

            Label lblSubtitulo = new Label
            {
                Text =
                    "Consulta y administra resultados fácilmente ✨",

                Font = new Font("Segoe UI", 12),

                ForeColor =
                    Color.FromArgb(100, 120, 150),

                AutoSize = true,

                Location = new Point(25, 65)
            };

            // =====================================================
            // LABEL ALUMNO
            // =====================================================

            Label lblAlumno = new Label
            {
                Text = "👨‍🎓 Seleccionar alumno:",

                Font = new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(20, 35, 90),

                AutoSize = true,

                Location = new Point(25, 125)
            };

            // =====================================================
            // COMBO ALUMNO
            // =====================================================

            cmbAlumno = new ComboBox
            {
                Font = new Font("Segoe UI", 11),

                Location = new Point(250, 118),

                Size = new Size(420, 40),

                DropDownStyle =
                    ComboBoxStyle.DropDownList,

                FlatStyle = FlatStyle.Flat,

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(50, 70, 120)
            };

            cmbAlumno.SelectedIndexChanged +=
                CmbAlumno_SelectedIndexChanged;

            // =====================================================
            // BOTON EDITAR
            // =====================================================

            btnEditar = CrearBoton(
                "✏️ Editar",
                Color.FromArgb(52, 199, 89)
            );

            btnEditar.Location =
                new Point(20, 190);

            btnEditar.Click += BtnEditar_Click;

            // =====================================================
            // DATA GRID
            // =====================================================

            dgvResultados = new DataGridView
            {
                Location = new Point(20, 280),

                Size = new Size(1320, 520),

                BackgroundColor = Color.White,

                BorderStyle = BorderStyle.None,

                CellBorderStyle =
                    DataGridViewCellBorderStyle
                    .SingleHorizontal,

                ColumnHeadersBorderStyle =
                    DataGridViewHeaderBorderStyle
                    .None,

                SelectionMode =
                    DataGridViewSelectionMode
                    .FullRowSelect,

                MultiSelect = false,

                ReadOnly = true,

                AllowUserToAddRows = false,

                AllowUserToDeleteRows = false,

                AllowUserToResizeRows = false,

                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode
                    .Fill,

                RowHeadersVisible = false,

                Font = new Font("Segoe UI", 11),

                GridColor =
                    Color.FromArgb(230, 235, 245)
            };

            dgvResultados.EnableHeadersVisualStyles =
                false;

            // =====================================================
            // HEADER
            // =====================================================

            dgvResultados.ColumnHeadersDefaultCellStyle
                .BackColor =
                Color.FromArgb(240, 247, 255);

            dgvResultados.ColumnHeadersDefaultCellStyle
                .ForeColor =
                Color.FromArgb(50, 70, 120);

            dgvResultados.ColumnHeadersDefaultCellStyle
                .Font =
                new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold
                );

            dgvResultados.ColumnHeadersHeight = 55;

            // =====================================================
            // FILAS
            // =====================================================

            dgvResultados.DefaultCellStyle.BackColor =
                Color.White;

            dgvResultados.DefaultCellStyle.ForeColor =
                Color.FromArgb(60, 70, 100);

            dgvResultados.DefaultCellStyle
                .SelectionBackColor =
                Color.FromArgb(220, 235, 255);

            dgvResultados.DefaultCellStyle
                .SelectionForeColor =
                Color.FromArgb(20, 35, 80);

            dgvResultados.RowTemplate.Height = 50;

            // =====================================================
            // CONTROLES
            // =====================================================

            this.Controls.Add(lblTitulo);

            this.Controls.Add(lblSubtitulo);

            this.Controls.Add(lblAlumno);

            this.Controls.Add(cmbAlumno);

            this.Controls.Add(btnEditar);

            this.Controls.Add(dgvResultados);
        }

        // =====================================================
        // CREAR BOTON
        // =====================================================

        private Button CrearBoton(
            string texto,
            Color color
        )
        {
            Button btn = new Button
            {
                Text = texto,

                Font = new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold
                ),

                BackColor = color,

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Size = new Size(170, 45),

                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor =
                    ControlPaint.Light(color);
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = color;
            };

            return btn;
        }

        // =====================================================
        // CARGAR ALUMNOS
        // =====================================================

        private void CargarAlumnos()
        {
            try
            {
                _alumnos =
                    _usuarioBll.ObtenerAlumnos();

                cmbAlumno.Items.Clear();

                cmbAlumno.Items.Add(
                    "-- Todos los alumnos --"
                );

                foreach (var a in _alumnos)
                {
                    cmbAlumno.Items.Add(
                        $"{a.Nombre} ({a.Grado})"
                    );
                }

                cmbAlumno.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar alumnos:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // CAMBIO ALUMNO
        // =====================================================

        private void CmbAlumno_SelectedIndexChanged(
            object? sender,
            EventArgs e
        )
        {
            try
            {
                List<ResultadoExamen> resultados;

                if (cmbAlumno.SelectedIndex <= 0)
                {
                    resultados =
                        _resultadoBll.ObtenerTodos();
                }
                else
                {
                    var alumno =
                        _alumnos[
                            cmbAlumno.SelectedIndex - 1
                        ];

                    resultados =
                        _resultadoBll.ObtenerPorAlumno(
                            alumno.Id
                        );
                }

                dgvResultados.DataSource = null;

                dgvResultados.DataSource = resultados;

                // =================================================
                // OCULTAR
                // =================================================

                if (dgvResultados.Columns.Contains("Id"))
                    dgvResultados.Columns["Id"].Visible = false;

                if (dgvResultados.Columns.Contains("UsuarioId"))
                    dgvResultados.Columns["UsuarioId"].Visible = false;

                if (dgvResultados.Columns.Contains("ExamenId"))
                    dgvResultados.Columns["ExamenId"].Visible = false;

                // =================================================
                // HEADERS
                // =================================================

                if (dgvResultados.Columns.Contains("NombreAlumno"))
                    dgvResultados.Columns["NombreAlumno"].HeaderText = "👤 Alumno";

                if (dgvResultados.Columns.Contains("NombreExamen"))
                    dgvResultados.Columns["NombreExamen"].HeaderText = "📝 Examen";

                if (dgvResultados.Columns.Contains("NombreUnidad"))
                    dgvResultados.Columns["NombreUnidad"].HeaderText = "📚 Unidad";

                if (dgvResultados.Columns.Contains("Calificacion"))
                    dgvResultados.Columns["Calificacion"].HeaderText = "📊 Calificación";

                if (dgvResultados.Columns.Contains("FechaPresentacion"))
                    dgvResultados.Columns["FechaPresentacion"].HeaderText = "📅 Fecha";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar resultados:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // EDITAR
        // =====================================================

        private void BtnEditar_Click(
            object? sender,
            EventArgs e
        )
        {
            if (dgvResultados.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un resultado.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            MessageBox.Show(
                "Aquí puedes abrir el formulario para editar resultados.",
                "Editar",
                MessageBoxButtons.OK,
                MessageBoxIcon.Information
            );
        }
    }
}