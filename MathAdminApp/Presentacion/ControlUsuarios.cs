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
            this.BackColor = Color.FromArgb(245, 250, 255);

            Label lblTitulo = new Label
            {
                Text = "👥 Gestión de Usuarios",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 80),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            Label lblSubtitulo = new Label
            {
                Text = "Administra usuarios fácilmente ✨",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(100, 120, 150),
                AutoSize = true,
                Location = new Point(25, 65)
            };

            panelBotones = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.Transparent
            };

            btnAgregar = CrearBoton(
                "➕  Agregar",
                Color.FromArgb(66, 133, 244)
            );

            btnAgregar.Location = new Point(20, 15);
            btnAgregar.Click += BtnAgregar_Click;

            btnEditar = CrearBoton(
                "✏️  Editar",
                Color.FromArgb(52, 199, 89)
            );

            btnEditar.Location = new Point(210, 15);
            btnEditar.Click += BtnEditar_Click;

            btnDesactivar = CrearBoton(
                "🚫  Eliminar",
                Color.FromArgb(255, 80, 120)
            );

            btnDesactivar.Location = new Point(400, 15);
            btnDesactivar.Click += BtnDesactivar_Click;

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

            txtBuscar.Anchor =
                AnchorStyles.Top | AnchorStyles.Right;

            txtBuscar.TextChanged += TxtBuscar_TextChanged;

            panelBotones.Controls.Add(btnAgregar);
            panelBotones.Controls.Add(btnEditar);
            panelBotones.Controls.Add(btnDesactivar);
            panelBotones.Controls.Add(txtBuscar);

            Panel panelTabla = new Panel
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(20, 200, 20, 20),
                BackColor = Color.Transparent
            };

            dgvUsuarios = new DataGridView
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
                GridColor = Color.FromArgb(230, 235, 245)
            };

            dgvUsuarios.EnableHeadersVisualStyles = false;

            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(240, 247, 255);

            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(50, 70, 120);

            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 11, FontStyle.Bold);

            dgvUsuarios.ColumnHeadersHeight = 55;

            dgvUsuarios.DefaultCellStyle.BackColor = Color.White;

            dgvUsuarios.DefaultCellStyle.ForeColor =
                Color.FromArgb(60, 70, 100);

            dgvUsuarios.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(220, 235, 255);

            dgvUsuarios.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(20, 35, 80);

            dgvUsuarios.RowTemplate.Height = 50;

            panelTabla.Controls.Add(dgvUsuarios);

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
                var usuarios = _bll.ObtenerUsuarios();

                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = usuarios;

                if (dgvUsuarios.Columns.Contains("Password"))
                    dgvUsuarios.Columns["Password"].Visible = false;

                if (dgvUsuarios.Columns.Contains("IdUsuario"))
                    dgvUsuarios.Columns["IdUsuario"].HeaderText =
                        "🆔 ID";

                if (dgvUsuarios.Columns.Contains("Nombre"))
                    dgvUsuarios.Columns["Nombre"].HeaderText =
                        "👤 Nombre";

                if (dgvUsuarios.Columns.Contains("Username"))
                    dgvUsuarios.Columns["Username"].HeaderText =
                        "💻 Usuario";

                if (dgvUsuarios.Columns.Contains("IdRol"))
                    dgvUsuarios.Columns["IdRol"].HeaderText =
                        "👔 Rol";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al cargar usuarios:\n{ex.Message}",
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
            EventArgs e)
        {
            try
            {
                var usuarios =
                    _bll.ObtenerUsuarios();

                string filtro =
                    txtBuscar.Text
                    .Replace("🔍 Buscar usuario...", "")
                    .Trim()
                    .ToLower();

                if (string.IsNullOrWhiteSpace(filtro))
                {
                    dgvUsuarios.DataSource = usuarios;
                    return;
                }

                dgvUsuarios.DataSource =
                    usuarios
                    .Where(u =>
                        u.Nombre.ToLower().Contains(filtro)
                        || u.Username.ToLower().Contains(filtro))
                    .ToList();
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
            EventArgs e)
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

        private void BtnEditar_Click(
            object? sender,
            EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un usuario para editar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var usuario =
                (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;

            var form =
                new FormUsuarioDetalle(usuario);

            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarDatos();
            }
        }

        // =====================================================
        // ELIMINAR
        // =====================================================

        private void BtnDesactivar_Click(
            object? sender,
            EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un usuario para eliminar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var usuario =
                (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;

            var resultado = MessageBox.Show(
                $"¿Desea eliminar al usuario '{usuario.Nombre}'?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    _bll.EliminarUsuario(
                        usuario.IdUsuario);

                    CargarDatos();

                    MessageBox.Show(
                        "Usuario eliminado correctamente.",
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