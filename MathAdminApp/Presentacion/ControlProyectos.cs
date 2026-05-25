using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public partial class ControlProyectos : UserControl
    {
        // =====================================================
        // CONTROLES
        // =====================================================

        private DataGridView dgvProyectos = null!;

        private Button btnAgregar = null!;
        private Button btnEditar = null!;
        private Button btnDesactivar = null!;

        private Panel panelBotones = null!;

        private readonly ProyectoBLL _bll = new();

        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public ControlProyectos()
        {
            InicializarComponentes();

            CargarDatos();
        }

        // =====================================================
        // DISEÑO
        // =====================================================

        private void InicializarComponentes()
        {
            this.BackColor = Color.FromArgb(245, 250, 255);

            // =================================================
            // TITULO
            // =================================================

            Label lblTitulo = new Label
            {
                Text = "📁 Gestión de Proyectos",

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

            // =================================================
            // SUBTITULO
            // =================================================

            Label lblSubtitulo = new Label
            {
                Text =
                    "Administra proyectos fácilmente ✨",

                Font = new Font("Segoe UI", 12),

                ForeColor =
                    Color.FromArgb(100, 120, 150),

                AutoSize = true,

                Location = new Point(25, 65)
            };

            // =================================================
            // PANEL BOTONES
            // =================================================

            panelBotones = new Panel
            {
                Dock = DockStyle.Top,

                Height = 80,

                BackColor = Color.Transparent
            };

            // =================================================
            // BOTON AGREGAR
            // =================================================

            btnAgregar = CrearBoton(
                "➕  Agregar",
                Color.FromArgb(66, 133, 244)
            );

            btnAgregar.Location =
                new Point(20, 15);

            btnAgregar.Click += BtnAgregar_Click;

            // =================================================
            // BOTON EDITAR
            // =================================================

            btnEditar = CrearBoton(
                "✏️  Editar",
                Color.FromArgb(52, 199, 89)
            );

            btnEditar.Location =
                new Point(210, 15);

            btnEditar.Click += BtnEditar_Click;

            // =================================================
            // BOTON DESACTIVAR
            // =================================================

            btnDesactivar = CrearBoton(
                "🚫  Eliminar",
                Color.FromArgb(255, 80, 120)
            );

            btnDesactivar.Location =
                new Point(400, 15);

            btnDesactivar.Click +=
                BtnDesactivar_Click;

            panelBotones.Controls.Add(
                btnAgregar
            );

            panelBotones.Controls.Add(
                btnEditar
            );

            panelBotones.Controls.Add(
                btnDesactivar
            );

            // =================================================
            // PANEL TABLA
            // =================================================

            Panel panelTabla = new Panel
            {
                Dock = DockStyle.Fill,

                Padding =
                    new Padding(20, 200, 20, 20),

                BackColor = Color.Transparent
            };

            // =================================================
            // DATA GRID
            // =================================================

            dgvProyectos = new DataGridView
            {
                Dock = DockStyle.Fill,

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

                Font = new Font(
                    "Segoe UI",
                    11
                ),

                GridColor =
                    Color.FromArgb(230, 235, 245)
            };

            dgvProyectos.EnableHeadersVisualStyles =
                false;

            // HEADER

            dgvProyectos
                .ColumnHeadersDefaultCellStyle
                .BackColor =
                Color.FromArgb(240, 247, 255);

            dgvProyectos
                .ColumnHeadersDefaultCellStyle
                .ForeColor =
                Color.FromArgb(50, 70, 120);

            dgvProyectos
                .ColumnHeadersDefaultCellStyle
                .Font =
                new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold
                );

            dgvProyectos.ColumnHeadersHeight = 55;

            // FILAS

            dgvProyectos.DefaultCellStyle.BackColor =
                Color.White;

            dgvProyectos.DefaultCellStyle.ForeColor =
                Color.FromArgb(60, 70, 100);

            dgvProyectos
                .DefaultCellStyle
                .SelectionBackColor =
                Color.FromArgb(220, 235, 255);

            dgvProyectos
                .DefaultCellStyle
                .SelectionForeColor =
                Color.FromArgb(20, 35, 80);

            dgvProyectos.RowTemplate.Height = 50;

            panelTabla.Controls.Add(
                dgvProyectos
            );

            // =================================================
            // AGREGAR CONTROLES
            // =================================================

            this.Controls.Add(panelTabla);

            this.Controls.Add(panelBotones);

            this.Controls.Add(lblTitulo);

            this.Controls.Add(lblSubtitulo);
        }

        // =====================================================
        // BOTON
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
        // CARGAR DATOS
        // =====================================================

        private void CargarDatos()
        {
            try
            {
                var proyectos =
                    _bll.ObtenerTodos();

                dgvProyectos.DataSource = null;

                dgvProyectos.DataSource =
                    proyectos;

                if (
                    dgvProyectos.Columns.Contains(
                        "idProyecto"
                    )
                )
                {
                    dgvProyectos.Columns[
                        "idProyecto"
                    ].Visible = false;
                }

                dgvProyectos.Columns["nombre"]
                    .HeaderText = "📁 Nombre";

                dgvProyectos.Columns["descripcion"]
                    .HeaderText =
                    "📝 Descripción";

                dgvProyectos.Columns["unidad"]
                    .HeaderText = "📚 Unidad";

                dgvProyectos.Columns[
                    "fechaCreacion"
                ].HeaderText =
                    "📅 Fecha";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // AGREGAR
        // =====================================================

        private void BtnAgregar_Click(
            object? sender,
            EventArgs e
        )
        {
            var form =
                new FormProyectoDetalle();

            if (
                form.ShowDialog()
                == DialogResult.OK
            )
            {
                CargarDatos();
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
            if (dgvProyectos.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un proyecto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var proyecto =
                (Proyecto)dgvProyectos
                .CurrentRow.DataBoundItem;

            var form =
                new FormProyectoDetalle(
                    proyecto
                );

            if (
                form.ShowDialog()
                == DialogResult.OK
            )
            {
                CargarDatos();
            }
        }

        // =====================================================
        // ELIMINAR
        // =====================================================

        private void BtnDesactivar_Click(
            object? sender,
            EventArgs e
        )
        {
            if (dgvProyectos.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un proyecto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var proyecto =
                (Proyecto)dgvProyectos
                .CurrentRow.DataBoundItem;

            var resultado =
                MessageBox.Show(
                    $"¿Desea eliminar el proyecto '{proyecto.nombre}'?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (resultado == DialogResult.Yes)
            {
                _bll.Eliminar(
                    proyecto.idProyecto
                );

                CargarDatos();
            }
        }
    }
}