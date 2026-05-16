// ============================================================
// Presentacion: ControlExamenes (MODERNO)
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class ControlExamenes : UserControl
    {
        // =====================================================
        // CONTROLES
        // =====================================================

        private ComboBox cmbUnidad = null!;

        private DataGridView dgvExamenes = null!;

        private TextBox txtNombreExamen = null!;

        private Button btnCrear = null!;

        private Button btnEliminar = null!;

        private TextBox txtBuscar = null!;

        // =====================================================
        // BLL
        // =====================================================

        private readonly ExamenBLL _examenBll = new();

        private readonly UnidadBLL _unidadBll = new();

        private List<Unidad> _unidades = new();

        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public ControlExamenes()
        {
            InicializarComponentes();

            CargarUnidades();
        }

        // =====================================================
        // INTERFAZ MODERNA
        // =====================================================

        private void InicializarComponentes()
        {
            // =================================================
            // USERCONTROL
            // =================================================

            this.BackColor =
                Color.FromArgb(245, 248, 255);

            // =================================================
            // PANEL SUPERIOR
            // =================================================

            Panel panelSuperior = new Panel
            {
                Dock = DockStyle.Top,

                Height = 220,

                BackColor = Color.Transparent
            };

            // =================================================
            // TITULO
            // =================================================

            Label lblTitulo = new Label
            {
                Text = "📝 Gestión de Exámenes",

                Font = new Font(
                    "Segoe UI",
                    28,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(15, 35, 90),

                AutoSize = true,

                Location = new Point(25, 15)
            };

            // =================================================
            // SUBTITULO
            // =================================================

            Label lblSubtitulo = new Label
            {
                Text =
                    "Administra y crea evaluaciones fácilmente ✨",

                Font = new Font("Segoe UI", 13),

                ForeColor =
                    Color.FromArgb(110, 120, 150),

                AutoSize = true,

                Location = new Point(30, 65)
            };

            // =================================================
            // FILTRO
            // =================================================

            Label lblFiltro = new Label
            {
                Text = "📚 Filtrar por unidad",

                Font = new Font(
                    "Segoe UI",
                    12,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(30, 50, 90),

                AutoSize = true,

                Location = new Point(30, 120)
            };

            cmbUnidad = new ComboBox
            {
                Font = new Font("Segoe UI", 12),

                Location = new Point(30, 155),

                Size = new Size(320, 45),

                DropDownStyle =
                    ComboBoxStyle.DropDownList,

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(50, 70, 120)
            };

            cmbUnidad.SelectedIndexChanged +=
                CmbUnidad_SelectedIndexChanged;

            // =================================================
            // NOMBRE EXAMEN
            // =================================================

            txtNombreExamen = new TextBox
            {
                PlaceholderText =
                    "✏️ Nombre del examen...",

                Font = new Font("Segoe UI", 12),

                Location = new Point(380, 155),

                Size = new Size(320, 45),

                BorderStyle = BorderStyle.FixedSingle,

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(50, 70, 120)
            };

            // =================================================
            // BOTON CREAR
            // =================================================

            btnCrear = CrearBoton(
                "➕ Crear",
                Color.FromArgb(50, 120, 255)
            );

            btnCrear.Location =
                new Point(730, 148);

            btnCrear.Click += BtnCrear_Click;

            // =================================================
            // BOTON ELIMINAR
            // =================================================

            btnEliminar = CrearBoton(
                "🗑 Eliminar",
                Color.FromArgb(255, 70, 120)
            );

            btnEliminar.Location =
                new Point(930, 148);

            btnEliminar.Click += BtnEliminar_Click;

            // =================================================
            // BUSCADOR
            // =================================================

            txtBuscar = new TextBox
            {
                PlaceholderText =
                    "🔍 Buscar examen...",

                Font = new Font("Segoe UI", 12),

                Size = new Size(250, 45),

                Location = new Point(1120, 155),

                BorderStyle = BorderStyle.FixedSingle,

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(60, 70, 100)
            };

            txtBuscar.TextChanged +=
                TxtBuscar_TextChanged;

            // =================================================
            // AGREGAR CONTROLES
            // =================================================

            panelSuperior.Controls.Add(lblTitulo);

            panelSuperior.Controls.Add(lblSubtitulo);

            panelSuperior.Controls.Add(lblFiltro);

            panelSuperior.Controls.Add(cmbUnidad);

            panelSuperior.Controls.Add(txtNombreExamen);

            panelSuperior.Controls.Add(btnCrear);

            panelSuperior.Controls.Add(btnEliminar);

            panelSuperior.Controls.Add(txtBuscar);

            // =================================================
            // TABLA MODERNA
            // =================================================

            dgvExamenes = new DataGridView
            {
                Dock = DockStyle.Fill,

                BackgroundColor = Color.White,

                BorderStyle = BorderStyle.None,

                CellBorderStyle =
                    DataGridViewCellBorderStyle.SingleHorizontal,

                ColumnHeadersBorderStyle =
                    DataGridViewHeaderBorderStyle.None,

                SelectionMode =
                    DataGridViewSelectionMode.FullRowSelect,

                MultiSelect = false,

                ReadOnly = true,

                AllowUserToAddRows = false,

                AllowUserToDeleteRows = false,

                AllowUserToResizeRows = false,

                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,

                RowHeadersVisible = false,

                Font = new Font("Segoe UI", 11),

                GridColor =
                    Color.FromArgb(235, 240, 250),

                RowTemplate =
                {
                    Height = 55
                }
            };

            dgvExamenes.EnableHeadersVisualStyles = false;

            // =================================================
            // HEADER
            // =================================================

            dgvExamenes.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(245, 248, 255);

            dgvExamenes.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(20, 35, 90);

            dgvExamenes.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    12,
                    FontStyle.Bold
                );

            dgvExamenes.ColumnHeadersHeight = 60;

            dgvExamenes.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // =================================================
            // FILAS
            // =================================================

            dgvExamenes.DefaultCellStyle.BackColor =
                Color.White;

            dgvExamenes.DefaultCellStyle.ForeColor =
                Color.FromArgb(40, 50, 80);

            dgvExamenes.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(230, 240, 255);

            dgvExamenes.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(20, 35, 90);

            dgvExamenes.DefaultCellStyle.Padding =
                new Padding(8);

            // =================================================
            // PANEL TABLA
            // =================================================

            Panel panelTabla = new Panel
            {
                Dock = DockStyle.Fill,

                Padding = new Padding(25, 0, 25, 25),

                BackColor = Color.Transparent
            };

            panelTabla.Controls.Add(dgvExamenes);

            // =================================================
            // AGREGAR
            // =================================================

            this.Controls.Add(panelTabla);

            this.Controls.Add(panelSuperior);
        }

        // =====================================================
        // BOTONES
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
                    12,
                    FontStyle.Bold
                ),

                BackColor = color,

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Size = new Size(180, 55),

                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;

            return btn;
        }

        // =====================================================
        // CARGAR UNIDADES
        // =====================================================

        private void CargarUnidades()
        {
            try
            {
                _unidades =
                    _unidadBll.ObtenerTodas();

                cmbUnidad.Items.Clear();

                cmbUnidad.Items.Add(
                    "-- Todas las unidades --"
                );

                foreach (var u in _unidades)
                {
                    cmbUnidad.Items.Add(
                        $"Unidad {u.NumeroUnidad}: {u.Nombre}"
                    );
                }

                cmbUnidad.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar unidades:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // CARGAR EXAMENES
        // =====================================================

        private void CargarExamenes(
            int? unidadId = null
        )
        {
            try
            {
                var examenes =
                    _examenBll.ObtenerPorUnidad(unidadId);

                dgvExamenes.DataSource = null;

                dgvExamenes.DataSource = examenes;

                // =================================================
                // OCULTAR
                // =================================================

                if (dgvExamenes.Columns.Contains("UnidadId"))
                    dgvExamenes.Columns["UnidadId"].Visible = false;

                if (dgvExamenes.Columns.Contains("Activo"))
                    dgvExamenes.Columns["Activo"].Visible = false;

                // =================================================
                // RENOMBRAR
                // =================================================

                if (dgvExamenes.Columns.Contains("NombreUnidad"))
                    dgvExamenes.Columns["NombreUnidad"]
                        .HeaderText = "Unidad";

                if (dgvExamenes.Columns.Contains("FechaCreacion"))
                    dgvExamenes.Columns["FechaCreacion"]
                        .HeaderText = "Fecha Creación";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar exámenes:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // FILTRAR
        // =====================================================

        private void CmbUnidad_SelectedIndexChanged(
            object? sender,
            EventArgs e
        )
        {
            if (cmbUnidad.SelectedIndex <= 0)
            {
                CargarExamenes(null);
            }
            else
            {
                var unidad =
                    _unidades[
                        cmbUnidad.SelectedIndex - 1
                    ];

                CargarExamenes(unidad.Id);
            }
        }

        // =====================================================
        // BUSCAR
        // =====================================================

        private void TxtBuscar_TextChanged(
            object? sender,
            EventArgs e
        )
        {
            try
            {
                string texto =
                    txtBuscar.Text
                        .Trim()
                        .ToLower();

                var examenes =
                    _examenBll.ObtenerPorUnidad(null);

                if (!string.IsNullOrWhiteSpace(texto))
                {
                    examenes = examenes
                        .Where(x =>
                            x.Nombre.ToLower().Contains(texto)
                        )
                        .ToList();
                }

                dgvExamenes.DataSource = null;

                dgvExamenes.DataSource = examenes;
            }
            catch
            {
            }
        }

        // =====================================================
        // CREAR
        // =====================================================

        private void BtnCrear_Click(
            object? sender,
            EventArgs e
        )
        {
            if (cmbUnidad.SelectedIndex <= 0)
            {
                MessageBox.Show(
                    "Seleccione una unidad.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            try
            {
                var unidad =
                    _unidades[
                        cmbUnidad.SelectedIndex - 1
                    ];

                var examen = new Examen
                {
                    Nombre =
                        txtNombreExamen.Text.Trim(),

                    UnidadId =
                        unidad.Id
                };

                _examenBll.Agregar(examen);

                txtNombreExamen.Clear();

                CargarExamenes(unidad.Id);

                MessageBox.Show(
                    "Examen creado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // ELIMINAR
        // =====================================================

        private void BtnEliminar_Click(
            object? sender,
            EventArgs e
        )
        {
            if (dgvExamenes.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un examen.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var examen =
                (Examen)dgvExamenes
                    .CurrentRow
                    .DataBoundItem;

            var resultado =
                MessageBox.Show(
                    $"¿Desea eliminar el examen '{examen.Nombre}'?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    _examenBll.Eliminar(examen.Id);

                    CmbUnidad_SelectedIndexChanged(
                        null,
                        EventArgs.Empty
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }
    }
}