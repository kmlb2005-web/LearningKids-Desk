// ============================================================
// Presentacion: ControlUnidades (UserControl)
// Vista para gestionar unidades tematicas (CRUD)
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    /// <summary>
    /// Control de usuario para la gestion de unidades.
    /// </summary>
    public class ControlUnidades : UserControl
    {
        private DataGridView dgvUnidades = null!;
        private Button btnAgregar = null!;
        private Button btnEditar = null!;
        private Button btnEliminar = null!;
        private Panel panelBotones = null!;
        private readonly UnidadBLL _bll = new();

        public ControlUnidades()
        {
            InicializarComponentes();
            CargarDatos();
        }

        private void InicializarComponentes()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);

            // --- Barra de botones ---
            panelBotones = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 5, 0, 5)
            };

            btnAgregar = CrearBoton("Agregar Tema", Color.FromArgb(63, 81, 181));
            btnAgregar.Location = new Point(0, 8);
            btnAgregar.Click += BtnAgregar_Click;

            btnEditar = CrearBoton("Editar", Color.FromArgb(0, 150, 136));
            btnEditar.Location = new Point(170, 8);
            btnEditar.Click += BtnEditar_Click;

            btnEliminar = CrearBoton("Eliminar", Color.FromArgb(211, 47, 47));
            btnEliminar.Location = new Point(310, 8);
            btnEliminar.Click += BtnEliminar_Click;

            panelBotones.Controls.Add(btnAgregar);
            panelBotones.Controls.Add(btnEditar);
            panelBotones.Controls.Add(btnEliminar);

            // --- Tabla ---
            dgvUnidades = new DataGridView
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
                GridColor = Color.FromArgb(255, 179, 0)
            };

            // Desactivar estilo visual del sistema
            dgvUnidades.EnableHeadersVisualStyles = false;

            // --- Encabezado ---
            dgvUnidades.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 241, 118); // Amarillo claro
            dgvUnidades.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvUnidades.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 179, 0);
            dgvUnidades.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvUnidades.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvUnidades.ColumnHeadersHeight = 40;


            // --- Selección de filas ---
            dgvUnidades.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 202, 40);
            dgvUnidades.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvUnidades.RowTemplate.Height = 35;

            this.Controls.Add(dgvUnidades);
            this.Controls.Add(panelBotones);
        }

        private Button CrearBoton(string texto, Color color)
        {  
              var btn = new Button
                {
                    Text = texto,
                    Font = new Font("Segoe UI", 10, FontStyle.Bold),
                    BackColor = Color.FromArgb(255, 179, 0),
                    ForeColor = Color.White,
                    FlatStyle = FlatStyle.Flat,
                    Size = new Size(130, 35),
                    Cursor = Cursors.Hand
                };
                btn.FlatAppearance.BorderSize = 0;
                return btn;
            }

        private void CargarDatos()
        {
            try
            {
                var unidades = _bll.ObtenerTodas();
                dgvUnidades.DataSource = null;
                dgvUnidades.DataSource = unidades;

                if (dgvUnidades.Columns.Contains("Id"))
                    dgvUnidades.Columns["Id"].HeaderText = "ID";
                if (dgvUnidades.Columns.Contains("NumeroUnidad"))
                    dgvUnidades.Columns["NumeroUnidad"].HeaderText = "No. Unidad";
                if (dgvUnidades.Columns.Contains("Descripcion"))
                    dgvUnidades.Columns["Descripcion"].HeaderText = "Descripcion";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar unidades:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            var form = new FormUnidadDetalle();
            if (form.ShowDialog() == DialogResult.OK)
                CargarDatos();
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            if (dgvUnidades.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una unidad.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var unidad = (Unidad)dgvUnidades.CurrentRow.DataBoundItem;
            var form = new FormUnidadDetalle(unidad);
            if (form.ShowDialog() == DialogResult.OK)
                CargarDatos();
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvUnidades.CurrentRow == null)
            {
                MessageBox.Show("Seleccione una unidad.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var unidad = (Unidad)dgvUnidades.CurrentRow.DataBoundItem;
            var resultado = MessageBox.Show($"Desea eliminar la unidad '{unidad.Nombre}'?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    _bll.Eliminar(unidad.Id);
                    CargarDatos();
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
