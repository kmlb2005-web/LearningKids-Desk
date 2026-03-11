// ============================================================
// Presentacion: ControlUnidades (UserControl)
// Vista para gestionar unidades tematicas (CRUD)
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;
using System.Drawing.Drawing2D;

namespace MathAdminApp.Presentacion
{
    public class ControlUnidades : UserControl
    {
        private DataGridView dgvUnidades = null!;
        private Button btnAgregar = null!;
        private Button btnEditar = null!;
        private Button btnEliminar = null!;
        private Panel panelBotones = null!;
        private Panel panelBusqueda = null!;
        private TextBox txtBuscar = null!;
        private Button btnBuscar = null!;
        private Button btnLimpiar = null!;
        private Label lblBuscar = null!;

        private readonly UnidadBLL _bll = new();

        public ControlUnidades()
        {
            InicializarComponentes();
            CargarDatos();
        }

        private void InicializarComponentes()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);

            // =============================
            // PANEL BUSQUEDA
            // =============================

            panelBusqueda = new Panel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Color.FromArgb(248, 249, 250),
                Padding = new Padding(15)
            };

            lblBuscar = new Label
            {
                Text = "Buscar unidad:",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                AutoSize = true,
                Location = new Point(5, 20)
            };

            txtBuscar = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(250, 30),
                Location = new Point(120, 18)
            };

            btnBuscar = CrearBoton("Buscar", Color.FromArgb(255, 179, 0));
            btnBuscar.Size = new Size(90, 32);
            btnBuscar.Location = new Point(380, 17);
            btnBuscar.Click += BtnBuscar_Click;

            btnLimpiar = CrearBoton("Limpiar", Color.FromArgb(255, 179, 0));
            btnLimpiar.Size = new Size(90, 32);
            btnLimpiar.Location = new Point(480, 17);
            btnLimpiar.Click += BtnLimpiar_Click;

            panelBusqueda.Controls.Add(lblBuscar);
            panelBusqueda.Controls.Add(txtBuscar);
            panelBusqueda.Controls.Add(btnBuscar);
            panelBusqueda.Controls.Add(btnLimpiar);

            // =============================
            // PANEL BOTONES
            // =============================

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

            // =============================
            // TABLA
            // =============================

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

            dgvUnidades.EnableHeadersVisualStyles = false;

            dgvUnidades.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 241, 118);
            dgvUnidades.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvUnidades.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 179, 0);
            dgvUnidades.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvUnidades.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvUnidades.ColumnHeadersHeight = 40;

            dgvUnidades.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 202, 40);
            dgvUnidades.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvUnidades.RowTemplate.Height = 35;

            this.Controls.Add(dgvUnidades);
            this.Controls.Add(panelBotones);
            this.Controls.Add(panelBusqueda);
        }

        // =============================
        // CREAR BOTON
        // =============================

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

            btn.Paint += (s, e) =>
            {
                RedondearBoton(btn, 20);
            };

            return btn;
        }

        // =============================
        // BOTONES REDONDOS
        // =============================

        private void RedondearBoton(Button boton, int radio)
        {
            GraphicsPath path = new GraphicsPath();
            path.StartFigure();

            path.AddArc(new Rectangle(0, 0, radio, radio), 180, 90);
            path.AddArc(new Rectangle(boton.Width - radio, 0, radio, radio), 270, 90);
            path.AddArc(new Rectangle(boton.Width - radio, boton.Height - radio, radio, radio), 0, 90);
            path.AddArc(new Rectangle(0, boton.Height - radio, radio, radio), 90, 90);

            path.CloseFigure();

            boton.Region = new Region(path);
        }

        // =============================
        // CARGAR DATOS
        // =============================

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

        // =============================
        // BUSCAR
        // =============================

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            var texto = txtBuscar.Text.ToLower();

            var lista = _bll.ObtenerTodas()
                .Where(u => u.Nombre.ToLower().Contains(texto))
                .ToList();

            dgvUnidades.DataSource = lista;
        }

        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarDatos();
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