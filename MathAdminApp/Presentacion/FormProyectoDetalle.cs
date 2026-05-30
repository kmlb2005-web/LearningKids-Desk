using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class FormProyectoDetalle : Form
    {
        private TextBox txtNombre = null!;
        private TextBox txtDescripcion = null!;
        private NumericUpDown numGrado = null!;
        private ComboBox cmbCampo = null!;
        private Button btnGuardar = null!;
        private Button btnCancelar = null!;

        private readonly ProyectoBLL _proyectoBLL = new();
        private readonly CampoFormativoBLL _campoBLL = new();
        private readonly BitacoraSistemaBLL _bitacoraBLL = new();
        private readonly Proyecto? _proyecto;
        private readonly Usuario? _usuarioActual;
        private readonly bool _esEdicion;
        private List<CampoFormativo> _campos = new();

        public FormProyectoDetalle() : this(null, null)
        {
        }

        public FormProyectoDetalle(Proyecto? proyecto) : this(proyecto, null)
        {
        }

        public FormProyectoDetalle(Proyecto? proyecto, Usuario? usuarioActual)
        {
            _proyecto = proyecto;
            _usuarioActual = usuarioActual;
            _esEdicion = proyecto != null;

            InicializarComponentes();
            CargarCampos();

            if (_esEdicion)
                CargarDatos();
        }

        private void InicializarComponentes()
        {
            Text = _esEdicion ? "Editar Proyecto" : "Nuevo Proyecto";
            Size = new Size(850, 650);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;

            Label lblTitulo = new()
            {
                Text = Text,
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 90),
                AutoSize = true,
                Location = new Point(50, 35)
            };

            txtNombre = CrearTextBox(50, 145, 720);
            txtNombre.PlaceholderText = "Nombre del proyecto...";

            txtDescripcion = CrearTextBox(50, 235, 720);
            txtDescripcion.PlaceholderText = "Descripcion del proyecto...";
            txtDescripcion.Multiline = true;
            txtDescripcion.Height = 95;

            numGrado = new NumericUpDown
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(50, 385),
                Size = new Size(220, 40),
                Minimum = 1,
                Maximum = 6
            };

            cmbCampo = new ComboBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(315, 385),
                Size = new Size(455, 40),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White
            };

            btnGuardar = CrearBoton(_esEdicion ? "Actualizar" : "Guardar", Color.FromArgb(50, 120, 255), 145, 500);
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = CrearBoton("Cancelar", Color.FromArgb(255, 80, 120), 445, 500);
            btnCancelar.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            Controls.Add(lblTitulo);
            Controls.Add(CrearLabel("Nombre", 50, 115));
            Controls.Add(txtNombre);
            Controls.Add(CrearLabel("Descripcion", 50, 205));
            Controls.Add(txtDescripcion);
            Controls.Add(CrearLabel("Grado", 50, 355));
            Controls.Add(numGrado);
            Controls.Add(CrearLabel("Campo formativo", 315, 355));
            Controls.Add(cmbCampo);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
        }

        private static Label CrearLabel(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 90),
                AutoSize = true,
                Location = new Point(x, y)
            };
        }

        private static TextBox CrearTextBox(int x, int y, int width)
        {
            return new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(x, y),
                Size = new Size(width, 42),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(70, 80, 110)
            };
        }

        private static Button CrearBoton(string texto, Color color, int x, int y)
        {
            Button btn = new()
            {
                Text = texto,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(250, 58),
                Location = new Point(x, y),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void CargarCampos()
        {
            _campos = _campoBLL.ObtenerTodos();
            cmbCampo.Items.Clear();

            foreach (var campo in _campos)
                cmbCampo.Items.Add(campo.Nombre);

            if (cmbCampo.Items.Count > 0)
                cmbCampo.SelectedIndex = 0;
        }

        private void CargarDatos()
        {
            if (_proyecto == null)
                return;

            txtNombre.Text = _proyecto.nombre;
            txtDescripcion.Text = _proyecto.descripcion;
            numGrado.Value = Math.Min(numGrado.Maximum, Math.Max(numGrado.Minimum, _proyecto.grado));

            int campoIndex = _campos.FindIndex(c => c.IdCampo == _proyecto.idCampo);
            if (campoIndex >= 0)
                cmbCampo.SelectedIndex = campoIndex;
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show("El nombre del proyecto es obligatorio.", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmbCampo.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione un campo formativo.", "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            try
            {
                Proyecto proyecto = _proyecto ?? new Proyecto();
                proyecto.nombre = txtNombre.Text.Trim();
                proyecto.descripcion = txtDescripcion.Text.Trim();
                proyecto.grado = (int)numGrado.Value;
                proyecto.idCampo = _campos[cmbCampo.SelectedIndex].IdCampo;
                if (!_esEdicion && _usuarioActual != null)
                    proyecto.creadoPor = _usuarioActual.IdUsuario;
                else if (proyecto.creadoPor <= 0)
                    proyecto.creadoPor = _usuarioActual?.IdUsuario ?? 1;

                if (_esEdicion)
                {
                    _proyectoBLL.Actualizar(proyecto);
                    _bitacoraBLL.Registrar(
                        _usuarioActual,
                        "Proyectos",
                        "Actualizacion",
                        $"Actualizo el proyecto '{proyecto.nombre}' (ID {proyecto.idProyecto})."
                    );
                }
                else
                {
                    _proyectoBLL.Crear(proyecto);
                    _bitacoraBLL.Registrar(
                        _usuarioActual,
                        "Proyectos",
                        "Alta",
                        $"Agrego el proyecto '{proyecto.nombre}' para grado {proyecto.grado}."
                    );
                }

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
