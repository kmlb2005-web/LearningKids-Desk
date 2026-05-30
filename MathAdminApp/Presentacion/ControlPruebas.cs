using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class ControlPruebas : UserControl
    {
        private ComboBox cmbTema = null!;
        private DataGridView dgvPruebas = null!;
        private TextBox txtTitulo = null!;
        private Button btnAgregar = null!;
        private Button btnEliminar = null!;
        private Button btnExportar = null!;
        private TextBox txtBuscar = null!;

        private readonly PruebaBLL _pruebaBll = new();
        private readonly TemaBLL _temaBll = new();
        private readonly BitacoraSistemaBLL _bitacoraBLL = new();
        private readonly Usuario _usuarioActual;
        private List<Tema> _temas = new();

        public ControlPruebas(Usuario usuarioActual)
        {
            _usuarioActual = usuarioActual;

            InicializarComponentes();
            CargarTemas();
        }

        private void InicializarComponentes()
        {
            BackColor = Color.FromArgb(245, 248, 255);

            Panel panelSuperior = new()
            {
                Dock = DockStyle.Top,
                Height = 285,
                BackColor = Color.Transparent
            };

            Label lblTitulo = new()
            {
                Text = "Gestion de Pruebas",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 35, 90),
                AutoSize = true,
                Location = new Point(25, 15)
            };

            Label lblSubtitulo = new()
            {
                Text = "Administra las pruebas registradas",
                Font = new Font("Segoe UI", 13),
                ForeColor = Color.FromArgb(110, 120, 150),
                AutoSize = true,
                Location = new Point(30, 82)
            };

            Label lblFiltro = new()
            {
                Text = "Filtrar por tema",
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(30, 50, 90),
                AutoSize = true,
                Location = new Point(30, 120)
            };

            cmbTema = new ComboBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(30, 155),
                Size = new Size(320, 45),
                DropDownStyle = ComboBoxStyle.DropDownList,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 70, 120)
            };
            cmbTema.SelectedIndexChanged += CmbTema_SelectedIndexChanged;

            txtTitulo = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(380, 155),
                Size = new Size(300, 45),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 70, 120),
                PlaceholderText = "Titulo de la prueba..."
            };

            txtBuscar = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(30, 220),
                Size = new Size(360, 45),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 70, 120),
                PlaceholderText = "Buscar prueba..."
            };

            txtBuscar.TextChanged += TxtBuscar_TextChanged;

            btnAgregar = CrearBoton("Agregar", Color.FromArgb(50, 120, 255));
            btnAgregar.Location = new Point(710, 148);
            btnAgregar.Click += BtnAgregar_Click;

            btnEliminar = CrearBoton("Eliminar", Color.FromArgb(255, 70, 120));
            btnEliminar.Location = new Point(900, 148);
            btnEliminar.Click += BtnEliminar_Click;

            btnExportar = CrearBoton("Exportar Excel", Color.FromArgb(22, 160, 133));
            btnExportar.Location = new Point(410, 215);
            btnExportar.Size = new Size(210, 55);
            btnExportar.Click += BtnExportar_Click;

            panelSuperior.Controls.Add(lblTitulo);
            panelSuperior.Controls.Add(lblSubtitulo);
            panelSuperior.Controls.Add(lblFiltro);
            panelSuperior.Controls.Add(cmbTema);
            panelSuperior.Controls.Add(txtTitulo);
            panelSuperior.Controls.Add(btnAgregar);
            panelSuperior.Controls.Add(btnEliminar);
            panelSuperior.Controls.Add(txtBuscar);
            panelSuperior.Controls.Add(btnExportar);

            dgvPruebas = CrearGrid();

            Panel panelTabla = new()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(25, 10, 25, 25),
                BackColor = Color.Transparent
            };
            panelTabla.Controls.Add(dgvPruebas);

            Controls.Add(panelTabla);
            Controls.Add(panelSuperior);
        }

        private Button CrearBoton(string texto, Color color)
        {
            Button btn = new()
            {
                Text = texto,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(180, 55),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private DataGridView CrearGrid()
        {
            DataGridView grid = new()
            {
                Dock = DockStyle.Fill,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AllowUserToResizeRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 11),
                GridColor = Color.FromArgb(235, 240, 250),
                RowTemplate = { Height = 55 }
            };

            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(245, 248, 255);
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(20, 35, 90);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            grid.ColumnHeadersHeight = 60;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(230, 240, 255);
            grid.DefaultCellStyle.SelectionForeColor = Color.FromArgb(20, 35, 90);
            grid.DefaultCellStyle.Padding = new Padding(8);

            return grid;
        }

        private void CargarTemas()
        {
            try
            {
                _temas = _temaBll.ObtenerVisibles(_usuarioActual);
                cmbTema.Items.Clear();
                cmbTema.Items.Add("-- Todos los temas --");

                foreach (var tema in _temas)
                    cmbTema.Items.Add(tema.Nombre);

                cmbTema.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar temas:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPruebas(int? idTema = null)
        {
            try
            {
                var pruebas = _pruebaBll.ObtenerVisibles(_usuarioActual, idTema);
                dgvPruebas.DataSource = null;
                dgvPruebas.DataSource = pruebas;

                if (dgvPruebas.Columns.Contains("IdPrueba"))
                    dgvPruebas.Columns["IdPrueba"].Visible = false;
                if (dgvPruebas.Columns.Contains("Titulo"))
                    dgvPruebas.Columns["Titulo"].HeaderText = "Titulo";
                if (dgvPruebas.Columns.Contains("IdTema"))
                    dgvPruebas.Columns["IdTema"].HeaderText = "Tema";
                if (dgvPruebas.Columns.Contains("CreadoPor"))
                    dgvPruebas.Columns["CreadoPor"].HeaderText = "Creado por";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar pruebas:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbTema_SelectedIndexChanged(object? sender, EventArgs e)
        {
            if (cmbTema.SelectedIndex <= 0)
            {
                CargarPruebas();
                return;
            }

            CargarPruebas(_temas[cmbTema.SelectedIndex - 1].IdTema);
        }

        private void TxtBuscar_TextChanged(object? sender, EventArgs e)
        {
            BuscarPruebas();
        }

        private void BuscarPruebas()
        {
            try
            {
                string texto = txtBuscar.Text.Trim().ToLower();
                int? idTema = cmbTema.SelectedIndex > 0
                    ? _temas[cmbTema.SelectedIndex - 1].IdTema
                    : null;

                var pruebas = _pruebaBll.ObtenerVisibles(_usuarioActual, idTema);
                if (!string.IsNullOrWhiteSpace(texto))
                    pruebas = pruebas.Where(p => p.Titulo.ToLower().Contains(texto)).ToList();

                dgvPruebas.DataSource = null;
                dgvPruebas.DataSource = pruebas;
            }
            catch
            {
            }
        }

        private void BtnExportar_Click(object? sender, EventArgs e)
        {
            ExportadorExcel.Exportar(dgvPruebas, "pruebas");
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            if (cmbTema.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione un tema.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            try
            {
                var tema = _temas[cmbTema.SelectedIndex - 1];
                var prueba = new Prueba
                {
                    Titulo = txtTitulo.Text.Trim(),
                    IdTema = tema.IdTema,
                    CreadoPor = _usuarioActual.IdUsuario
                };

                _pruebaBll.Agregar(prueba);
                _bitacoraBLL.Registrar(
                    _usuarioActual,
                    "Pruebas",
                    "Alta",
                    $"Agrego la prueba '{prueba.Titulo}' al tema '{tema.Nombre}'."
                );

                txtTitulo.Clear();
                CargarPruebas(tema.IdTema);
                MessageBox.Show("Prueba agregada correctamente.", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvPruebas.CurrentRow?.DataBoundItem is not Prueba prueba)
            {
                MessageBox.Show("Seleccione una prueba.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var resultado = MessageBox.Show(
                $"Desea eliminar la prueba '{prueba.Titulo}'?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
                return;

            try
            {
                _pruebaBll.Eliminar(prueba.IdPrueba);
                _bitacoraBLL.Registrar(
                    _usuarioActual,
                    "Pruebas",
                    "Eliminacion",
                    $"Elimino la prueba '{prueba.Titulo}' (ID {prueba.IdPrueba})."
                );

                CmbTema_SelectedIndexChanged(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
