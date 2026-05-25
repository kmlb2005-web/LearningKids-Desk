using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class FormProyectoDetalle : Form
    {
        // =====================================================
        // CONTROLES
        // =====================================================

        private TextBox txtNombre = null!;
        private TextBox txtDescripcion = null!;
        private ComboBox cmbUnidad = null!;

        private Button btnGuardar = null!;
        private Button btnCancelar = null!;

        // =====================================================
        // VARIABLES
        // =====================================================

        private readonly ProyectoBLL _proyectoBLL =
            new();

        private readonly Proyecto? _proyecto;

        private readonly bool _esEdicion;

        // =====================================================
        // CONSTRUCTORES
        // =====================================================

        public FormProyectoDetalle()
            : this(null)
        {
        }

        public FormProyectoDetalle(
            Proyecto? proyecto
        )
        {
            _proyecto = proyecto;

            _esEdicion = proyecto != null;

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
                ? "Editar Proyecto"
                : "Nuevo Proyecto";

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

                SizeMode =
                    PictureBoxSizeMode.Zoom,

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
                    ? "✏️ Editar Proyecto"
                    : "📋 Nuevo Proyecto",

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
                    "Completa la información del proyecto ✨",

                Font = new Font(
                    "Segoe UI",
                    16
                ),

                ForeColor =
                    Color.FromArgb(
                        120,
                        130,
                        160
                    ),

                AutoSize = true,

                Location = new Point(
                    220,
                    115
                )
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
                    "📁 Nombre del proyecto",
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
                "Escribe el nombre del proyecto...";

            panel.Controls.Add(txtNombre);

            // =================================================
            // DESCRIPCIÓN
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
                Font = new Font(
                    "Segoe UI",
                    14
                ),

                Location = new Point(x, y),

                Size = new Size(width, 130),

                BorderStyle =
                    BorderStyle.FixedSingle,

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(
                        70,
                        80,
                        110
                    ),

                Multiline = true
            };

            txtDescripcion.PlaceholderText =
                "Describe el proyecto...";

            panel.Controls.Add(
                txtDescripcion
            );

            // =================================================
            // UNIDAD
            // =================================================

            y += 180;

            panel.Controls.Add(
                CrearLabel(
                    "📚 Unidad",
                    x,
                    y
                )
            );

            y += 45;

            cmbUnidad = new ComboBox
            {
                Font = new Font(
                    "Segoe UI",
                    14
                ),

                Location = new Point(
                    x,
                    y
                ),

                Size = new Size(
                    width,
                    55
                ),

                DropDownStyle =
                    ComboBoxStyle
                    .DropDownList,

                FlatStyle =
                    FlatStyle.Flat,

                BackColor =
                    Color.White,

                ForeColor =
                    Color.FromArgb(
                        30,
                        50,
                        90
                    )
            };

            cmbUnidad.Items.Add(
                new UnidadItem(
                    1,
                    "Unidad 1"
                )
            );

            cmbUnidad.Items.Add(
                new UnidadItem(
                    2,
                    "Unidad 2"
                )
            );

            cmbUnidad.Items.Add(
                new UnidadItem(
                    3,
                    "Unidad 3"
                )
            );

            cmbUnidad.Items.Add(
                new UnidadItem(
                    4,
                    "Unidad 4"
                )
            );

            cmbUnidad.Items.Add(
                new UnidadItem(
                    5,
                    "Unidad 5"
                )
            );

            cmbUnidad.SelectedIndex = 0;

            panel.Controls.Add(cmbUnidad);

            // =================================================
            // LINEA
            // =================================================

            Panel linea = new Panel
            {
                BackColor =
                    Color.FromArgb(
                        230,
                        235,
                        245
                    ),

                Size = new Size(
                    760,
                    2
                ),

                Location = new Point(
                    70,
                    y + 90
                )
            };

            panel.Controls.Add(linea);

            // =================================================
            // BOTON GUARDAR
            // =================================================

            btnGuardar = new Button
            {
                Text = _esEdicion
                    ? "💾 Actualizar"
                    : "💾 Guardar",

                Font = new Font(
                    "Segoe UI",
                    14,
                    FontStyle.Bold
                ),

                BackColor =
                    Color.FromArgb(
                        50,
                        120,
                        255
                    ),

                ForeColor =
                    Color.White,

                FlatStyle =
                    FlatStyle.Flat,

                Size = new Size(
                    260,
                    65
                ),

                Location = new Point(
                    230,
                    y + 125
                ),

                Cursor = Cursors.Hand
            };

            btnGuardar.FlatAppearance
                .BorderSize = 0;

            btnGuardar.Click +=
                BtnGuardar_Click;

            btnGuardar.MouseEnter +=
                (s, e) =>
                {
                    btnGuardar.BackColor =
                        Color.FromArgb(
                            70,
                            140,
                            255
                        );
                };

            btnGuardar.MouseLeave +=
                (s, e) =>
                {
                    btnGuardar.BackColor =
                        Color.FromArgb(
                            50,
                            120,
                            255
                        );
                };

            // =================================================
            // BOTON CANCELAR
            // =================================================

            btnCancelar = new Button
            {
                Text = "❌ Cancelar",

                Font = new Font(
                    "Segoe UI",
                    14,
                    FontStyle.Bold
                ),

                BackColor =
                    Color.White,

                ForeColor =
                    Color.FromArgb(
                        255,
                        70,
                        120
                    ),

                FlatStyle =
                    FlatStyle.Flat,

                Size = new Size(
                    260,
                    65
                ),

                Location = new Point(
                    520,
                    y + 125
                ),

                Cursor = Cursors.Hand
            };

            btnCancelar.FlatAppearance
                .BorderColor =
                Color.FromArgb(
                    255,
                    70,
                    120
                );

            btnCancelar.FlatAppearance
                .BorderSize = 2;

            btnCancelar.Click +=
                (s, e) =>
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
                    Color.FromArgb(
                        15,
                        35,
                        90
                    ),

                AutoSize = true,

                Location = new Point(
                    x,
                    y
                )
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
                Font = new Font(
                    "Segoe UI",
                    14
                ),

                Location = new Point(
                    x,
                    y
                ),

                Size = new Size(
                    width,
                    55
                ),

                BorderStyle =
                    BorderStyle.FixedSingle,

                BackColor =
                    Color.White,

                ForeColor =
                    Color.FromArgb(
                        70,
                        80,
                        110
                    )
            };
        }

        // =====================================================
        // CARGAR DATOS
        // =====================================================

        private void CargarDatos()
        {
            if (_proyecto == null)
                return;

            txtNombre.Text =
                _proyecto.nombre;

            txtDescripcion.Text =
                _proyecto.descripcion;

            cmbUnidad.SelectedIndex =
                _proyecto.unidad - 1;
        }

        // =====================================================
        // GUARDAR
        // =====================================================

        private void BtnGuardar_Click(
            object? sender,
            EventArgs e
        )
        {
            if (
                string.IsNullOrWhiteSpace(
                    txtNombre.Text
                )
            )
            {
                MessageBox.Show(
                    "El nombre del proyecto es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
                if (
                    _esEdicion
                    && _proyecto != null
                )
                {
                    _proyecto.nombre =
                        txtNombre.Text.Trim();

                    _proyecto.descripcion =
                        txtDescripcion.Text.Trim();

                    _proyecto.unidad =
                        (
                            (UnidadItem)
                            cmbUnidad
                            .SelectedItem!
                        ).Numero;

                    _proyectoBLL.Actualizar(
                        _proyecto
                    );

                    MessageBox.Show(
                        "Proyecto actualizado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    Proyecto proyecto =
                        new()
                        {
                            nombre =
                                txtNombre.Text
                                .Trim(),

                            descripcion =
                                txtDescripcion
                                .Text
                                .Trim(),

                            unidad =
                                (
                                    (UnidadItem)
                                    cmbUnidad
                                    .SelectedItem!
                                ).Numero
                        };

                    _proyectoBLL.Insertar(
                        proyecto
                    );

                    MessageBox.Show(
                        "Proyecto agregado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                this.DialogResult =
                    DialogResult.OK;

                this.Close();
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

    // =========================================================
    // CLASE AUXILIAR
    // =========================================================

    public class UnidadItem
    {
        public int Numero { get; }

        public string Etiqueta { get; }

        public UnidadItem(
            int numero,
            string etiqueta
        )
        {
            Numero = numero;

            Etiqueta = etiqueta;
        }

        public override string ToString()
        {
            return Etiqueta;
        }
    }
}