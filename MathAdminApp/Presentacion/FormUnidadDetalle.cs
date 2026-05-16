// ============================================================
// Presentacion: FormUnidadDetalle (MODERNO)
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class FormUnidadDetalle : Form
    {
        // =====================================================
        // VARIABLES
        // =====================================================

        private readonly Unidad? _unidad;

        private readonly UnidadBLL _bll = new();

        private readonly bool _esEdicion;

        // =====================================================
        // CONTROLES
        // =====================================================

        private TextBox txtNombre = null!;

        private TextBox txtDescripcion = null!;

        private NumericUpDown nudNumero = null!;

        private Button btnGuardar = null!;

        private Button btnCancelar = null!;

        // =====================================================
        // CONSTRUCTORES
        // =====================================================

        public FormUnidadDetalle() : this(null) { }

        public FormUnidadDetalle(Unidad? unidad)
        {
            _unidad = unidad;

            _esEdicion = unidad != null;

            InicializarComponentes();

            if (_esEdicion)
                CargarDatos();
        }

        // =====================================================
        // DISEÑO MODERNO
        // =====================================================

        private void InicializarComponentes()
        {
            // =================================================
            // FORMULARIO
            // =================================================

            this.Text = _esEdicion
                ? "Editar Unidad"
                : "Nueva Unidad";

            this.Size = new Size(950, 760);

            this.StartPosition =
                FormStartPosition.CenterParent;

            this.FormBorderStyle =
                FormBorderStyle.FixedDialog;

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
                Image = Image.FromFile(
                    "Resources/Louz.png"
                ),

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
                    ? "✏️ Editar Unidad"
                    : "📘 Nueva Unidad",

                Font = new Font(
                    "Segoe UI",
                    34,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(20, 35, 90),

                AutoSize = true,

                Location = new Point(210, 50)
            };

            // =================================================
            // SUBTITULO
            // =================================================

            Label lblSubtitulo = new Label
            {
                Text =
                    "Configura la información de la unidad ✨",

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
            // NUMERO DE UNIDAD
            // =================================================

            panel.Controls.Add(
                CrearLabel(
                    "🔢 Número de unidad",
                    x,
                    y
                )
            );

            y += 45;

            nudNumero = new NumericUpDown
            {
                Font = new Font("Segoe UI", 14),

                Location = new Point(x, y),

                Size = new Size(200, 55),

                Minimum = 1,

                Maximum = 50,

                Value = 1,

                BorderStyle = BorderStyle.FixedSingle,

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(60, 70, 110)
            };

            panel.Controls.Add(nudNumero);

            // =================================================
            // NOMBRE
            // =================================================

            y += 115;

            panel.Controls.Add(
                CrearLabel(
                    "📚 Nombre de la unidad",
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
                "Ejemplo: Números naturales";

            panel.Controls.Add(txtNombre);

            // =================================================
            // DESCRIPCION
            // =================================================

            y += 115;

            panel.Controls.Add(
                CrearLabel(
                    "📝 Descripción",
                    x,
                    y
                )
            );

            y += 45;

            txtDescripcion = new TextBox
            {
                Font = new Font("Segoe UI", 14),

                Location = new Point(x, y),

                Size = new Size(width, 140),

                BorderStyle = BorderStyle.FixedSingle,

                Multiline = true,

                ScrollBars = ScrollBars.Vertical,

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(60, 70, 110)
            };

            panel.Controls.Add(txtDescripcion);

            // =================================================
            // LINEA DIVISORA
            // =================================================

            Panel linea = new Panel
            {
                BackColor =
                    Color.FromArgb(230, 235, 245),

                Size = new Size(760, 2),

                Location = new Point(70, y + 180)
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

                Location =
                    new Point(230, y + 220),

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

                Location =
                    new Point(520, y + 220),

                Cursor = Cursors.Hand
            };

            btnCancelar.FlatAppearance.BorderColor =
                Color.FromArgb(255, 70, 120);

            btnCancelar.FlatAppearance.BorderSize = 2;

            btnCancelar.Click += (s, e) =>
            {
                this.DialogResult =
                    DialogResult.Cancel;

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
            if (_unidad == null)
                return;

            txtNombre.Text =
                _unidad.Nombre;

            txtDescripcion.Text =
                _unidad.Descripcion;

            nudNumero.Value =
                _unidad.NumeroUnidad;
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
                if (_esEdicion && _unidad != null)
                {
                    _unidad.Nombre =
                        txtNombre.Text.Trim();

                    _unidad.Descripcion =
                        txtDescripcion.Text.Trim();

                    _unidad.NumeroUnidad =
                        (int)nudNumero.Value;

                    _bll.Actualizar(_unidad);

                    MessageBox.Show(
                        "Unidad actualizada.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    var nueva = new Unidad
                    {
                        Nombre =
                            txtNombre.Text.Trim(),

                        Descripcion =
                            txtDescripcion.Text.Trim(),

                        NumeroUnidad =
                            (int)nudNumero.Value
                    };

                    _bll.Agregar(nueva);

                    MessageBox.Show(
                        "Unidad agregada.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                this.DialogResult =
                    DialogResult.OK;

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
