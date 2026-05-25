using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class FormResultadoDetalle : Form
    {
        private ComboBox cmbAlumno = null!;
        private ComboBox cmbPrueba = null!;
        private NumericUpDown numCalificacion = null!;
        private DateTimePicker dtpFecha = null!;
        private Button btnGuardar = null!;
        private Button btnCancelar = null!;

        private readonly ResultadoBLL _resultadoBll = new();
        private readonly UsuarioBLL _usuarioBll = new();
        private readonly PruebaBLL _pruebaBll = new();
        private readonly Resultado? _resultado;
        private readonly Usuario? _usuarioActual;
        private readonly bool _esEdicion;

        private List<Usuario> _alumnos = new();
        private List<Prueba> _pruebas = new();

        public FormResultadoDetalle(Resultado? resultado = null, Usuario? usuarioActual = null)
        {
            _resultado = resultado;
            _usuarioActual = usuarioActual;
            _esEdicion = resultado != null;

            InicializarComponentes();
            CargarCombos();

            if (_esEdicion)
                CargarDatos();
        }

        private void InicializarComponentes()
        {
            Text = _esEdicion ? "Editar Resultado" : "Nuevo Resultado";
            Size = new Size(720, 520);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.White;

            Label lblTitulo = new()
            {
                Text = Text,
                Font = new Font("Segoe UI", 24, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 90),
                AutoSize = true,
                Location = new Point(40, 30)
            };

            cmbAlumno = CrearCombo(40, 125);
            cmbPrueba = CrearCombo(40, 210);

            numCalificacion = new NumericUpDown
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(40, 295),
                Size = new Size(260, 40),
                DecimalPlaces = 2,
                Minimum = 0,
                Maximum = 100
            };

            dtpFecha = new DateTimePicker
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(360, 295),
                Size = new Size(280, 40),
                Format = DateTimePickerFormat.Short
            };

            btnGuardar = CrearBoton("Guardar", Color.FromArgb(50, 120, 255), 90, 385);
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = CrearBoton("Cancelar", Color.FromArgb(255, 80, 120), 370, 385);
            btnCancelar.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            Controls.Add(lblTitulo);
            Controls.Add(CrearLabel("Alumno", 40, 95));
            Controls.Add(cmbAlumno);
            Controls.Add(CrearLabel("Prueba", 40, 180));
            Controls.Add(cmbPrueba);
            Controls.Add(CrearLabel("Calificacion", 40, 265));
            Controls.Add(numCalificacion);
            Controls.Add(CrearLabel("Fecha", 360, 265));
            Controls.Add(dtpFecha);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
        }

        private static Label CrearLabel(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 90),
                AutoSize = true,
                Location = new Point(x, y)
            };
        }

        private static ComboBox CrearCombo(int x, int y)
        {
            return new ComboBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(x, y),
                Size = new Size(600, 40),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White
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
                Size = new Size(220, 55),
                Location = new Point(x, y),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void CargarCombos()
        {
            _alumnos = _usuarioActual != null && _usuarioActual.IdRol == 2
                ? _usuarioBll.ObtenerAlumnosPorDocente(_usuarioActual.IdUsuario)
                : _usuarioBll.ObtenerAlumnos();

            _pruebas = _usuarioActual != null
                ? _pruebaBll.ObtenerVisibles(_usuarioActual)
                : _pruebaBll.ObtenerPorTema(null);

            cmbAlumno.Items.Clear();
            foreach (var alumno in _alumnos)
                cmbAlumno.Items.Add(alumno.Nombre);

            cmbPrueba.Items.Clear();
            foreach (var prueba in _pruebas)
                cmbPrueba.Items.Add(prueba.Titulo);

            if (cmbAlumno.Items.Count > 0)
                cmbAlumno.SelectedIndex = 0;
            if (cmbPrueba.Items.Count > 0)
                cmbPrueba.SelectedIndex = 0;
        }

        private void CargarDatos()
        {
            if (_resultado == null)
                return;

            int alumnoIndex = _alumnos.FindIndex(a => a.IdUsuario == _resultado.IdAlumno);
            if (alumnoIndex >= 0)
                cmbAlumno.SelectedIndex = alumnoIndex;

            int pruebaIndex = _pruebas.FindIndex(p => p.IdPrueba == _resultado.IdPrueba);
            if (pruebaIndex >= 0)
                cmbPrueba.SelectedIndex = pruebaIndex;

            numCalificacion.Value = Math.Min(numCalificacion.Maximum, Math.Max(numCalificacion.Minimum, _resultado.Calificacion));
            dtpFecha.Value = _resultado.Fecha == DateTime.MinValue ? DateTime.Today : _resultado.Fecha;
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            if (cmbAlumno.SelectedIndex < 0 || cmbPrueba.SelectedIndex < 0)
            {
                MessageBox.Show("Seleccione alumno y prueba.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                Resultado resultado = _resultado ?? new Resultado();
                resultado.IdAlumno = _alumnos[cmbAlumno.SelectedIndex].IdUsuario;
                resultado.IdPrueba = _pruebas[cmbPrueba.SelectedIndex].IdPrueba;
                resultado.Calificacion = numCalificacion.Value;
                resultado.Fecha = dtpFecha.Value.Date;

                if (_esEdicion)
                    _resultadoBll.ActualizarResultado(resultado);
                else
                    _resultadoBll.AgregarResultado(resultado);

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
