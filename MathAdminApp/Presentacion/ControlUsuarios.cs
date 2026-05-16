// ============================================================
// Presentacion: ControlUsuarios (DISEÑO MODERNO RESPONSIVE)
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class ControlUsuarios : UserControl
    {
        // =====================================================
        // CONTROLES
        // =====================================================

        private DataGridView dgvUsuarios = null!;

        private Button btnAgregar = null!;
        private Button btnEditar = null!;
        private Button btnDesactivar = null!;

        private Panel panelBotones = null!;

        private TextBox txtBuscar = null!;

        private readonly UsuarioBLL _bll = new();

        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public ControlUsuarios()
        {
            InicializarComponentes();

            CargarDatos();
        }

        // =====================================================
        // DISEÑO MODERNO
        // =====================================================

        private void InicializarComponentes()
        {
            // =================================================
            // USERCONTROL
            // =================================================

            this.BackColor = Color.FromArgb(245, 250, 255);

            // =================================================
            // TITULO
            // =================================================

            Label lblTitulo = new Label
            {
                Text = "👥 Gestión de Usuarios",

                Font = new Font("Segoe UI", 22, FontStyle.Bold),

                ForeColor = Color.FromArgb(20, 35, 80),

                AutoSize = true,

                Location = new Point(20, 20)
            };

            // =================================================
            // SUBTITULO
            // =================================================

            Label lblSubtitulo = new Label
            {
                Text = "Administra alumnos y profesores fácilmente ✨",

                Font = new Font("Segoe UI", 12),

                ForeColor = Color.FromArgb(100, 120, 150),

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

            btnAgregar.Location = new Point(20, 15);

            btnAgregar.Click += BtnAgregar_Click;

            // =================================================
            // BOTON EDITAR
            // =================================================

            btnEditar = CrearBoton(
                "✏️  Editar",
                Color.FromArgb(52, 199, 89)
            );

            btnEditar.Location = new Point(210, 15);

            btnEditar.Click += BtnEditar_Click;

            // =================================================
            // BOTON DESACTIVAR
            // =================================================

            btnDesactivar = CrearBoton(
                "🚫  Desactivar",
                Color.FromArgb(255, 80, 120)
            );

            btnDesactivar.Location = new Point(400, 15);

            btnDesactivar.Click += BtnDesactivar_Click;

            // =================================================
            // BUSCADOR
            // =================================================

            txtBuscar = new TextBox
            {
                Font = new Font("Segoe UI", 11),

                Size = new Size(260, 40),

                Location = new Point(1100, 18),

                BorderStyle = BorderStyle.FixedSingle,

                BackColor = Color.White,

                ForeColor = Color.FromArgb(120, 120, 140),

                Text = "🔍 Buscar usuario..."
            };

            txtBuscar.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            // =================================================
            // AGREGAR BOTONES
            // =================================================

            panelBotones.Controls.Add(btnAgregar);

            panelBotones.Controls.Add(btnEditar);

            panelBotones.Controls.Add(btnDesactivar);

            panelBotones.Controls.Add(txtBuscar);

            // =================================================
            // PANEL TABLA RESPONSIVE
            // =================================================

            Panel panelTabla = new Panel
            {
                Dock = DockStyle.Fill,

                Padding = new Padding(20, 200, 20, 20),

                BackColor = Color.Transparent
            };

            // =================================================
            // DATA GRID
            // =================================================

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

                AllowUserToResizeRows = false,

                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,

                RowHeadersVisible = false,

                Font = new Font("Segoe UI", 11),

                GridColor = Color.FromArgb(230, 235, 245)
            };

            dgvUsuarios.EnableHeadersVisualStyles = false;

            // =================================================
            // HEADER
            // =================================================

            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(240, 247, 255);

            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(50, 70, 120);

            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 11, FontStyle.Bold);

            dgvUsuarios.ColumnHeadersHeight = 55;

            // =================================================
            // FILAS
            // =================================================

            dgvUsuarios.DefaultCellStyle.BackColor = Color.White;

            dgvUsuarios.DefaultCellStyle.ForeColor =
                Color.FromArgb(60, 70, 100);

            dgvUsuarios.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(220, 235, 255);

            dgvUsuarios.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(20, 35, 80);

            dgvUsuarios.RowTemplate.Height = 50;

            // =================================================
            // AGREGAR TABLA
            // =================================================

            panelTabla.Controls.Add(dgvUsuarios);

            // =================================================
            // AGREGAR CONTROLES
            // =================================================

            this.Controls.Add(panelTabla);

            this.Controls.Add(panelBotones);

            this.Controls.Add(lblTitulo);

            this.Controls.Add(lblSubtitulo);
        }

        // =====================================================
        // CREAR BOTON MODERNO
        // =====================================================

        private Button CrearBoton(string texto, Color color)
        {
            Button btn = new Button
            {
                Text = texto,

                Font = new Font("Segoe UI", 11, FontStyle.Bold),

                BackColor = color,

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Size = new Size(170, 45),

                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;

            // Hover
            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor = ControlPaint.Light(color);
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
                var alumnos = _bll.ObtenerAlumnos();

                dgvUsuarios.DataSource = null;

                dgvUsuarios.DataSource = alumnos;

                // =============================================
                // OCULTAR COLUMNAS
                // =============================================

                if (dgvUsuarios.Columns.Contains("Contrasena"))
                    dgvUsuarios.Columns["Contrasena"].Visible = false;

                if (dgvUsuarios.Columns.Contains("Rol"))
                    dgvUsuarios.Columns["Rol"].Visible = false;

                // =============================================
                // RENOMBRAR COLUMNAS
                // =============================================

                if (dgvUsuarios.Columns.Contains("Id"))
                    dgvUsuarios.Columns["Id"].HeaderText = "🆔 ID";

                if (dgvUsuarios.Columns.Contains("Nombre"))
                    dgvUsuarios.Columns["Nombre"].HeaderText = "👤 Nombre";

                if (dgvUsuarios.Columns.Contains("Correo"))
                    dgvUsuarios.Columns["Correo"].HeaderText = "✉️ Correo";

                if (dgvUsuarios.Columns.Contains("NombreUsuario"))
                    dgvUsuarios.Columns["NombreUsuario"].HeaderText = "💻 Usuario";

                if (dgvUsuarios.Columns.Contains("Grado"))
                    dgvUsuarios.Columns["Grado"].HeaderText = "🎓 Grado";

                if (dgvUsuarios.Columns.Contains("Activo"))
                    dgvUsuarios.Columns["Activo"].HeaderText = "✅ Activo";

                if (dgvUsuarios.Columns.Contains("FechaCreacion"))
                    dgvUsuarios.Columns["FechaCreacion"].HeaderText =
                        "📅 Fecha Registro";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar alumnos:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // AGREGAR
        // =====================================================

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            var form = new FormUsuarioDetalle();

            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarDatos();
            }
        }

        // =====================================================
        // EDITAR
        // =====================================================

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un alumno para editar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var usuario =
                (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;

            var form = new FormUsuarioDetalle(usuario);

            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarDatos();
            }
        }

        // =====================================================
        // DESACTIVAR
        // =====================================================

        private void BtnDesactivar_Click(object? sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un alumno para desactivar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var usuario =
                (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;

            var resultado = MessageBox.Show(
                $"¿Desea desactivar al alumno '{usuario.Nombre}'?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    _bll.DesactivarAlumno(usuario.Id);

                    CargarDatos();

                    MessageBox.Show(
                        "Alumno desactivado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
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