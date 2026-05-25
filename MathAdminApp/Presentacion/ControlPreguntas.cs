using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class ControlPreguntas : UserControl
    {
        private ComboBox cmbExamen = null!;
        private DataGridView dgvPreguntas = null!;

        private Button btnAgregar = null!;
        private Button btnEditar = null!;
        private Button btnEliminar = null!;

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
            this.BackColor =
                Color.FromArgb(245, 250, 255);

            // =====================================================
            // TITULO
            // =====================================================

            Label lblTitulo = new Label
            {
                Text = "❓ Gestión de Preguntas",

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
                    "Administra preguntas de exámenes fácilmente ✨",

                Font = new Font("Segoe UI", 12),

                ForeColor =
                    Color.FromArgb(100, 120, 150),

                AutoSize = true,

                Location = new Point(25, 65)
            };

            // =====================================================
            // LABEL EXAMEN
            // =====================================================

            Label lblExamen = new Label
            {
                Text = "📚 Seleccionar examen:",

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
            // COMBO EXAMEN
            // =====================================================

            cmbExamen = new ComboBox
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

            cmbExamen.SelectedIndexChanged +=
                CmbExamen_SelectedIndexChanged;

            // =====================================================
            // BOTON AGREGAR
            // =====================================================

            btnAgregar = CrearBoton(
                "➕ Agregar",
                Color.FromArgb(66, 133, 244)
            );

            btnAgregar.Location =
                new Point(20, 190);

            btnAgregar.Click += BtnAgregar_Click;

            // =====================================================
            // BOTON EDITAR
            // =====================================================

            btnEditar = CrearBoton(
                "✏️ Editar",
                Color.FromArgb(52, 199, 89)
            );

            btnEditar.Location =
                new Point(210, 190);

            btnEditar.Click += BtnEditar_Click;

            // =====================================================
            // BOTON ELIMINAR
            // =====================================================

            btnEliminar = CrearBoton(
                "🗑️ Eliminar",
                Color.FromArgb(255, 80, 120)
            );

            btnEliminar.Location =
                new Point(400, 190);

            btnEliminar.Click += BtnEliminar_Click;

            // =====================================================
            // DATA GRID
            // =====================================================

            dgvPreguntas = new DataGridView
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

            dgvPreguntas.EnableHeadersVisualStyles =
                false;

            // =====================================================
            // HEADER
            // =====================================================

            dgvPreguntas.ColumnHeadersDefaultCellStyle
                .BackColor =
                Color.FromArgb(240, 247, 255);

            dgvPreguntas.ColumnHeadersDefaultCellStyle
                .ForeColor =
                Color.FromArgb(50, 70, 120);

            dgvPreguntas.ColumnHeadersDefaultCellStyle
                .Font =
                new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold
                );

            dgvPreguntas.ColumnHeadersHeight = 55;

            // =====================================================
            // FILAS
            // =====================================================

            dgvPreguntas.DefaultCellStyle.BackColor =
                Color.White;

            dgvPreguntas.DefaultCellStyle.ForeColor =
                Color.FromArgb(60, 70, 100);

            dgvPreguntas.DefaultCellStyle
                .SelectionBackColor =
                Color.FromArgb(220, 235, 255);

            dgvPreguntas.DefaultCellStyle
                .SelectionForeColor =
                Color.FromArgb(20, 35, 80);

            dgvPreguntas.RowTemplate.Height = 50;

            // =====================================================
            // CONTROLES
            // =====================================================

            this.Controls.Add(lblTitulo);

            this.Controls.Add(lblSubtitulo);

            this.Controls.Add(lblExamen);

            this.Controls.Add(cmbExamen);

            this.Controls.Add(btnAgregar);

            this.Controls.Add(btnEditar);

            this.Controls.Add(btnEliminar);

            this.Controls.Add(dgvPreguntas);
        }

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

        private void CargarExamenes()
        {
            _examenes =
                _examenBll.ObtenerPorUnidad(null);

            cmbExamen.Items.Clear();

            cmbExamen.Items.Add(
                "-- Seleccione un examen --"
            );

            foreach (var ex in _examenes)
            {
                cmbExamen.Items.Add(
                    $"[{ex.NombreUnidad}] {ex.Nombre}"
                );
            }

            cmbExamen.SelectedIndex = 0;
        }

        private void CargarPreguntas(int examenId)
        {
            var preguntas =
                _preguntaBll.ObtenerPorExamen(
                    examenId
                );

            dgvPreguntas.DataSource = null;

            dgvPreguntas.DataSource = preguntas;
        }

        private void CmbExamen_SelectedIndexChanged(
            object? sender,
            EventArgs e
        )
        {
            if (cmbExamen.SelectedIndex <= 0)
            {
                dgvPreguntas.DataSource = null;
                return;
            }

            var examen =
                _examenes[
                    cmbExamen.SelectedIndex - 1
                ];

            CargarPreguntas(examen.Id);
        }

        private void BtnAgregar_Click(
            object? sender,
            EventArgs e
        )
        {
            if (cmbExamen.SelectedIndex <= 0)
            {
                MessageBox.Show(
                    "Seleccione un examen."
                );

                return;
            }

            var examen =
                _examenes[
                    cmbExamen.SelectedIndex - 1
                ];

            var form =
                new FormPreguntaDetalle(
                    examen.Id
                );

            if (form.ShowDialog() ==
                DialogResult.OK)
            {
                CargarPreguntas(examen.Id);
            }
        }

        private void BtnEditar_Click(
            object? sender,
            EventArgs e
        )
        {
            if (dgvPreguntas.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione una pregunta."
                );

                return;
            }

            var pregunta =
                (Pregunta)dgvPreguntas
                .CurrentRow
                .DataBoundItem;

            var form =
                new FormPreguntaDetalle(
                    pregunta.ExamenId,
                    pregunta
                );

            if (form.ShowDialog() ==
                DialogResult.OK)
            {
                CargarPreguntas(
                    pregunta.ExamenId
                );
            }
        }

        private void BtnEliminar_Click(
            object? sender,
            EventArgs e
        )
        {
            MessageBox.Show(
                "Aquí puedes agregar la lógica para eliminar."
            );
        }
    }
}