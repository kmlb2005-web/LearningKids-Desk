using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public partial class ControlProyectos : UserControl
    {
        // =====================================================
        // CONTROLES
        // =====================================================

        private DataGridView dgvProyectos = null!;

        private Button btnAgregar = null!;
        private Button btnEditar = null!;
        private Button btnDesactivar = null!;
        private Button btnExportar = null!;

        private Panel panelBotones = null!;
        private TextBox txtBuscar = null!;

        private readonly ProyectoBLL _bll = new();
        private readonly BitacoraSistemaBLL _bitacoraBLL = new();
        private readonly Usuario _usuarioActual;

        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public ControlProyectos(Usuario usuarioActual)
        {
            _usuarioActual = usuarioActual;

            InicializarComponentes();

            CargarDatos();
        }

        // =====================================================
        // DISEÑO
        // =====================================================

        private void InicializarComponentes()
        {
            this.BackColor = Color.FromArgb(245, 250, 255);

            // =================================================
            // TITULO
            // =================================================

            Label lblTitulo = new Label
            {
                Text = "Gestion de Proyectos",

                Font = new Font(
                    "Segoe UI",
                    28,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(20, 35, 80),

                AutoSize = true,

                Location = new Point(25, 15)
            };

            // =================================================
            // SUBTITULO
            // =================================================

            Label lblSubtitulo = new Label
            {
                Text =
                    "Administra los proyectos registrados",

                Font = new Font("Segoe UI", 13),

                ForeColor =
                    Color.FromArgb(100, 120, 150),

                AutoSize = true,

                Location = new Point(30, 82)
            };

            // =================================================
            // PANEL BOTONES
            // =================================================

            panelBotones = new Panel
            {
                Dock = DockStyle.Top,

                Height = 220,

                BackColor = Color.Transparent
            };

            // =================================================
            // BOTON AGREGAR
            // =================================================

            btnAgregar = CrearBoton(
                "➕  Agregar",
                Color.FromArgb(66, 133, 244)
            );

            btnAgregar.Location =
                new Point(20, 123);

            btnAgregar.Click += BtnAgregar_Click;

            // =================================================
            // BOTON EDITAR
            // =================================================

            btnEditar = CrearBoton(
                "✏️  Editar",
                Color.FromArgb(52, 199, 89)
            );

            btnEditar.Location =
                new Point(210, 123);

            btnEditar.Click += BtnEditar_Click;

            // =================================================
            // BOTON DESACTIVAR
            // =================================================

            btnDesactivar = CrearBoton(
                "🚫  Eliminar",
                Color.FromArgb(255, 80, 120)
            );

            btnDesactivar.Location =
                new Point(400, 123);

            btnDesactivar.Click +=
                BtnDesactivar_Click;

            btnExportar = CrearBoton(
                "📄  Exportar Excel",
                Color.FromArgb(22, 160, 133)
            );

            btnExportar.Size = new Size(210, 45);
            btnExportar.Location = new Point(910, 123);
            btnExportar.Click += BtnExportar_Click;

            txtBuscar = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Size = new Size(300, 40),
                Location = new Point(590, 126),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(60, 70, 100),
                PlaceholderText = "Nombre de proyecto..."
            };

            txtBuscar.TextChanged += TxtBuscar_TextChanged;

            panelBotones.Controls.Add(
                btnAgregar
            );

            panelBotones.Controls.Add(
                btnEditar
            );

            panelBotones.Controls.Add(
                btnDesactivar
            );

            panelBotones.Controls.Add(
                txtBuscar
            );

            panelBotones.Controls.Add(
                btnExportar
            );

            panelBotones.Controls.Add(
                lblTitulo
            );

            panelBotones.Controls.Add(
                lblSubtitulo
            );

            // =================================================
            // PANEL TABLA
            // =================================================

            Panel panelTabla = new Panel
            {
                Dock = DockStyle.Fill,

                Padding =
                    new Padding(25, 0, 25, 25),

                BackColor = Color.Transparent
            };

            // =================================================
            // DATA GRID
            // =================================================

            dgvProyectos = new DataGridView
            {
                Dock = DockStyle.Fill,

                BackgroundColor = Color.White,

                BorderStyle = BorderStyle.None,

                CellBorderStyle =
                    DataGridViewCellBorderStyle
                    .SingleHorizontal,

                ColumnHeadersBorderStyle =
                    DataGridViewHeaderBorderStyle
                    .None,

                SelectionMode =
                    DataGridViewSelectionMode
                    .FullRowSelect,

                MultiSelect = false,

                ReadOnly = true,

                AllowUserToAddRows = false,

                AllowUserToDeleteRows = false,

                AllowUserToResizeRows = false,

                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode
                    .Fill,

                RowHeadersVisible = false,

                Font = new Font(
                    "Segoe UI",
                    11
                ),

                GridColor =
                    Color.FromArgb(230, 235, 245)
            };

            dgvProyectos.EnableHeadersVisualStyles =
                false;

            // HEADER

            dgvProyectos
                .ColumnHeadersDefaultCellStyle
                .BackColor =
                Color.FromArgb(240, 247, 255);

            dgvProyectos
                .ColumnHeadersDefaultCellStyle
                .ForeColor =
                Color.FromArgb(50, 70, 120);

            dgvProyectos
                .ColumnHeadersDefaultCellStyle
                .Font =
                new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold
                );

            dgvProyectos.ColumnHeadersHeight = 55;

            // FILAS

            dgvProyectos.DefaultCellStyle.BackColor =
                Color.White;

            dgvProyectos.DefaultCellStyle.ForeColor =
                Color.FromArgb(60, 70, 100);

            dgvProyectos
                .DefaultCellStyle
                .SelectionBackColor =
                Color.FromArgb(220, 235, 255);

            dgvProyectos
                .DefaultCellStyle
                .SelectionForeColor =
                Color.FromArgb(20, 35, 80);

            dgvProyectos.RowTemplate.Height = 50;

            panelTabla.Controls.Add(
                dgvProyectos
            );

            // =================================================
            // AGREGAR CONTROLES
            // =================================================

            this.Controls.Add(panelTabla);

            this.Controls.Add(panelBotones);
        }

        // =====================================================
        // BOTON
        // =====================================================

        private Button CrearBoton(
            string texto,
            Color color
        )
        {
            Button btn = new Button
            {
                Text = texto,

                Font = new Font(
                    "Segoe UI",
                    11,
                    FontStyle.Bold
                ),

                BackColor = color,

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Size = new Size(170, 45),

                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;

            btn.MouseEnter += (s, e) =>
            {
                btn.BackColor =
                    ControlPaint.Light(color);
            };

            btn.MouseLeave += (s, e) =>
            {
                btn.BackColor = color;
            };

            return btn;
        }

        // =====================================================
        // CARGAR DATOS
        // =====================================================

        private void CargarDatos()
        {
            try
            {
                var proyectos =
                    _bll.ObtenerVisibles(_usuarioActual);

                dgvProyectos.DataSource = null;

                dgvProyectos.DataSource =
                    proyectos;

                if (
                    dgvProyectos.Columns.Contains(
                        "idProyecto"
                    )
                )
                {
                    dgvProyectos.Columns[
                        "idProyecto"
                    ].Visible = false;
                }

                dgvProyectos.Columns["nombre"]
                    .HeaderText = "📁 Nombre";

                dgvProyectos.Columns["descripcion"]
                    .HeaderText =
                    "📝 Descripción";

                dgvProyectos.Columns["grado"]
                    .HeaderText = "Grado";

                dgvProyectos.Columns["idCampo"]
                    .HeaderText = "Campo";

                dgvProyectos.Columns["creadoPor"]
                    .HeaderText = "Creado por";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error:\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void TxtBuscar_TextChanged(object? sender, EventArgs e)
        {
            BuscarProyectos();
        }

        private void BuscarProyectos()
        {
            try
            {
                string texto = txtBuscar.Text.Trim().ToLower();
                var proyectos = _bll.ObtenerVisibles(_usuarioActual);

                if (!string.IsNullOrWhiteSpace(texto))
                {
                    proyectos = proyectos
                        .Where(p =>
                            p.nombre.ToLower().Contains(texto)
                            || p.descripcion.ToLower().Contains(texto))
                        .ToList();
                }

                dgvProyectos.DataSource = null;
                dgvProyectos.DataSource = proyectos;
            }
            catch
            {
            }
        }

        private void BtnExportar_Click(object? sender, EventArgs e)
        {
            ExportadorExcel.Exportar(dgvProyectos, "proyectos");
        }

        // =====================================================
        // AGREGAR
        // =====================================================

        private void BtnAgregar_Click(
            object? sender,
            EventArgs e
        )
        {
                var form =
                new FormProyectoDetalle(null, _usuarioActual);

            if (
                form.ShowDialog()
                == DialogResult.OK
            )
            {
                CargarDatos();
            }
        }

        // =====================================================
        // EDITAR
        // =====================================================

        private void BtnEditar_Click(
            object? sender,
            EventArgs e
        )
        {
            if (dgvProyectos.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un proyecto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var proyecto =
                (Proyecto)dgvProyectos
                .CurrentRow.DataBoundItem;

            var form =
                new FormProyectoDetalle(
                    proyecto,
                    _usuarioActual
                );

            if (
                form.ShowDialog()
                == DialogResult.OK
            )
            {
                CargarDatos();
            }
        }

        // =====================================================
        // ELIMINAR
        // =====================================================

        private void BtnDesactivar_Click(
            object? sender,
            EventArgs e
        )
        {
            if (dgvProyectos.CurrentRow == null)
            {
                MessageBox.Show(
                    "Seleccione un proyecto.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            var proyecto =
                (Proyecto)dgvProyectos
                .CurrentRow.DataBoundItem;

            var resultado =
                MessageBox.Show(
                    $"¿Desea desactivar el proyecto '{proyecto.nombre}'?",
                    "Confirmar",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question
                );

            if (resultado == DialogResult.Yes)
            {
                _bll.Eliminar(
                    proyecto.idProyecto
                );

                _bitacoraBLL.Registrar(
                    _usuarioActual,
                    "Proyectos",
                    "Desactivacion",
                    $"Desactivo el proyecto '{proyecto.nombre}' (ID {proyecto.idProyecto})."
                );

                CargarDatos();

                MessageBox.Show(
                    "Proyecto desactivado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
        }
    }
}
