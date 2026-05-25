// ============================================================
// FORMULARIO MODERNO - NUEVO ALUMNO
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class FormUsuarioDetalle : Form
    {
        // =====================================================
        // VARIABLES
        // =====================================================

        private readonly UsuarioBLL _bll = new();

        private readonly Usuario? _usuario;
        private readonly Usuario? _usuarioActual;

        private readonly bool _esEdicion;

        // =====================================================
        // CONTROLES
        // =====================================================

        private TextBox txtNombre = null!;
        private TextBox txtCorreo = null!;
        private TextBox txtUsuario = null!;
        private TextBox txtContrasena = null!;

        private ComboBox cmbGrado = null!;

        private Button btnGuardar = null!;
        private Button btnCancelar = null!;

        // =====================================================
        // CONSTRUCTORES
        // =====================================================

        public FormUsuarioDetalle() : this(null, null) { }

        public FormUsuarioDetalle(Usuario? usuario) : this(usuario, null) { }

        public FormUsuarioDetalle(Usuario? usuario, Usuario? usuarioActual)
        {
            _usuario = usuario;
            _usuarioActual = usuarioActual;

            _esEdicion = usuario != null;

            InicializarComponentes();

            if (_esEdicion)
                CargarDatos();
        }

        // =====================================================
        // DISEÑO
        // =====================================================

        private void InicializarComponentes()
        {
            // =================================================
            // FORMULARIO
            // =================================================

            this.Text = _esEdicion
                ? "Editar Alumno"
                : "Nuevo Alumno";

            this.Size = new Size(950, 820);

            this.StartPosition = FormStartPosition.CenterParent;

            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.MaximizeBox = false;

            this.MinimizeBox = false;

            this.BackColor = Color.White;

            this.AutoScroll = true;

            // =================================================
            // PANEL PRINCIPAL
            // =================================================

            Panel panel = new Panel
            {
                Dock = DockStyle.Fill,

                BackColor = Color.White,

                AutoScroll = true
            };

            // =================================================
            // ROBOT
            // =================================================

            PictureBox picRobot = new PictureBox
            {
                Image = Image.FromFile("Resources/Louz.png"),

                SizeMode = PictureBoxSizeMode.Zoom,

                Size = new Size(130, 130),

                Location = new Point(45, 35),

                BackColor = Color.Transparent
            };

            // =================================================
            // TITULO
            // =================================================

            Label lblTitulo = new Label
            {
                Text = _esEdicion
                    ? "✏️ Editar Alumno"
                    : "➕ Nuevo Alumno",

                Font = new Font(
                    "Segoe UI",
                    34,
                    FontStyle.Bold
                ),

                ForeColor = Color.FromArgb(20, 35, 90),

                AutoSize = true,

                Location = new Point(210, 50)
            };

            // =================================================
            // SUBTITULO
            // =================================================

            Label lblSubtitulo = new Label
            {
                Text =
                    "Completa la información del alumno ✨",

                Font = new Font("Segoe UI", 16),

                ForeColor =
                    Color.FromArgb(120, 130, 160),

                AutoSize = true,

                Location = new Point(220, 115)
            };

            // =================================================
            // POSICIONES
            // =================================================

            int x = 70;

            int width = 760;

            int y = 220;

            // =================================================
            // NOMBRE
            // =================================================

            panel.Controls.Add(
                CrearLabel(
                    "👤 Nombre completo",
                    x,
                    y
                )
            );

            y += 45;

            txtNombre = CrearTextBox(
                x,
                y,
                width
            );

            txtNombre.PlaceholderText =
                "Escribe el nombre completo del alumno...";

            panel.Controls.Add(txtNombre);

            // =================================================
            // CORREO
            // =================================================

            y += 115;

            panel.Controls.Add(
                CrearLabel(
                    "✉️ Correo electrónico",
                    x,
                    y
                )
            );

            y += 45;

            txtCorreo = CrearTextBox(
                x,
                y,
                width
            );

            txtCorreo.PlaceholderText =
                "ejemplo@correo.com";

            panel.Controls.Add(txtCorreo);

            // =================================================
            // USUARIO
            // =================================================

            y += 115;

            panel.Controls.Add(
                CrearLabel(
                    "💻 Nombre de usuario",
                    x,
                    y
                )
            );

            y += 45;

            txtUsuario = CrearTextBox(
                x,
                y,
                width
            );

            txtUsuario.PlaceholderText =
                "Escribe el nombre de usuario...";

            txtUsuario.Enabled = !_esEdicion;

            panel.Controls.Add(txtUsuario);

            // =================================================
            // CONTRASEÑA
            // =================================================

            y += 115;

            Label lblPass = CrearLabel(
                "🔒 Contraseña",
                x,
                y
            );

            txtContrasena = CrearTextBox(
                x,
                y + 45,
                width
            );

            txtContrasena.PlaceholderText =
                "Escribe la contraseña...";

            txtContrasena.UseSystemPasswordChar = true;

            if (!_esEdicion)
            {
                panel.Controls.Add(lblPass);

                panel.Controls.Add(txtContrasena);

                y += 115;
            }

            // =================================================
            // GRADO
            // =================================================

            y += 20;

            panel.Controls.Add(
                CrearLabel(
                    "🎓 Grado",
                    x,
                    y
                )
            );

            y += 45;

            cmbGrado = new ComboBox
            {
                Font = new Font("Segoe UI", 14),

                Location = new Point(x, y),

                Size = new Size(width, 55),

                DropDownStyle = ComboBoxStyle.DropDownList,

                FlatStyle = FlatStyle.Flat,

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(30, 50, 90)
            };

            cmbGrado.Items.AddRange(
                new[]
                {
                    "1ro",
                    "2do",
                    "3ro",
                    "4to",
                    "5to",
                    "6to"
                }
            );

            cmbGrado.SelectedIndex = 5;

            panel.Controls.Add(cmbGrado);

            // =================================================
            // LINEA
            // =================================================

            Panel linea = new Panel
            {
                BackColor =
                    Color.FromArgb(230, 235, 245),

                Size = new Size(760, 2),

                Location = new Point(70, y + 90)
            };

            panel.Controls.Add(linea);

            // =================================================
            // BOTON GUARDAR
            // =================================================

            btnGuardar = new Button
            {
                Text = "💾  Guardar",

                Font = new Font(
                    "Segoe UI",
                    14,
                    FontStyle.Bold
                ),

                BackColor =
                    Color.FromArgb(50, 120, 255),

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Size = new Size(260, 65),

                Location = new Point(230, y + 125),

                Cursor = Cursors.Hand
            };

            btnGuardar.FlatAppearance.BorderSize = 0;

            btnGuardar.Click += BtnGuardar_Click;

            // Hover
            btnGuardar.MouseEnter += (s, e) =>
            {
                btnGuardar.BackColor =
                    Color.FromArgb(70, 140, 255);
            };

            btnGuardar.MouseLeave += (s, e) =>
            {
                btnGuardar.BackColor =
                    Color.FromArgb(50, 120, 255);
            };

            // =================================================
            // BOTON CANCELAR
            // =================================================

            btnCancelar = new Button
            {
                Text = "❌  Cancelar",

                Font = new Font(
                    "Segoe UI",
                    14,
                    FontStyle.Bold
                ),

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(255, 70, 120),

                FlatStyle = FlatStyle.Flat,

                Size = new Size(260, 65),

                Location = new Point(520, y + 125),

                Cursor = Cursors.Hand
            };

            btnCancelar.FlatAppearance.BorderColor =
                Color.FromArgb(255, 70, 120);

            btnCancelar.FlatAppearance.BorderSize = 2;

            btnCancelar.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;

                this.Close();
            };

            // =================================================
            // AGREGAR CONTROLES
            // =================================================

            panel.Controls.Add(picRobot);

            panel.Controls.Add(lblTitulo);

            panel.Controls.Add(lblSubtitulo);

            panel.Controls.Add(btnGuardar);

            panel.Controls.Add(btnCancelar);

            this.Controls.Add(panel);
        }

        // =====================================================
        // LABEL
        // =====================================================

        private Label CrearLabel(
            string texto,
            int x,
            int y
        )
        {
            return new Label
            {
                Text = texto,

                Font = new Font(
                    "Segoe UI",
                    16,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(15, 35, 90),

                AutoSize = true,

                Location = new Point(x, y)
            };
        }

        // =====================================================
        // TEXTBOX
        // =====================================================

        private TextBox CrearTextBox(
            int x,
            int y,
            int width
        )
        {
            return new TextBox
            {
                Font = new Font("Segoe UI", 14),

                Location = new Point(x, y),

                Size = new Size(width, 55),

                BorderStyle = BorderStyle.FixedSingle,

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(70, 80, 110)
            };
        }

        // =====================================================
        // CARGAR DATOS
        // =====================================================

        private void CargarDatos()
        {
            if (_usuario == null)
                return;

            txtNombre.Text = _usuario.Nombre;

            txtCorreo.Text = string.Empty;

            txtUsuario.Text =
                _usuario.Username;

            txtContrasena.Text =
                _usuario.Password;

            if (cmbGrado.Items.Count > 0)
                cmbGrado.SelectedIndex = 0;
        }

        // =====================================================
        // GUARDAR
        // =====================================================

        private void BtnGuardar_Click(
            object? sender,
            EventArgs e
        )
        {
            try
            {
                if (_esEdicion && _usuario != null)
                {
                    _usuario.Nombre =
                        txtNombre.Text.Trim();

                    _usuario.Username =
                        txtUsuario.Text.Trim();

                    _usuario.Password =
                        txtContrasena.Text;

                    if (_usuario.IdRol <= 0)
                        _usuario.IdRol = 3;

                    _bll.ActualizarUsuario(_usuario);

                    MessageBox.Show(
                        "Alumno actualizado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    var nuevo = new Usuario
                    {
                        Nombre =
                            txtNombre.Text.Trim(),

                        Username =
                            txtUsuario.Text.Trim(),

                        Password =
                            txtContrasena.Text,

                        IdRol = 3
                    };

                    if (_usuarioActual != null && _usuarioActual.IdRol == 2)
                    {
                        _bll.AgregarAlumnoParaDocente(
                            nuevo,
                            _usuarioActual.IdUsuario
                        );
                    }
                    else
                    {
                        _bll.AgregarUsuario(nuevo);
                    }

                    MessageBox.Show(
                        "Alumno agregado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
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
