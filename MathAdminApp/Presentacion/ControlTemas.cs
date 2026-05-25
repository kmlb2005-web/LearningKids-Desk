// ============================================================
// CONTROL TEMAS — TEMÁTICA CONSISTENTE CON REFERENCIA
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public partial class ControlTemas : UserControl
    {
        // =====================================================
        // CONTROLES
        // =====================================================

        private DataGridView dgvTemas = null!;

        private Button btnAgregar = null!;
        private Button btnEditar = null!;
        private Button btnEliminar = null!;

        private Panel panelBotones = null!;

        private ComboBox cmbFiltroProyecto = null!;

        private readonly TemaBLL _temaBLL = new();
        private readonly ProyectoBLL _proyectoBLL = new();

        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public ControlTemas()
        {
            InitializeComponent();

            InicializarUI();

            CargarFiltroProyectos();

            CargarTemas();
        }

        // =====================================================
        // DISEÑO
        // =====================================================

        private void InicializarUI()
        {
            // =================================================
            // USERCONTROL
            // =================================================

            this.BackColor = Color.FromArgb(245, 250, 255);

            // =================================================
            // TITULO
            // =================================================

            Label lblTitulo = new Label
            {
                Text = "📖 Gestión de Temas",

                Font = new Font("Segoe UI", 22, FontStyle.Bold),

                ForeColor = Color.FromArgb(20, 35, 80),

                AutoSize = true,

                Location = new Point(20, 20)
            };

            // =================================================
            // SUBTITULO
            // =================================================

            Label lblSubtitulo = new Label
            {
                Text = "Administra los temas de cada proyecto ✨",

                Font = new Font("Segoe UI", 12),

                ForeColor = Color.FromArgb(100, 120, 150),

                AutoSize = true,

                Location = new Point(25, 65)
            };

            // =================================================
            // PANEL BOTONES
            // =================================================

            panelBotones = new Panel
            {
                Dock = DockStyle.Top,

                Height = 80,

                BackColor = Color.Transparent
            };

            // =================================================
            // BOTON AGREGAR
            // =================================================

            btnAgregar = CrearBoton(
                "➕  Agregar",
                Color.FromArgb(66, 133, 244)
            );

            btnAgregar.Location = new Point(20, 15);

            btnAgregar.Click += BtnAgregar_Click;

            // =================================================
            // BOTON EDITAR
            // =================================================

            btnEditar = CrearBoton(
                "✏️  Editar",
                Color.FromArgb(52, 199, 89)
            );

            btnEditar.Location = new Point(210, 15);

            btnEditar.Click += BtnEditar_Click;

            // =================================================
            // BOTON ELIMINAR
            // =================================================

            btnEliminar = CrearBoton(
                "🗑  Eliminar",
                Color.FromArgb(255, 80, 120)
            );

            btnEliminar.Location = new Point(400, 15);

            btnEliminar.Click += BtnEliminar_Click;

            // =================================================
            // LABEL FILTRO
            // =================================================

            Label lblFiltro = new Label
            {
                Text = "🔍 Filtrar por proyecto:",

                Font = new Font("Segoe UI", 11, FontStyle.Bold),

                ForeColor = Color.FromArgb(20, 35, 80),

                AutoSize = true,

                Location = new Point(610, 25)
            };

            // =================================================
            // COMBO FILTRO
            // =================================================

            cmbFiltroProyecto = new ComboBox
            {
                Font = new Font("Segoe UI", 11),

                Location = new Point(810, 20),

                Size = new Size(260, 40),

                DropDownStyle = ComboBoxStyle.DropDownList,

                FlatStyle = FlatStyle.Flat,

                BackColor = Color.White,

                ForeColor = Color.FromArgb(30, 50, 90)
            };

            cmbFiltroProyecto.SelectedIndexChanged +=
                CmbFiltroProyecto_SelectedIndexChanged;

            // =================================================
            // AGREGAR AL PANEL
            // =================================================

            panelBotones.Controls.Add(btnAgregar);

            panelBotones.Controls.Add(btnEditar);

            panelBotones.Controls.Add(btnEliminar);

            panelBotones.Controls.Add(lblFiltro);

            panelBotones.Controls.Add(cmbFiltroProyecto);

            // =================================================
            // PANEL TABLA
            // =================================================

            Panel panelTabla = new Panel
            {
                Dock = DockStyle.Fill,

                Padding = new Padding(20, 200, 20, 20),

                BackColor = Color.Transparent
            };

            // =================================================
            // DATA GRID
            // =================================================

            dgvTemas = new DataGridView
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

            dgvTemas.EnableHeadersVisualStyles = false;

            // =================================================
            // HEADER
            // =================================================

            dgvTemas.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(240, 247, 255);

            dgvTemas.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(50, 70, 120);

            dgvTemas.ColumnHeadersDefaultCellStyle.Font =
                new Font("Segoe UI", 11, FontStyle.Bold);

            dgvTemas.ColumnHeadersHeight = 55;

            // =================================================
            // FILAS
            // =================================================

            dgvTemas.DefaultCellStyle.BackColor = Color.White;

            dgvTemas.DefaultCellStyle.ForeColor =
                Color.FromArgb(60, 70, 100);

            dgvTemas.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(220, 235, 255);

            dgvTemas.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(20, 35, 80);

            dgvTemas.RowTemplate.Height = 50;

            // =================================================
            // COLUMNAS
            // =================================================

            dgvTemas.Columns.Add("idTema", "ID");

            dgvTemas.Columns.Add("nombre", "📌 Nombre");

            dgvTemas.Columns.Add("descripcion", "📝 Descripción");

            dgvTemas.Columns.Add("proyecto", "📋 Proyecto");

            dgvTemas.Columns["idTema"].Visible = false;

            // =================================================
            // AGREGAR TABLA
            // =================================================

            panelTabla.Controls.Add(dgvTemas);

            // =================================================
            // AGREGAR CONTROLES
            // =================================================

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
        // CARGAR FILTRO
        // =====================================================

        private void CargarFiltroProyectos()
        {
            try
            {
                cmbFiltroProyecto.Items.Clear();

                cmbFiltroProyecto.Items.Add(
                    new ProyectoItem(0, "-- Todos los proyectos --")
                );

                var proyectos = _proyectoBLL.ObtenerTodos();

                foreach (var p in proyectos)
                    cmbFiltroProyecto.Items.Add(
                        new ProyectoItem(p.idProyecto, p.nombre)
                    );

                cmbFiltroProyecto.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar proyectos: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // CARGAR TEMAS
        // =====================================================

        private void CargarTemas(int idProyecto = 0)
        {
            try
            {
                dgvTemas.Rows.Clear();

                List<Tema> temas = idProyecto == 0
                    ? _temaBLL.ObtenerTodos()
                    : _temaBLL.ObtenerPorProyecto(idProyecto);

                foreach (var t in temas)
                    dgvTemas.Rows.Add(
                        t.idTema,
                        t.nombre,
                        t.descripcion,
                        t.nombreProyecto
                    );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "Error al cargar temas: " + ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // FILTRO
        // =====================================================

        private void CmbFiltroProyecto_SelectedIndexChanged(
            object? sender, EventArgs e)
        {
            if (cmbFiltroProyecto.SelectedItem is ProyectoItem item)
                CargarTemas(item.Id);
        }

        // =====================================================
        // AGREGAR
        // =====================================================

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            var form = new FormTemaDetalle();

            if (form.ShowDialog() == DialogResult.OK)
                CargarTemas();
        }

        // =====================================================
        // EDITAR
        // =====================================================

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            if (dgvTemas.CurrentRow == null ||
                dgvTemas.CurrentRow.Index < 0)
            {
                MessageBox.Show(
                    "Seleccione un tema para editar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            int idTema = Convert.ToInt32(
                dgvTemas.CurrentRow.Cells["idTema"].Value
            );

            string nombre =
                dgvTemas.CurrentRow.Cells["nombre"].Value.ToString()!;

            string descripcion =
                dgvTemas.CurrentRow.Cells["descripcion"].Value?.ToString()
                ?? string.Empty;

            string proyecto =
                dgvTemas.CurrentRow.Cells["proyecto"].Value?.ToString()
                ?? string.Empty;

            var tema = new Tema
            {
                idTema = idTema,
                nombre = nombre,
                descripcion = descripcion,
                nombreProyecto = proyecto
            };

            var form = new FormTemaDetalle(tema);

            if (form.ShowDialog() == DialogResult.OK)
                CargarTemas();
        }

        // =====================================================
        // ELIMINAR
        // =====================================================

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvTemas.CurrentRow == null ||
                dgvTemas.CurrentRow.Index < 0)
            {
                MessageBox.Show(
                    "Seleccione un tema.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            int idTema = Convert.ToInt32(
                dgvTemas.CurrentRow.Cells["idTema"].Value
            );

            string nombre =
                dgvTemas.CurrentRow.Cells["nombre"].Value.ToString()!;

            var confirm = MessageBox.Show(
                $"¿Desea eliminar el tema \"{nombre}\"?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.Yes)
            {
                try
                {
                    _temaBLL.Eliminar(idTema);

                    CargarTemas();

                    MessageBox.Show(
                        "Tema eliminado correctamente.",
                        "Éxito",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information
                    );
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        "Error al eliminar: " + ex.Message,
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }
    }
}
