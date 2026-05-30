// ============================================================
// FORMULARIO MODERNO - NUEVO / EDITAR USUARIO
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
        private readonly AlumnoBLL _alumnoBLL = new();
        private readonly BitacoraSistemaBLL _bitacoraBLL = new();

        private readonly Usuario? _usuario;
        private readonly Usuario? _usuarioActual;

        private readonly bool _esEdicion;

        // =====================================================
        // CONTROLES
        // =====================================================

        private TextBox txtNombre = null!;
        private TextBox txtUsuario = null!;
        private TextBox txtContrasena = null!;

        private ComboBox cmbRol = null!;
        private ComboBox cmbGrado = null!;
        private Label lblGrado = null!;

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
                ? "Editar Usuario"
                : "Nuevo Usuario";

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
                    ? "✏️ Editar Usuario"
                    : "➕ Nuevo Usuario",

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
                    "Completa la información según el rol ✨",

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
                "Escribe el nombre completo...";

            panel.Controls.Add(txtNombre);

            // =================================================
            // ROL
            // =================================================

            y += 115;

            panel.Controls.Add(
                CrearLabel(
                    "👔 Rol",
                    x,
                    y
                )
            );

            y += 45;

            cmbRol = new ComboBox
            {
                Font = new Font("Segoe UI", 14),
                Location = new Point(x, y),
                Size = new Size(width, 55),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(30, 50, 90)
            };

            ConfigurarRoles();
            cmbRol.SelectedIndexChanged += (s, e) => ActualizarCamposPorRol();

            panel.Controls.Add(cmbRol);

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

            lblGrado = CrearLabel(
                "🎓 Grado",
                x,
                y
            );

            panel.Controls.Add(lblGrado);

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

            ActualizarCamposPorRol();
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

            txtUsuario.Text =
                _usuario.Username;

            txtContrasena.Text =
                _usuario.Password;

            if (cmbGrado.Items.Count > 0)
            {
                Alumno? alumno = _usuario.IdRol == 3
                    ? _alumnoBLL.ObtenerPorId(_usuario.IdUsuario)
                    : null;

                int grado = alumno?.Grado ?? 1;
                cmbGrado.SelectedIndex = Math.Max(0, Math.Min(5, grado - 1));
            }

            SeleccionarRol(_usuario.IdRol);
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
                    int idRol = ObtenerRolSeleccionado();

                    _usuario.Nombre =
                        txtNombre.Text.Trim();

                    _usuario.Username =
                        txtUsuario.Text.Trim();

                    _usuario.Password =
                        txtContrasena.Text;

                    _usuario.IdRol = idRol;

                    _bll.ActualizarUsuario(_usuario);
                    GuardarDatosAlumnoSiAplica(_usuario.IdUsuario, idRol);

                    _bitacoraBLL.Registrar(
                        _usuarioActual,
                        "Usuarios",
                        "Actualizacion",
                        $"Actualizo el usuario '{_usuario.Nombre}' como {ObtenerNombreRol(idRol)} (ID {_usuario.IdUsuario})."
                    );

                    MessageBox.Show(
                        "Usuario actualizado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    int idRol = ObtenerRolSeleccionado();

                    var nuevo = new Usuario
                    {
                        Nombre =
                            txtNombre.Text.Trim(),

                        Username =
                            txtUsuario.Text.Trim(),

                        Password =
                            txtContrasena.Text,

                        IdRol = idRol
                    };

                    if (_usuarioActual != null && _usuarioActual.IdRol == 2)
                    {
                        nuevo.IdRol = 3;

                        _bll.AgregarAlumnoParaDocente(
                            nuevo,
                            _usuarioActual.IdUsuario
                        );
                    }
                    else
                    {
                        _bll.AgregarUsuario(nuevo);
                    }

                    GuardarDatosAlumnoSiAplica(nuevo.IdUsuario, nuevo.IdRol);

                    _bitacoraBLL.Registrar(
                        _usuarioActual,
                        "Usuarios",
                        "Alta",
                        $"Agrego el usuario '{nuevo.Nombre}' como {ObtenerNombreRol(nuevo.IdRol)} con usuario '{nuevo.Username}'."
                    );

                    MessageBox.Show(
                        "Usuario agregado correctamente.",
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

        private void ConfigurarRoles()
        {
            cmbRol.Items.Clear();

            if (_usuarioActual != null && _usuarioActual.IdRol == 2)
            {
                cmbRol.Items.Add(new RolItem(3, "Alumno"));
            }
            else
            {
                cmbRol.Items.Add(new RolItem(1, "Administrador"));
                cmbRol.Items.Add(new RolItem(2, "Docente"));
                cmbRol.Items.Add(new RolItem(3, "Alumno"));
            }

            cmbRol.SelectedIndex = cmbRol.Items.Count > 0
                ? cmbRol.Items.Count - 1
                : -1;
        }

        private void SeleccionarRol(int idRol)
        {
            for (int i = 0; i < cmbRol.Items.Count; i++)
            {
                if (cmbRol.Items[i] is RolItem item && item.Id == idRol)
                {
                    cmbRol.SelectedIndex = i;
                    return;
                }
            }
        }

        private int ObtenerRolSeleccionado()
        {
            if (cmbRol.SelectedItem is not RolItem item)
                throw new ArgumentException("Debe seleccionar un rol.");

            return item.Id;
        }

        private string ObtenerNombreRol(int idRol)
        {
            return idRol switch
            {
                1 => "Administrador",
                2 => "Docente",
                3 => "Alumno",
                _ => "Usuario"
            };
        }

        private int ObtenerGradoSeleccionado()
        {
            return cmbGrado.SelectedIndex < 0
                ? 1
                : cmbGrado.SelectedIndex + 1;
        }

        private void ActualizarCamposPorRol()
        {
            bool esAlumno = ObtenerRolSeleccionadoSeguro() == 3;

            lblGrado.Visible = esAlumno;
            cmbGrado.Visible = esAlumno;
        }

        private int ObtenerRolSeleccionadoSeguro()
        {
            return cmbRol.SelectedItem is RolItem item
                ? item.Id
                : 3;
        }

        private void GuardarDatosAlumnoSiAplica(int idUsuario, int idRol)
        {
            if (idUsuario <= 0)
                return;

            Alumno? alumno = _alumnoBLL.ObtenerPorId(idUsuario);

            if (idRol == 3)
            {
                Alumno datosAlumno = new()
                {
                    IdAlumno = idUsuario,
                    IdTutor = alumno?.IdTutor,
                    Grado = ObtenerGradoSeleccionado()
                };

                if (alumno == null)
                    _alumnoBLL.AgregarAlumno(datosAlumno);
                else
                    _alumnoBLL.ActualizarAlumno(datosAlumno);

                return;
            }

            if (alumno != null)
                _alumnoBLL.EliminarAlumno(idUsuario);
        }

        private sealed class RolItem
        {
            public int Id { get; }

            public string Nombre { get; }

            public RolItem(int id, string nombre)
            {
                Id = id;
                Nombre = nombre;
            }

            public override string ToString() => Nombre;
        }
    }
}
