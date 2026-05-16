// ============================================================
// Presentacion: ControlUnidades (MODERNO)
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class ControlUnidades : UserControl
    {
        // =====================================================
        // CONTROLES
        // =====================================================

        private DataGridView dgvUnidades = null!;

        private Button btnAgregar = null!;

        private Button btnEditar = null!;

        private Button btnEliminar = null!;

        private TextBox txtBuscar = null!;

        private readonly UnidadBLL _bll = new();

        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public ControlUnidades()
        {
            InicializarComponentes();

            CargarDatos();
        }

        // =====================================================
        // INTERFAZ
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

                Height = 170,

                BackColor = Color.Transparent
            };

            // =================================================
            // TITULO
            // =================================================

            Label lblTitulo = new Label
            {
                Text = "📘 Gestión de Temas",

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
                    "Administra fácilmente las unidades temáticas ✨",

                Font = new Font("Segoe UI", 13),

                ForeColor =
                    Color.FromArgb(110, 120, 150),

                AutoSize = true,

                Location = new Point(30, 65)
            };

            // =================================================
            // BOTON AGREGAR
            // =================================================

            btnAgregar = CrearBoton(
                "➕ Agregar",
                Color.FromArgb(50, 120, 255)
            );

            btnAgregar.Location =
                new Point(30, 110);

            btnAgregar.Click += BtnAgregar_Click;

            // =================================================
            // BOTON EDITAR
            // =================================================

            btnEditar = CrearBoton(
                "✏️ Editar",
                Color.FromArgb(255, 179, 0)
            );

            btnEditar.Location =
                new Point(230, 110);

            btnEditar.Click += BtnEditar_Click;

            // =================================================
            // BOTON ELIMINAR
            // =================================================

            btnEliminar = CrearBoton(
                "🗑 Eliminar",
                Color.FromArgb(255, 70, 120)
            );

            btnEliminar.Location =
                new Point(430, 110);

            btnEliminar.Click += BtnEliminar_Click;

            // =================================================
            // BUSCADOR
            // =================================================

            txtBuscar = new TextBox
            {
                PlaceholderText = "🔍 Buscar tema...",

                Font = new Font("Segoe UI", 12),

                Size = new Size(280, 45),

                Location = new Point(820, 115),

                BorderStyle = BorderStyle.FixedSingle,

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(60, 70, 100)
            };

            txtBuscar.TextChanged += TxtBuscar_TextChanged;

            // =================================================
            // AGREGAR CONTROLES
            // =================================================

            panelSuperior.Controls.Add(lblTitulo);

            panelSuperior.Controls.Add(lblSubtitulo);

            panelSuperior.Controls.Add(btnAgregar);

            panelSuperior.Controls.Add(btnEditar);

            panelSuperior.Controls.Add(btnEliminar);

            panelSuperior.Controls.Add(txtBuscar);

            // =================================================
            // TABLA MODERNA
            // =================================================

            dgvUnidades = new DataGridView
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

            dgvUnidades.EnableHeadersVisualStyles = false;

            // =================================================
            // HEADER
            // =================================================

            dgvUnidades.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(245, 248, 255);

            dgvUnidades.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(20, 35, 90);

            dgvUnidades.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    12,
                    FontStyle.Bold
                );

            dgvUnidades.ColumnHeadersHeight = 60;

            dgvUnidades.ColumnHeadersDefaultCellStyle.Alignment =
                DataGridViewContentAlignment.MiddleLeft;

            // =================================================
            // FILAS
            // =================================================

            dgvUnidades.DefaultCellStyle.BackColor =
                Color.White;

            dgvUnidades.DefaultCellStyle.ForeColor =
                Color.FromArgb(40, 50, 80);

            dgvUnidades.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(230, 240, 255);

            dgvUnidades.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(20, 35, 90);

            dgvUnidades.DefaultCellStyle.Padding =
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

            panelTabla.Controls.Add(dgvUnidades);

            // =================================================
            // AGREGAR
            // =================================================

            this.Controls.Add(panelTabla);

            this.Controls.Add(panelSuperior);
        }

        // =====================================================
        // BOTONES MODERNOS
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
        // CARGAR DATOS
        // =====================================================

        private void CargarDatos()
        {
            try
            {
                var unidades =
                    _bll.ObtenerTodas();

                dgvUnidades.DataSource = null;

                dgvUnidades.DataSource = unidades;

                // =================================================
                // RENOMBRAR COLUMNAS
                // =================================================

                if (dgvUnidades.Columns.Contains("Id"))
                    dgvUnidades.Columns["Id"]
                        .HeaderText = "ID";

                if (dgvUnidades.Columns.Contains("NumeroUnidad"))
                    dgvUnidades.Columns["NumeroUnidad"]
                        .HeaderText = "No. Unidad";

                if (dgvUnidades.Columns.Contains("Descripcion"))
                    dgvUnidades.Columns["Descripcion"]
                        .HeaderText = "Descripción";

                // =================================================
                // TAMAÑOS
                // =================================================

                dgvUnidades.Columns["Id"].FillWeight = 15;

                dgvUnidades.Columns["NumeroUnidad"].FillWeight = 20;

                dgvUnidades.Columns["Nombre"].FillWeight = 35;

                dgvUnidades.Columns["Descripcion"].FillWeight = 60;
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

                var unidades =
                    _bll.ObtenerTodas();

                if (!string.IsNullOrWhiteSpace(texto))
                {
                    unidades = unidades
                        .Where(u =>
                            u.Nombre.ToLower().Contains(texto)
                            ||
                            u.Descripcion.ToLower().Contains(texto)
                        )
                        .ToList();
                }

                dgvUnidades.DataSource = null;

                dgvUnidades.DataSource = unidades;
            }
            catch
            {
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
                new FormUnidadDetalle();

            if (form.ShowDialog() == DialogResult.OK)
                CargarDatos();
        }

        // =====================================================
        // EDITAR
        // =====================================================

        private void BtnEditar_Click(
            object? sender,
            EventArgs e
        )
        {
            if (dgvUnidades.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione una unidad.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var unidad =
                (Unidad)dgvUnidades
                    .CurrentRow
                    .DataBoundItem;

            var form =
                new FormUnidadDetalle(unidad);

            if (form.ShowDialog() == DialogResult.OK)
                CargarDatos();
        }

        // =====================================================
        // ELIMINAR
        // =====================================================

        private void BtnEliminar_Click(
            object? sender,
            EventArgs e
        )
        {
            if (dgvUnidades.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione una unidad.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var unidad =
                (Unidad)dgvUnidades
                    .CurrentRow
                    .DataBoundItem;

            var resultado =
                MessageBox.Show(
                    $"¿Desea eliminar la unidad '{unidad.Nombre}'?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    _bll.Eliminar(unidad.Id);

                    CargarDatos();
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