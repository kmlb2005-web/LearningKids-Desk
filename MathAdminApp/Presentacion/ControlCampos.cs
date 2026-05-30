using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public partial class ControlCampos : UserControl
    {
        // =====================================================
        // CONTROLES
        // =====================================================

        private DataGridView dgvCampos = null!;

        private TextBox txtBuscar = null!;

        private Button btnAgregar = null!;

        private Button btnEditar = null!;

        private Button btnEliminar = null!;

        // =====================================================
        // BLL
        // =====================================================

        private readonly CampoFormativoBLL _campoBLL = new();
        private readonly BitacoraSistemaBLL _bitacoraBLL = new();
        private readonly Usuario? _usuarioActual;

        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public ControlCampos() : this(null)
        {
        }

        public ControlCampos(Usuario? usuarioActual)
        {
            _usuarioActual = usuarioActual;

            InicializarComponentes();

            CargarCampos();
        }

        // =====================================================
        // INTERFAZ
        // =====================================================

        private void InicializarComponentes()
        {
            this.BackColor =
                Color.FromArgb(245, 248, 255);

            // =================================================
            // PANEL SUPERIOR
            // =================================================

            Panel panelSuperior = new Panel
            {
                Dock = DockStyle.Top,

                Height = 220,

                BackColor = Color.Transparent
            };

            // =================================================
            // TITULO
            // =================================================

            Label lblTitulo = new Label
            {
                Text = "Gestion de Campos",

                Font = new Font(
                    "Segoe UI",
                    28,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(15, 35, 90),

                AutoSize = true,

                Location = new Point(25, 15)
            };

            // =================================================
            // SUBTITULO
            // =================================================

            Label lblSubtitulo = new Label
            {
                Text =
                    "Administra los campos formativos registrados",

                Font = new Font(
                    "Segoe UI",
                    13
                ),

                ForeColor =
                    Color.FromArgb(110, 120, 150),

                AutoSize = true,

                Location = new Point(30, 82)
            };

            // =================================================
            // BOTON AGREGAR
            // =================================================

            btnAgregar = CrearBoton(
                "➕ Agregar",
                Color.FromArgb(50, 120, 255)
            );

            btnAgregar.Location =
                new Point(30, 123);

            btnAgregar.Click += BtnAgregar_Click;

            // =================================================
            // BOTON EDITAR
            // =================================================

            btnEditar = CrearBoton(
                "✏️ Editar",
                Color.FromArgb(52, 199, 89)
            );

            btnEditar.Location =
                new Point(230, 123);

            btnEditar.Click += BtnEditar_Click;

            // =================================================
            // BOTON ELIMINAR
            // =================================================

            btnEliminar = CrearBoton(
                "🗑️ Eliminar",
                Color.FromArgb(255, 70, 120)
            );

            btnEliminar.Location =
                new Point(430, 123);

            btnEliminar.Click += BtnEliminar_Click;

            // =================================================
            // BUSCADOR
            // =================================================

            txtBuscar = new TextBox
            {
                PlaceholderText =
                    "🔍 Buscar campo...",

                Font = new Font(
                    "Segoe UI",
                    12
                ),

                Size = new Size(250, 45),

                Location = new Point(960, 130),

                BorderStyle =
                    BorderStyle.FixedSingle,

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(60, 70, 100)
            };

            txtBuscar.TextChanged +=
                TxtBuscar_TextChanged;

            // =================================================
            // AGREGAR CONTROLES
            // =================================================

            panelSuperior.Controls.Add(lblTitulo);

            panelSuperior.Controls.Add(lblSubtitulo);

            panelSuperior.Controls.Add(btnAgregar);

            panelSuperior.Controls.Add(btnEditar);

            panelSuperior.Controls.Add(btnEliminar);

            panelSuperior.Controls.Add(txtBuscar);

            // =================================================
            // TABLA
            // =================================================

            dgvCampos = new DataGridView
            {
                Dock = DockStyle.Fill,

                BackgroundColor = Color.White,

                BorderStyle = BorderStyle.None,

                CellBorderStyle =
                    DataGridViewCellBorderStyle.SingleHorizontal,

                ColumnHeadersBorderStyle =
                    DataGridViewHeaderBorderStyle.None,

                SelectionMode =
                    DataGridViewSelectionMode.RowHeaderSelect,

                MultiSelect = false,

                ReadOnly = true,

                AllowUserToAddRows = false,

                AllowUserToDeleteRows = false,

                AllowUserToResizeRows = false,

                AutoSizeColumnsMode =
                    DataGridViewAutoSizeColumnsMode.Fill,

                RowHeadersVisible = false,

                Font = new Font(
                    "Segoe UI",
                    11
                ),

                GridColor =
                    Color.FromArgb(235, 240, 250),

                RowTemplate =
                {
                    Height = 55
                }
            };

            dgvCampos.EnableHeadersVisualStyles = false;

            dgvCampos.CellClick +=
                DgvCampos_CellClick;

            dgvCampos.ColumnHeadersDefaultCellStyle.BackColor =
                Color.FromArgb(245, 248, 255);

            dgvCampos.ColumnHeadersDefaultCellStyle.ForeColor =
                Color.FromArgb(20, 35, 90);

            dgvCampos.ColumnHeadersDefaultCellStyle.Font =
                new Font(
                    "Segoe UI",
                    12,
                    FontStyle.Bold
                );

            dgvCampos.ColumnHeadersHeight = 60;

            dgvCampos.DefaultCellStyle.BackColor =
                Color.White;

            dgvCampos.DefaultCellStyle.ForeColor =
                Color.FromArgb(40, 50, 80);

            dgvCampos.DefaultCellStyle.SelectionBackColor =
                Color.FromArgb(230, 240, 255);

            dgvCampos.DefaultCellStyle.SelectionForeColor =
                Color.FromArgb(20, 35, 90);

            dgvCampos.DefaultCellStyle.Padding =
                new Padding(8);

            // =================================================
            // PANEL TABLA
            // =================================================

            Panel panelTabla = new Panel
            {
                Dock = DockStyle.Fill,

                Padding =
                    new Padding(25, 0, 25, 25),

                BackColor =
                    Color.Transparent
            };

            panelTabla.Controls.Add(dgvCampos);

            // =================================================
            // AGREGAR
            // =================================================

            this.Controls.Add(panelTabla);

            this.Controls.Add(panelSuperior);
        }

        // =====================================================
        // CREAR BOTON
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
                    12,
                    FontStyle.Bold
                ),

                BackColor = color,

                ForeColor = Color.White,

                FlatStyle =
                    FlatStyle.Flat,

                Size =
                    new Size(180, 55),

                Cursor =
                    Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;

            return btn;
        }

        // =====================================================
        // CARGAR CAMPOS
        // =====================================================

        private void CargarCampos()
        {
            try
            {
                dgvCampos.CellClick -=
                    DgvCampos_CellClick;

                dgvCampos.DataSource = null;

                var lista =
                    _campoBLL.ObtenerTodos();

                dgvCampos.DataSource = lista;

                if (dgvCampos.Columns.Contains("IdCampo"))
                {
                    dgvCampos.Columns["IdCampo"]
                        .HeaderText = "ID";
                }

                if (dgvCampos.Columns.Contains("Nombre"))
                {
                    dgvCampos.Columns["Nombre"]
                        .HeaderText =
                            "Campo Formativo";
                }

                dgvCampos.ClearSelection();

                dgvCampos.CellClick +=
                    DgvCampos_CellClick;
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private string? PedirNombreCampo(string titulo, string valorInicial = "")
        {
            using Form dialogo = new Form
            {
                Text = titulo,
                StartPosition = FormStartPosition.CenterParent,
                FormBorderStyle = FormBorderStyle.FixedDialog,
                MinimizeBox = false,
                MaximizeBox = false,
                ClientSize = new Size(420, 145),
                BackColor = Color.White
            };

            TextBox txtNombreCampo = new TextBox
            {
                Text = valorInicial,
                Font = new Font("Segoe UI", 12),
                Location = new Point(20, 25),
                Size = new Size(380, 35),
                BorderStyle = BorderStyle.FixedSingle
            };

            Button btnAceptar = new Button
            {
                Text = "Aceptar",
                DialogResult = DialogResult.OK,
                Location = new Point(220, 85),
                Size = new Size(85, 35)
            };

            Button btnCancelar = new Button
            {
                Text = "Cancelar",
                DialogResult = DialogResult.Cancel,
                Location = new Point(315, 85),
                Size = new Size(85, 35)
            };

            dialogo.Controls.Add(txtNombreCampo);
            dialogo.Controls.Add(btnAceptar);
            dialogo.Controls.Add(btnCancelar);
            dialogo.AcceptButton = btnAceptar;
            dialogo.CancelButton = btnCancelar;

            if (dialogo.ShowDialog(this) != DialogResult.OK)
            {
                return null;
            }

            return txtNombreCampo.Text.Trim();
        }

        // =====================================================
        // AGREGAR
        // =====================================================

        private void BtnAgregar_Click(
            object? sender,
            EventArgs e
        )
        {
            try
            {
                string? nombre = PedirNombreCampo("Agregar campo formativo");

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return;
                }

                CampoFormativo campo =
                    new CampoFormativo
                    {
                        Nombre =
                            nombre
                    };

                _campoBLL.Agregar(campo);

                _bitacoraBLL.Registrar(
                    _usuarioActual,
                    "Campos",
                    "Alta",
                    $"Agrego el campo formativo '{campo.Nombre}'."
                );

                CargarCampos();

                MessageBox.Show(
                    "Campo agregado correctamente."
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message
                );
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
            if (
                dgvCampos.CurrentRow == null
                ||
                dgvCampos.CurrentRow.Index < 0
                ||
                dgvCampos.Rows.Count == 0
            )
            {
                MessageBox.Show(
                    "Seleccione un campo para editar.",
                    "Aviso",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );

                return;
            }

            try
            {
                if (
                    dgvCampos.CurrentRow.DataBoundItem
                    is not CampoFormativo campo
                )
                {
                    MessageBox.Show(
                        "No se pudo obtener el campo seleccionado.",
                        "Error",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );

                    return;
                }

                string? nombre = PedirNombreCampo(
                    "Editar campo formativo",
                    campo.Nombre
                );

                if (string.IsNullOrWhiteSpace(nombre))
                {
                    return;
                }

                campo.Nombre =
                    nombre;

                _campoBLL.Editar(campo);

                _bitacoraBLL.Registrar(
                    _usuarioActual,
                    "Campos",
                    "Actualizacion",
                    $"Actualizo el campo formativo '{campo.Nombre}' (ID {campo.IdCampo})."
                );

                CargarCampos();

                MessageBox.Show(
                    "Campo actualizado correctamente.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        // =====================================================
        // ELIMINAR
        // =====================================================

        private void BtnEliminar_Click(
            object? sender,
            EventArgs e
        )
        {
            if (
                dgvCampos.CurrentRow == null
                ||
                dgvCampos.CurrentRow.Index < 0
            )
            {
                MessageBox.Show(
                    "Seleccione un campo."
                );

                return;
            }

            try
            {
                if (
                    dgvCampos.CurrentRow.DataBoundItem
                    is not CampoFormativo campo
                )
                {
                    MessageBox.Show(
                        "No se pudo obtener el campo."
                    );

                    return;
                }

                DialogResult resultado =
                    MessageBox.Show(
                        $"¿Desactivar '{campo.Nombre}'?",
                        "Confirmar",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question
                    );

                if (resultado == DialogResult.Yes)
                {
                    _campoBLL.Eliminar(
                        campo.IdCampo
                    );

                    _bitacoraBLL.Registrar(
                        _usuarioActual,
                        "Campos",
                        "Desactivacion",
                        $"Desactivo el campo formativo '{campo.Nombre}' (ID {campo.IdCampo})."
                    );

                    CargarCampos();

                    MessageBox.Show(
                        "Campo desactivado correctamente."
                    );
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    ex.Message
                );
            }
        }

        // =====================================================
        // CELL CLICK
        // =====================================================

        private void DgvCampos_CellClick(
            object? sender,
            DataGridViewCellEventArgs e
        )
        {
            try
            {
                if (
                    e.RowIndex < 0
                    ||
                    e.RowIndex >= dgvCampos.Rows.Count
                    ||
                    dgvCampos.Rows.Count == 0
                )
                {
                    return;
                }

                if (
                    dgvCampos.Rows[e.RowIndex]
                        .DataBoundItem
                    is not CampoFormativo campo
                )
                {
                    return;
                }

            }
            catch
            {
            }
        }

        // =====================================================
        // BUSCAR
        // =====================================================

        private void TxtBuscar_TextChanged(
            object? sender,
            EventArgs e
        )
        {
            BuscarCampos();
        }

        private void BuscarCampos()
        {
            try
            {
                string texto =
                    txtBuscar.Text
                        .Trim()
                        .ToLower();

                var lista =
                    _campoBLL.ObtenerTodos();

                if (!string.IsNullOrWhiteSpace(texto))
                {
                    lista = lista
                        .Where(x =>
                            x.Nombre
                                .ToLower()
                                .Contains(texto)
                        )
                        .ToList();
                }

                dgvCampos.DataSource = null;

                dgvCampos.DataSource = lista;

                dgvCampos.ClearSelection();
            }
            catch
            {
            }
        }
    }
}
