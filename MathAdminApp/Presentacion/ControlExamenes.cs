// ============================================================
// Presentacion: ControlExamenes (UserControl)
// Vista para gestionar examenes por unidad
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    /// <summary>
    /// Control de usuario para la gestion de examenes.
    /// Permite filtrar por unidad y crear/eliminar examenes.
    /// </summary>
    public class ControlExamenes : UserControl
    {
        private ComboBox cmbUnidad = null!;
        private Label lblFiltro = null!;
        private DataGridView dgvExamenes = null!;
        private TextBox txtNombreExamen = null!;
        private Button btnCrear = null!;
        private Button btnEliminar = null!;
        private Panel panelSuperior = null!;
        private Panel panelCrear = null!;

        private readonly ExamenBLL _examenBll = new();
        private readonly UnidadBLL _unidadBll = new();
        private List<Unidad> _unidades = new();

        public ControlExamenes()
        {
            InicializarComponentes();
            CargarUnidades();
        }

        private void InicializarComponentes()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);

            // --- Panel superior: filtro por unidad ---
            panelSuperior = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.Transparent
            };

            lblFiltro = new Label
            {
                Text = "Filtrar por unidad:",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(0, 15)
            };

            cmbUnidad = new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(140, 10),
                Size = new Size(300, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbUnidad.SelectedIndexChanged += CmbUnidad_SelectedIndexChanged;

            panelSuperior.Controls.Add(lblFiltro);
            panelSuperior.Controls.Add(cmbUnidad);

            // --- Panel para crear nuevo examen ---
            panelCrear = new Panel
            {
                Dock = DockStyle.Top,
                Height = 55,
                BackColor = Color.Transparent
            };

            var lblNuevo = new Label
            {
                Text = "Nombre del examen:",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(0, 18)
            };

            txtNombreExamen = new TextBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(155, 14),
                Size = new Size(280, 26),
                BorderStyle = BorderStyle.FixedSingle
            };

            btnCrear = new Button
            {
                Text = "Crear Examen",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 179, 0), // Amarillo fuerte
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(130, 32),
                Location = new Point(450, 12),
                Cursor = Cursors.Hand
            };
            btnCrear.FlatAppearance.BorderSize = 0;
            btnCrear.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 202, 40); // Hover
            btnCrear.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 179, 0); // Click
            btnCrear.Click += BtnCrear_Click;


            btnEliminar = new Button
            {
                Text = "Eliminar",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 179, 0), // Amarillo fuerte
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 32),
                Location = new Point(590, 12),
                Cursor = Cursors.Hand
            };
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 179, 0);
            btnEliminar.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 160, 0);
            btnEliminar.Click += BtnEliminar_Click;

            panelCrear.Controls.Add(lblNuevo);
            panelCrear.Controls.Add(txtNombreExamen);
            panelCrear.Controls.Add(btnCrear);
            panelCrear.Controls.Add(btnEliminar);
            // --- Tabla de examenes ---
            dgvExamenes = new DataGridView
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10),
                GridColor = Color.FromArgb(255, 179, 0) // Amarillo fuerte
            };

            // Desactivar estilo visual del sistema
            dgvExamenes.EnableHeadersVisualStyles = false;

            // ----- ENCABEZADO -----
            dgvExamenes.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 241, 118); // Amarillo claro
            dgvExamenes.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvExamenes.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 241, 118);
            dgvExamenes.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvExamenes.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvExamenes.ColumnHeadersHeight = 40;

            // ----- SELECCIÓN -----
            dgvExamenes.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 202, 40);
            dgvExamenes.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvExamenes.RowTemplate.Height = 35;

            this.Controls.Add(dgvExamenes);
            this.Controls.Add(panelCrear);
            this.Controls.Add(panelSuperior);
        }

        private void CargarUnidades()
        {
            try
            {
                _unidades = _unidadBll.ObtenerTodas();
                cmbUnidad.Items.Clear();
                cmbUnidad.Items.Add("-- Todas las unidades --");
                foreach (var u in _unidades)
                    cmbUnidad.Items.Add($"Unidad {u.NumeroUnidad}: {u.Nombre}");
                cmbUnidad.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar unidades:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarExamenes(int? unidadId = null)
        {
            try
            {
                var examenes = _examenBll.ObtenerPorUnidad(unidadId);
                dgvExamenes.DataSource = null;
                dgvExamenes.DataSource = examenes;

                if (dgvExamenes.Columns.Contains("UnidadId"))
                    dgvExamenes.Columns["UnidadId"].Visible = false;
                if (dgvExamenes.Columns.Contains("Activo"))
                    dgvExamenes.Columns["Activo"].Visible = false;
                if (dgvExamenes.Columns.Contains("NombreUnidad"))
                    dgvExamenes.Columns["NombreUnidad"].HeaderText = "Unidad";
                if (dgvExamenes.Columns.Contains("FechaCreacion"))
                    dgvExamenes.Columns["FechaCreacion"].HeaderText = "Fecha Creacion";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar examenes:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbUnidad_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbUnidad.SelectedIndex <= 0)
            {
                CargarExamenes(null);
            }
            else
            {
                var unidad = _unidades[cmbUnidad.SelectedIndex - 1];
                CargarExamenes(unidad.Id);
            }
        }

        private void BtnCrear_Click(object? sender, EventArgs e)
        {
            if (cmbUnidad.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione una unidad para crear el examen.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var unidad = _unidades[cmbUnidad.SelectedIndex - 1];
                var examen = new Examen
                {
                    Nombre = txtNombreExamen.Text.Trim(),
                    UnidadId = unidad.Id
                };
                _examenBll.Agregar(examen);
                txtNombreExamen.Clear();
                CargarExamenes(unidad.Id);
                MessageBox.Show("Examen creado.", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvExamenes.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un examen.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var examen = (Examen)dgvExamenes.CurrentRow.DataBoundItem;
            var resultado = MessageBox.Show($"Desea eliminar el examen '{examen.Nombre}'?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    _examenBll.Eliminar(examen.Id);
                    CmbUnidad_SelectedIndexChanged(null, EventArgs.Empty);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
