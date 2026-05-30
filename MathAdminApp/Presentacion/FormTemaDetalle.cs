// ============================================================
// FORMULARIO MODERNO — NUEVO / EDITAR TEMA
// (misma temática que FormUsuarioDetalle)
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class FormTemaDetalle : Form
    {
        // =====================================================
        // VARIABLES
        // =====================================================

        private readonly TemaBLL _temaBLL = new();
        private readonly ProyectoBLL _proyectoBLL = new();
        private readonly BitacoraSistemaBLL _bitacoraBLL = new();

        private readonly Tema? _tema;
        private readonly Usuario? _usuarioActual;

        private readonly bool _esEdicion;

        // =====================================================
        // CONTROLES
        // =====================================================

        private TextBox txtNombre = null!;
        private TextBox txtDescripcion = null!;

        private ComboBox cmbProyecto = null!;

        private Button btnGuardar = null!;
        private Button btnCancelar = null!;

        // =====================================================
        // CONSTRUCTORES
        // =====================================================

        public FormTemaDetalle() : this(null, null) { }

        public FormTemaDetalle(Tema? tema) : this(tema, null) { }

        public FormTemaDetalle(Tema? tema, Usuario? usuarioActual)
        {
            _tema = tema;
            _usuarioActual = usuarioActual;

            _esEdicion = tema != null;

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
                ? "Editar Tema"
                : "Nuevo Tema";

            this.Size = new Size(950, 780);

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
                    ? "✏️ Editar Tema"
                    : "➕ Nuevo Tema",

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
                Text = "Completa la información del tema ✨",

                Font = new Font("Segoe UI", 16),

                ForeColor = Color.FromArgb(120, 130, 160),

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
                CrearLabel("📌 Nombre del tema", x, y)
            );

            y += 45;

            txtNombre = CrearTextBox(x, y, width);

            txtNombre.PlaceholderText =
                "Escribe el nombre del tema...";

            panel.Controls.Add(txtNombre);

            // =================================================
            // DESCRIPCION
            // =================================================

            y += 115;

            panel.Controls.Add(
                CrearLabel("📝 Descripción", x, y)
            );

            y += 45;

            txtDescripcion = new TextBox
            {
                Font = new Font("Segoe UI", 14),

                Location = new Point(x, y),

                Size = new Size(width, 110),

                Multiline = true,

                BorderStyle = BorderStyle.FixedSingle,

                BackColor = Color.White,

                ForeColor = Color.FromArgb(70, 80, 110),

                PlaceholderText =
                    "Describe brevemente el tema..."
            };

            panel.Controls.Add(txtDescripcion);

            // =================================================
            // PROYECTO
            // =================================================

            y += 175;

            panel.Controls.Add(
                CrearLabel("📋 Proyecto", x, y)
            );

            y += 45;

            cmbProyecto = new ComboBox
            {
                Font = new Font("Segoe UI", 14),

                Location = new Point(x, y),

                Size = new Size(width, 55),

                DropDownStyle = ComboBoxStyle.DropDownList,

                FlatStyle = FlatStyle.Flat,

                BackColor = Color.White,

                ForeColor = Color.FromArgb(30, 50, 90)
            };

            try
            {
                var proyectos = _usuarioActual == null
                    ? _proyectoBLL.ObtenerTodos()
                    : _proyectoBLL.ObtenerVisibles(_usuarioActual);

                foreach (var p in proyectos)
                    cmbProyecto.Items.Add(
                        new ProyectoItem(p.idProyecto, p.nombre)
                    );

                if (cmbProyecto.Items.Count > 0)
                    cmbProyecto.SelectedIndex = 0;
            }
            catch
            {
                cmbProyecto.Items.Add(
                    new ProyectoItem(0, "Sin proyectos disponibles")
                );

                cmbProyecto.SelectedIndex = 0;
            }

            panel.Controls.Add(cmbProyecto);

            // =================================================
            // LINEA
            // =================================================

            Panel linea = new Panel
            {
                BackColor = Color.FromArgb(230, 235, 245),

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

                BackColor = Color.FromArgb(50, 120, 255),

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Size = new Size(260, 65),

                Location = new Point(230, y + 125),

                Cursor = Cursors.Hand
            };

            btnGuardar.FlatAppearance.BorderSize = 0;

            btnGuardar.Click += BtnGuardar_Click;

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

                ForeColor = Color.FromArgb(255, 70, 120),

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
        // CARGAR DATOS (modo edición)
        // =====================================================

        private void CargarDatos()
        {
            if (_tema == null)
                return;

            txtNombre.Text = _tema.Nombre;

            txtDescripcion.Text = _tema.Descripcion;

            // Seleccionar el proyecto que corresponde en el combo
            foreach (ProyectoItem item in cmbProyecto.Items)
            {
                if (item.Id == _tema.IdProyecto)
                {
                    cmbProyecto.SelectedItem = item;
                    break;
                }
            }
        }

        // =====================================================
        // LABEL
        // =====================================================

        private Label CrearLabel(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,

                Font = new Font(
                    "Segoe UI",
                    16,
                    FontStyle.Bold
                ),

                ForeColor = Color.FromArgb(15, 35, 90),

                AutoSize = true,

                Location = new Point(x, y)
            };
        }

        // =====================================================
        // TEXTBOX
        // =====================================================

        private TextBox CrearTextBox(int x, int y, int width)
        {
            return new TextBox
            {
                Font = new Font("Segoe UI", 14),

                Location = new Point(x, y),

                Size = new Size(width, 55),

                BorderStyle = BorderStyle.FixedSingle,

                BackColor = Color.White,

                ForeColor = Color.FromArgb(70, 80, 110)
            };
        }

        // =====================================================
        // GUARDAR
        // =====================================================

        private void BtnGuardar_Click(
            object? sender,
            EventArgs e
        )
        {
            if (string.IsNullOrWhiteSpace(txtNombre.Text))
            {
                MessageBox.Show(
                    "El nombre del tema es obligatorio.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            if (cmbProyecto.SelectedItem == null ||
                ((ProyectoItem)cmbProyecto.SelectedItem).Id == 0)
            {
                MessageBox.Show(
                    "Selecciona un proyecto válido.",
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
                if (_esEdicion && _tema != null)
                {
                    _tema.Nombre = txtNombre.Text.Trim();

                    _tema.Descripcion = txtDescripcion.Text.Trim();

                    _tema.IdProyecto =
                        ((ProyectoItem)cmbProyecto.SelectedItem!).Id;

                    _temaBLL.ActualizarTema(_tema);

                    _bitacoraBLL.Registrar(
                        _usuarioActual,
                        "Temas",
                        "Actualizacion",
                        $"Actualizo el tema '{_tema.Nombre}' (ID {_tema.IdTema})."
                    );

                    MessageBox.Show(
                        "Tema actualizado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                else
                {
                    Tema nuevo = new()
                    {
                        Nombre = txtNombre.Text.Trim(),

                        Descripcion = txtDescripcion.Text.Trim(),

                        IdProyecto =
                            ((ProyectoItem)cmbProyecto.SelectedItem!).Id
                    };

                    _temaBLL.AgregarTema(nuevo);

                    _bitacoraBLL.Registrar(
                        _usuarioActual,
                        "Temas",
                        "Alta",
                        $"Agrego el tema '{nuevo.Nombre}' al proyecto {nuevo.IdProyecto}."
                    );

                    MessageBox.Show(
                        "Tema guardado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error al guardar: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }

    // =========================================================
    // CLASES AUXILIARES
    // =========================================================

    public class ProyectoItem
    {
        public int Id { get; }

        public string Nombre { get; }

        public ProyectoItem(int id, string nombre)
        {
            Id = id;

            Nombre = nombre;
        }

        public override string ToString() => Nombre;
    }
}
