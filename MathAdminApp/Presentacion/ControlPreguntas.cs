// ============================================================// Presentacion: ControlPreguntas (UserControl)// Vista moderna para gestionar preguntas de examenes// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class ControlPreguntas : UserControl
    {
        private ComboBox cmbExamen = null!; private DataGridView dgvPreguntas = null!; private Button btnAgregar = null!; private Button btnEliminar = null!;

        private readonly PreguntaBLL _preguntaBll = new();
        private readonly ExamenBLL _examenBll = new();
        private List<Examen> _examenes = new();

        public ControlPreguntas()
        {
            this.DoubleBuffered = true;

            InicializarComponentes();
            CargarExamenes();
        }

        // =====================================================
        // DISEÑO VISUAL
        // =====================================================

        private void InicializarComponentes()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);
            this.Dock = DockStyle.Fill;

            // =====================================================
            // TITULO
            // =====================================================

            Label lblTitulo = new Label
            {
                Text = "❓ Gestión de Preguntas",
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 44, 99),
                AutoSize = true,
                Location = new Point(35, 25)
            };

            Label lblSubtitulo = new Label
            {
                Text = "Administra las preguntas de los exámenes fácilmente ✨",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(90, 110, 140),
                AutoSize = true,
                Location = new Point(40, 75)
            };

            // =====================================================
            // PANEL FILTROS
            // =====================================================

            Panel panelFiltros = new Panel
            {
                Size = new Size(1100, 80),
                Location = new Point(40, 120),
                BackColor = Color.Transparent
            };

            // =====================================================
            // LABEL EXAMEN
            // =====================================================

            Label lblSeleccionar = new Label
            {
                Text = "Seleccionar examen",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 44, 99),
                AutoSize = true,
                Location = new Point(0, 0)
            };

            // =====================================================
            // COMBOBOX
            // =====================================================

            cmbExamen = new ComboBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(0, 35),
                Size = new Size(420, 35),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White
            };

            cmbExamen.SelectedIndexChanged += CmbExamen_SelectedIndexChanged;

            // =====================================================
            // BOTON AGREGAR
            // =====================================================

            btnAgregar = new Button
            {
                Text = "➕ Agregar",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(59, 130, 246),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(170, 42),
                Location = new Point(500, 28),
                Cursor = Cursors.Hand
            };

            btnAgregar.FlatAppearance.BorderSize = 0;

            btnAgregar.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(37, 99, 235);

            btnAgregar.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(29, 78, 216);

            btnAgregar.Click += BtnAgregar_Click;

            // =====================================================
            // BOTON ELIMINAR
            // =====================================================

            btnEliminar = new Button
            {
                Text = "🗑 Eliminar",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 77, 109),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(170, 42),
                Location = new Point(690, 28),
                Cursor = Cursors.Hand
            };

            btnEliminar.FlatAppearance.BorderSize = 0;

            btnEliminar.FlatAppearance.MouseOverBackColor =
                Color.FromArgb(244, 63, 94);

            btnEliminar.FlatAppearance.MouseDownBackColor =
                Color.FromArgb(225, 29, 72);

            btnEliminar.Click += BtnEliminar_Click;

            // =====================================================
            // AGREGAR AL PANEL
            // =====================================================

            panelFiltros.Controls.Add(lblSeleccionar);
            panelFiltros.Controls.Add(cmbExamen);
            panelFiltros.Controls.Add(btnAgregar);
            panelFiltros.Controls.Add(btnEliminar);

            // =====================================================
            // TABLA
            // =====================================================

            dgvPreguntas = new DataGridView
            {
                Location = new Point(40, 230),
                Size = new Size(1100, 500),

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

            dgvPreguntas.EnableHeadersVisualStyles = false;

            // =====================================================
            // HEADER
            // =====================================================

            dgvPreguntas.ColumnHeadersBorderStyle =
                DataGridViewHeaderBorderStyle.None;

            dgvPreguntas.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(240, 244, 255);

            dgvPreguntas.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(15, 44, 99);

            dgvPreguntas.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 11, FontStyle.Bold);

            dgvPreguntas.ColumnHeadersHeight = 45;

            // =====================================================
            // FILAS
            // =====================================================

            dgvPreguntas.DefaultCellStyle.BackColor = Color.White;

            dgvPreguntas.DefaultCellStyle.ForeColor =
                Color.FromArgb(60, 60, 60);

            dgvPreguntas.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(219, 234, 254);

            dgvPreguntas.DefaultCellStyle.SelectionForeColor =
                Color.Black;

            dgvPreguntas.RowTemplate.Height = 38;

            // =====================================================
            // AGREGAR CONTROLES
            // =====================================================

            this.Controls.Add(lblTitulo);
            this.Controls.Add(lblSubtitulo);
            this.Controls.Add(panelFiltros);
            this.Controls.Add(dgvPreguntas);
        }

        // =====================================================
        // CARGAR EXAMENES
        // =====================================================

        private void CargarExamenes()
        {
            try
            {
                _examenes = _examenBll.ObtenerPorUnidad(null);

                cmbExamen.Items.Clear();

                cmbExamen.Items.Add("-- Seleccione un examen --");

                foreach (var ex in _examenes)
                {
                    cmbExamen.Items.Add(
                        $"[{ex.NombreUnidad}] {ex.Nombre}");
                }

                cmbExamen.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar exámenes:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // CARGAR PREGUNTAS
        // =====================================================

        private void CargarPreguntas(int examenId)
        {
            try
            {
                var preguntas =
                    _preguntaBll.ObtenerPorExamen(examenId);

                dgvPreguntas.DataSource = null;
                dgvPreguntas.DataSource = preguntas;

                if (dgvPreguntas.Columns.Contains("ExamenId"))
                    dgvPreguntas.Columns["ExamenId"].Visible = false;

                if (dgvPreguntas.Columns.Contains("RespuestaCorrecta"))
                    dgvPreguntas.Columns["RespuestaCorrecta"].HeaderText =
                        "Respuesta";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar preguntas:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        // =====================================================
        // EVENTO COMBOBOX
        // =====================================================

        private void CmbExamen_SelectedIndexChanged(
            object? sender,
            EventArgs e)
        {
            if (cmbExamen.SelectedIndex <= 0)
            {
                dgvPreguntas.DataSource = null;
                return;
            }

            var examen = _examenes[cmbExamen.SelectedIndex - 1];

            CargarPreguntas(examen.Id);
        }

        // =====================================================
        // AGREGAR PREGUNTA
        // =====================================================

        private void BtnAgregar_Click(
            object? sender,
            EventArgs e)
        {
            if (cmbExamen.SelectedIndex <= 0)
            {
                MessageBox.Show(
                    "Seleccione un examen primero.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            var examen = _examenes[cmbExamen.SelectedIndex - 1];

            var form = new FormPreguntaDetalle(examen.Id);

            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarPreguntas(examen.Id);
            }
        }

        // =====================================================
        // ELIMINAR PREGUNTA
        // =====================================================

        private void BtnEliminar_Click(
            object? sender,
            EventArgs e)
        {
            if (dgvPreguntas.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione una pregunta.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                return;
            }

            var pregunta =
                (Pregunta)dgvPreguntas.CurrentRow.DataBoundItem;

            var resultado = MessageBox.Show(
                "¿Desea eliminar esta pregunta?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    _preguntaBll.Eliminar(pregunta.Id);

                    var examen =
                        _examenes[cmbExamen.SelectedIndex - 1];

                    CargarPreguntas(examen.Id);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        $"Error: {ex.Message}",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
        }
    }

}