// ============================================================
// Presentacion: FormUnidadDetalle
// Formulario para agregar o editar una unidad
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    /// <summary>
    /// Formulario modal para agregar o editar una unidad tematica.
    /// </summary>
    public class FormUnidadDetalle : Form
    {
        private Label lblNombre = null!;
        private Label lblDescripcion = null!;
        private Label lblNumero = null!;
        private TextBox txtNombre = null!;
        private TextBox txtDescripcion = null!;
        private NumericUpDown nudNumero = null!;
        private Button btnGuardar = null!;
        private Button btnCancelar = null!;

        private readonly Unidad? _unidad;
        private readonly UnidadBLL _bll = new();
        private readonly bool _esEdicion;

        public FormUnidadDetalle() : this(null) { }

        public FormUnidadDetalle(Unidad? unidad)
        {
            _unidad = unidad;
            _esEdicion = unidad != null;
            InicializarComponentes();
            if (_esEdicion) CargarDatos();
        }

        private void InicializarComponentes()
        {
            this.Text = _esEdicion ? "Editar Unidad" : "Agregar Unidad";
            this.Size = new Size(420, 380);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            int y = 20;

            lblNumero = CrearLabel("Numero de unidad:", 20, y);
            y += 22;
            nudNumero = new NumericUpDown
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(20, y),
                Size = new Size(100, 28),
                Minimum = 1,
                Maximum = 50,
                Value = 1
            };

            y += 45;
            lblNombre = CrearLabel("Nombre de la unidad:", 20, y);
            y += 22;
            txtNombre = CrearTextBox(20, y, 360);

            y += 40;
            lblDescripcion = CrearLabel("Descripcion:", 20, y);
            y += 22;
            txtDescripcion = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(20, y),
                Size = new Size(360, 80),
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            y += 95;
            btnGuardar = new Button
            {
                Text = "Guardar",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(0, 150, 136),
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

            this.Controls.Add(lblNumero);
            this.Controls.Add(nudNumero);
            this.Controls.Add(lblNombre);
            this.Controls.Add(txtNombre);
            this.Controls.Add(lblDescripcion);
            this.Controls.Add(txtDescripcion);
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
            if (_unidad == null) return;
            txtNombre.Text = _unidad.Nombre;
            txtDescripcion.Text = _unidad.Descripcion;
            nudNumero.Value = _unidad.NumeroUnidad;
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                if (_esEdicion && _unidad != null)
                {
                    _unidad.Nombre = txtNombre.Text.Trim();
                    _unidad.Descripcion = txtDescripcion.Text.Trim();
                    _unidad.NumeroUnidad = (int)nudNumero.Value;
                    _bll.Actualizar(_unidad);
                    MessageBox.Show("Unidad actualizada.", "Exito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    var nueva = new Unidad
                    {
                        Nombre = txtNombre.Text.Trim(),
                        Descripcion = txtDescripcion.Text.Trim(),
                        NumeroUnidad = (int)nudNumero.Value
                    };
                    _bll.Agregar(nueva);
                    MessageBox.Show("Unidad agregada.", "Exito",
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
