// ============================================================
// Presentacion: ControlUsuarios (UserControl)
// Vista para gestionar alumnos (CRUD)
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;
using System.Drawing.Drawing2D;

namespace MathAdminApp.Presentacion
{
    public class ControlUsuarios : UserControl
    {
        private DataGridView dgvUsuarios = null!;
        private Button btnAgregar = null!;
        private Button btnEditar = null!;
        private Button btnDesactivar = null!;
        private Panel panelBotones = null!;
        private Panel panelBusqueda = null!;
        private TextBox txtBuscar = null!;
        private Button btnBuscar = null!;
        private Button btnLimpiar = null!;
        private Label lblBuscar = null!;

        private readonly UsuarioBLL _bll = new();

        public ControlUsuarios()
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
                Text = "Buscar alumno:",
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

            btnBuscar = CrearBoton("Buscar");
            btnBuscar.Size = new Size(90, 32);
            btnBuscar.Location = new Point(380, 17);
            btnBuscar.Click += BtnBuscar_Click;

            btnLimpiar = CrearBoton("Limpiar");
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

            btnAgregar = CrearBoton("Agregar Alumno");
            btnAgregar.Location = new Point(0, 8);
            btnAgregar.Click += BtnAgregar_Click;

            btnEditar = CrearBoton("Editar");
            btnEditar.Location = new Point(170, 8);
            btnEditar.Click += BtnEditar_Click;

            btnDesactivar = CrearBoton("Desactivar");
            btnDesactivar.Location = new Point(310, 8);
            btnDesactivar.Click += BtnDesactivar_Click;

            panelBotones.Controls.Add(btnAgregar);
            panelBotones.Controls.Add(btnEditar);
            panelBotones.Controls.Add(btnDesactivar);

            // =============================
            // TABLA
            // =============================

            dgvUsuarios = new DataGridView
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

            dgvUsuarios.EnableHeadersVisualStyles = false;

            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 241, 118);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 179, 0);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);

            dgvUsuarios.ColumnHeadersHeight = 40;

            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 202, 40);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgvUsuarios.RowTemplate.Height = 35;

            this.Controls.Add(dgvUsuarios);
            this.Controls.Add(panelBotones);
            this.Controls.Add(panelBusqueda);
        }

        // =============================
        // CREAR BOTON
        // =============================

        private Button CrearBoton(string texto)
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
            dgvUsuarios.DataSource = null;
            dgvUsuarios.DataSource = _bll.ObtenerAlumnos();

            if (dgvUsuarios.Columns.Contains("Contrasena"))
                dgvUsuarios.Columns["Contrasena"].Visible = false;

            if (dgvUsuarios.Columns.Contains("Rol"))
                dgvUsuarios.Columns["Rol"].Visible = false;

            if (dgvUsuarios.Columns.Contains("NombreUsuario"))
                dgvUsuarios.Columns["NombreUsuario"].HeaderText = "Usuario";

            if (dgvUsuarios.Columns.Contains("FechaCreacion"))
                dgvUsuarios.Columns["FechaCreacion"].HeaderText = "Fecha Registro";
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            var texto = txtBuscar.Text.ToLower();

            var lista = _bll.ObtenerAlumnos()
                .Where(a => a.Nombre.ToLower().Contains(texto) ||
                            a.NombreUsuario.ToLower().Contains(texto))
                .ToList();

            dgvUsuarios.DataSource = lista;
        }

        private void BtnLimpiar_Click(object? sender, EventArgs e)
        {
            txtBuscar.Clear();
            CargarDatos();
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            var form = new FormUsuarioDetalle();

            if (form.ShowDialog() == DialogResult.OK)
                CargarDatos();
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;

            var usuario = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;

            var form = new FormUsuarioDetalle(usuario);

            if (form.ShowDialog() == DialogResult.OK)
                CargarDatos();
        }

        private void BtnDesactivar_Click(object? sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null) return;

            var usuario = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;

            if (MessageBox.Show($"Desea desactivar a {usuario.Nombre}?",
                "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                _bll.DesactivarAlumno(usuario.Id);
                CargarDatos();
            }
        }
    }
}