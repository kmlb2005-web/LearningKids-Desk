// ============================================================
// Presentacion: FormUsuarioDetalle
// Formulario para agregar o editar un alumno
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    /// <summary>
    /// Formulario modal para agregar o editar un alumno.
    /// </summary>
    public class FormUsuarioDetalle : Form
    {
        private Label lblNombre = null!;
        private Label lblCorreo = null!;
        private Label lblUsuario = null!;
        private Label lblContrasena = null!;
        private Label lblGrado = null!;
        private TextBox txtNombre = null!;
        private TextBox txtCorreo = null!;
        private TextBox txtUsuario = null!;
        private TextBox txtContrasena = null!;
        private ComboBox cmbGrado = null!;
        private Button btnGuardar = null!;
        private Button btnCancelar = null!;

        private readonly Usuario? _usuario;
        private readonly UsuarioBLL _bll = new();
        private readonly bool _esEdicion;

        /// <summary>
        /// Constructor para agregar un nuevo alumno.
        /// </summary>
        public FormUsuarioDetalle() : this(null) { }

        /// <summary>
        /// Constructor para editar un alumno existente.
        /// </summary>
        public FormUsuarioDetalle(Usuario? usuario)
        {
            _usuario = usuario;
            _esEdicion = usuario != null;
            InicializarComponentes();
            if (_esEdicion) CargarDatos();
        }

        private void InicializarComponentes()
        {
            this.Text = _esEdicion ? "Editar Alumno" : "Agregar Alumno";
            this.Size = new Size(420, 400);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            int y = 20;
            int labelX = 20;
            int inputX = 20;
            int inputWidth = 360;

            lblNombre = CrearLabel("Nombre completo:", labelX, y);
            y += 22;
            txtNombre = CrearTextBox(inputX, y, inputWidth);

            y += 40;
            lblCorreo = CrearLabel("Correo electronico:", labelX, y);
            y += 22;
            txtCorreo = CrearTextBox(inputX, y, inputWidth);

            y += 40;
            lblUsuario = CrearLabel("Nombre de usuario:", labelX, y);
            y += 22;
            txtUsuario = CrearTextBox(inputX, y, inputWidth);
            txtUsuario.Enabled = !_esEdicion; // No editable en modo edicion

            y += 40;
            lblContrasena = CrearLabel("Contrasena:", labelX, y);
            y += 22;
            txtContrasena = CrearTextBox(inputX, y, inputWidth);
            txtContrasena.UseSystemPasswordChar = true;
            txtContrasena.Visible = !_esEdicion; // Solo al crear
            lblContrasena.Visible = !_esEdicion;

            if (_esEdicion) y -= 62; // Ajustar si no se muestra contrasena

            y += 40;
            lblGrado = CrearLabel("Grado:", labelX, y);
            y += 22;
            cmbGrado = new ComboBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(inputX, y),
                Size = new Size(inputWidth, 30),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbGrado.Items.AddRange(new[] { "1ro", "2do", "3ro", "4to", "5to", "6to" });
            cmbGrado.SelectedIndex = 5; // 6to por defecto

            y += 50;
            btnGuardar = new Button
            {
                Text = "Guardar",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(63, 81, 181),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(170, 40),
                Location = new Point(20, y),
                Cursor = Cursors.Hand
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(200, 200, 200),
                ForeColor = Color.FromArgb(60, 60, 60),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(170, 40),
                Location = new Point(210, y),
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblCorreo);
            this.Controls.Add(txtCorreo);
            this.Controls.Add(lblUsuario);
            this.Controls.Add(txtUsuario);
            this.Controls.Add(lblContrasena);
            this.Controls.Add(txtContrasena);
            this.Controls.Add(lblGrado);
            this.Controls.Add(cmbGrado);
            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnCancelar);
        }

        private Label CrearLabel(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(x, y)
            };
        }

        private TextBox CrearTextBox(int x, int y, int width)
        {
            return new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(x, y),
                Size = new Size(width, 28),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        private void CargarDatos()
        {
            if (_usuario == null) return;
            txtNombre.Text = _usuario.Nombre;
            txtCorreo.Text = _usuario.Correo;
            txtUsuario.Text = _usuario.NombreUsuario;
            cmbGrado.SelectedItem = _usuario.Grado;
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_esEdicion && _usuario != null)
                {
                    _usuario.Nombre = txtNombre.Text.Trim();
                    _usuario.Correo = txtCorreo.Text.Trim();
                    _usuario.Grado = cmbGrado.SelectedItem?.ToString() ?? "6to";
                    _bll.ActualizarAlumno(_usuario);
                    MessageBox.Show("Alumno actualizado correctamente.", "Exito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var nuevo = new Usuario
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Correo = txtCorreo.Text.Trim(),
                        NombreUsuario = txtUsuario.Text.Trim(),
                        Contrasena = txtContrasena.Text,
                        Grado = cmbGrado.SelectedItem?.ToString() ?? "6to"
                    };
                    _bll.AgregarAlumno(nuevo);
                    MessageBox.Show("Alumno agregado correctamente.", "Exito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
