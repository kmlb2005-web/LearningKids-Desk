// ============================================================
// Presentacion: ControlPreguntas (UserControl)
// Vista para gestionar preguntas de examenes
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    /// <summary>
    /// Control de usuario para la gestion de preguntas.
    /// Permite seleccionar un examen y agregar preguntas de tipo
    /// opcion multiple o abierta.
    /// </summary>
    public class ControlPreguntas : UserControl
    {
        private ComboBox cmbExamen = null!;
        private Label lblSeleccionar = null!;
        private DataGridView dgvPreguntas = null!;
        private Button btnAgregar = null!;
        private Button btnEliminar = null!;
        private Panel panelSuperior = null!;
        private Panel panelBotones = null!;

        private readonly PreguntaBLL _preguntaBll = new();
        private readonly ExamenBLL _examenBll = new();
        private List<Examen> _examenes = new();

        public ControlPreguntas()
        {
            InicializarComponentes();
            CargarExamenes();
        }

        private void InicializarComponentes()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);

            // --- Panel superior: selector de examen ---
            panelSuperior = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.Transparent
            };

            lblSeleccionar = new Label
            {
                Text = "Seleccionar examen:",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(0, 15)
            };

            cmbExamen = new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(155, 10),
                Size = new Size(400, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbExamen.SelectedIndexChanged += CmbExamen_SelectedIndexChanged;

            panelSuperior.Controls.Add(lblSeleccionar);
            panelSuperior.Controls.Add(cmbExamen);

            // --- Barra de botones ---
            panelBotones = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.Transparent
            };

            btnAgregar = new Button
            {
                Text = "Agregar Pregunta",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 179, 0), // Amarillo base
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(155, 35),
                Location = new Point(0, 8),
                Cursor = Cursors.Hand
            };
            btnAgregar.FlatAppearance.BorderSize = 0;
            btnAgregar.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 202, 40);
            btnAgregar.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 160, 0);
            btnAgregar.Click += BtnAgregar_Click;


            btnEliminar = new Button
            {
                Text = "Eliminar",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 179, 0), // Amarillo base
                ForeColor = Color.Black,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(100, 35),
                Location = new Point(170, 8),
                Cursor = Cursors.Hand
            };
            btnEliminar.FlatAppearance.BorderSize = 0;
            btnEliminar.FlatAppearance.MouseOverBackColor = Color.FromArgb(255, 202, 40);
            btnEliminar.FlatAppearance.MouseDownBackColor = Color.FromArgb(255, 160, 0);
            btnEliminar.Click += BtnEliminar_Click;

            panelBotones.Controls.Add(btnAgregar);
            panelBotones.Controls.Add(btnEliminar);

            // --- Tabla de preguntas ---
            dgvPreguntas = new DataGridView
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

            // Desactivar estilos del sistema
            dgvPreguntas.EnableHeadersVisualStyles = false;

            // ----- ENCABEZADO -----
            dgvPreguntas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 241, 118);
            dgvPreguntas.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvPreguntas.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 241, 118);
            dgvPreguntas.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvPreguntas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvPreguntas.ColumnHeadersHeight = 40;

            // ----- SELECCIÓN -----
            dgvPreguntas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 202, 40);
            dgvPreguntas.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvPreguntas.RowTemplate.Height = 35;

            this.Controls.Add(dgvPreguntas);
            this.Controls.Add(panelBotones);
            this.Controls.Add(panelSuperior);
        }

        private void CargarExamenes()
        {
            try
            {
                _examenes = _examenBll.ObtenerPorUnidad(null);
                cmbExamen.Items.Clear();
                cmbExamen.Items.Add("-- Seleccione un examen --");
                foreach (var ex in _examenes)
                    cmbExamen.Items.Add($"[{ex.NombreUnidad}] {ex.Nombre}");
                cmbExamen.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar examenes:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPreguntas(int examenId)
        {
            try
            {
                var preguntas = _preguntaBll.ObtenerPorExamen(examenId);
                dgvPreguntas.DataSource = null;
                dgvPreguntas.DataSource = preguntas;

                if (dgvPreguntas.Columns.Contains("ExamenId"))
                    dgvPreguntas.Columns["ExamenId"].Visible = false;
                if (dgvPreguntas.Columns.Contains("RespuestaCorrecta"))
                    dgvPreguntas.Columns["RespuestaCorrecta"].HeaderText = "Respuesta";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar preguntas:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbExamen_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbExamen.SelectedIndex <= 0)
            {
                dgvPreguntas.DataSource = null;
                return;
            }

            var examen = _examenes[cmbExamen.SelectedIndex - 1];
            CargarPreguntas(examen.Id);
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            if (cmbExamen.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un examen primero.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var examen = _examenes[cmbExamen.SelectedIndex - 1];
            var form = new FormPreguntaDetalle(examen.Id);
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarPreguntas(examen.Id);
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvPreguntas.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una pregunta.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var pregunta = (Pregunta)dgvPreguntas.CurrentRow.DataBoundItem;
            var resultado = MessageBox.Show("Desea eliminar esta pregunta?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    _preguntaBll.Eliminar(pregunta.Id);
                    var examen = _examenes[cmbExamen.SelectedIndex - 1];
                    CargarPreguntas(examen.Id);
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
