using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class ControlPreguntas : UserControl
    {
        private ComboBox cmbPrueba = null!;
        private DataGridView dgvPreguntas = null!;
        private Button btnAgregar = null!;
        private Button btnEditar = null!;
        private Button btnDesactivar = null!;
        private Button btnExportar = null!;
        private TextBox txtBuscar = null!;

        private readonly PreguntaBLL _preguntaBll = new();
        private readonly PruebaBLL _pruebaBll = new();
        private readonly BitacoraSistemaBLL _bitacoraBLL = new();
        private readonly Usuario _usuarioActual;
        private List<Prueba> _pruebas = new();

        public ControlPreguntas(Usuario usuarioActual)
        {
            _usuarioActual = usuarioActual;

            InicializarComponentes();
            CargarPruebas();
        }

        private void InicializarComponentes()
        {
            BackColor = Color.FromArgb(245, 250, 255);

            Label lblTitulo = new()
            {
                Text = "Gestion de Preguntas",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 80),
                AutoSize = true,
                Location = new Point(20, 15)
            };

            Label lblSubtitulo = new()
            {
                Text = "Administra las preguntas de cada prueba",
                Font = new Font("Segoe UI", 13),
                ForeColor = Color.FromArgb(100, 120, 150),
                AutoSize = true,
                Location = new Point(25, 82)
            };

            Label lblPrueba = new()
            {
                Text = "Seleccionar prueba:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 90),
                AutoSize = true,
                Location = new Point(25, 125)
            };

            cmbPrueba = new ComboBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(250, 118),
                Size = new Size(420, 40),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 70, 120)
            };
            cmbPrueba.SelectedIndexChanged += CmbPrueba_SelectedIndexChanged;

            // =================================================
            // BOTON AGREGAR
            // =================================================

            btnAgregar = CrearBoton(
                "➕  Agregar",
                Color.FromArgb(66, 133, 244)
            );

            btnAgregar.Location =
                new Point(20, 185);

            btnAgregar.Click += BtnAgregar_Click;

            // =================================================
            // BOTON EDITAR
            // =================================================

            btnEditar = CrearBoton(
                "✏️  Editar",
                Color.FromArgb(52, 199, 89)
            );

            btnEditar.Location =
                new Point(210, 185);

            btnEditar.Click += BtnEditar_Click;

            // =================================================
            // BOTON DESACTIVAR
            // =================================================

            btnDesactivar = CrearBoton(
                "🚫  Eliminar",
                Color.FromArgb(255, 80, 120)
            );

            btnDesactivar.Location =
                new Point(400, 185);

            btnDesactivar.Click += BtnEliminar_Click;

            btnExportar = CrearBoton(
                "Exportar Excel",
                Color.FromArgb(22, 160, 133)
            );

            btnExportar.Size = new Size(210, 45);
            btnExportar.Location = new Point(980, 185);
            btnExportar.Click += BtnExportar_Click;

            txtBuscar = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(610, 188),
                Size = new Size(340, 40),
                BorderStyle = BorderStyle.FixedSingle,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 70, 120),
                PlaceholderText = "Buscar pregunta..."
            };

            txtBuscar.TextChanged += TxtBuscar_TextChanged;

            dgvPreguntas = new DataGridView
            {
                Location = new Point(20, 280),
                Size = new Size(1320, 520),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
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
                GridColor = Color.FromArgb(230, 235, 245)
            };

            dgvPreguntas.EnableHeadersVisualStyles = false;
            dgvPreguntas.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 247, 255);
            dgvPreguntas.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(50, 70, 120);
            dgvPreguntas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvPreguntas.ColumnHeadersHeight = 55;
            dgvPreguntas.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 255);
            dgvPreguntas.DefaultCellStyle.SelectionForeColor = Color.FromArgb(20, 35, 80);
            dgvPreguntas.RowTemplate.Height = 50;

            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblPrueba);
            Controls.Add(cmbPrueba);
            Controls.Add(btnAgregar);
            Controls.Add(btnEditar);
            Controls.Add(btnDesactivar);
            Controls.Add(txtBuscar);
            Controls.Add(btnExportar);
            Controls.Add(dgvPreguntas);
        }

        private Button CrearBoton(string texto, Color color)
        {
            Button btn = new()
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
            return btn;
        }

        private void CargarPruebas()
        {
            try
            {
                _pruebas = _pruebaBll.ObtenerVisibles(_usuarioActual);
                cmbPrueba.Items.Clear();
                cmbPrueba.Items.Add("-- Todas las pruebas --");

                foreach (var prueba in _pruebas)
                    cmbPrueba.Items.Add(prueba.Titulo);

                cmbPrueba.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar pruebas:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarPreguntas(int? idPrueba = null)
        {
            try
            {
                List<Pregunta> preguntas;

                if (idPrueba.HasValue)
                {
                    preguntas = _preguntaBll.ObtenerPorPrueba(idPrueba.Value);
                }
                else
                {
                    preguntas = _pruebas
                        .SelectMany(p => _preguntaBll.ObtenerPorPrueba(p.IdPrueba))
                        .ToList();
                }

                dgvPreguntas.DataSource = null;
                dgvPreguntas.DataSource = preguntas;

                if (dgvPreguntas.Columns.Contains("IdPregunta"))
                    dgvPreguntas.Columns["IdPregunta"].Visible = false;
                if (dgvPreguntas.Columns.Contains("Texto"))
                    dgvPreguntas.Columns["Texto"].HeaderText = "Pregunta";
                if (dgvPreguntas.Columns.Contains("IdPrueba"))
                    dgvPreguntas.Columns["IdPrueba"].HeaderText = "Prueba";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar preguntas:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbPrueba_SelectedIndexChanged(object? sender, EventArgs e)
        {
            BuscarPreguntas();
        }

        private void TxtBuscar_TextChanged(object? sender, EventArgs e)
        {
            BuscarPreguntas();
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            BuscarPreguntas();
        }

        private void BuscarPreguntas()
        {
            try
            {
                int? idPrueba = cmbPrueba.SelectedIndex > 0
                    ? _pruebas[cmbPrueba.SelectedIndex - 1].IdPrueba
                    : null;

                List<Pregunta> preguntas = idPrueba.HasValue
                    ? _preguntaBll.ObtenerPorPrueba(idPrueba.Value)
                    : _pruebas
                        .SelectMany(p => _preguntaBll.ObtenerPorPrueba(p.IdPrueba))
                        .ToList();

                string texto = txtBuscar.Text.Trim().ToLower();

                if (!string.IsNullOrWhiteSpace(texto))
                {
                    preguntas = preguntas
                        .Where(p => p.Texto.ToLower().Contains(texto))
                        .ToList();
                }

                dgvPreguntas.DataSource = null;
                dgvPreguntas.DataSource = preguntas;
            }
            catch
            {
            }
        }

        private void BtnExportar_Click(object? sender, EventArgs e)
        {
            ExportadorExcel.Exportar(dgvPreguntas, "preguntas");
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            if (cmbPrueba.SelectedIndex <= 0)
            {
                MessageBox.Show("Seleccione una prueba.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var prueba = _pruebas[cmbPrueba.SelectedIndex - 1];
            var form = new FormPreguntaDetalle(prueba.IdPrueba);

            if (form.ShowDialog() == DialogResult.OK)
            {
                _bitacoraBLL.Registrar(
                    _usuarioActual,
                    "Preguntas",
                    "Alta",
                    $"Agrego una pregunta a la prueba '{prueba.Titulo}' (ID {prueba.IdPrueba})."
                );

                CargarPreguntas(prueba.IdPrueba);
            }
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            if (dgvPreguntas.CurrentRow?.DataBoundItem is not Pregunta pregunta)
            {
                MessageBox.Show("Seleccione una pregunta.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var form = new FormPreguntaDetalle(pregunta.IdPrueba, pregunta);

            if (form.ShowDialog() == DialogResult.OK)
            {
                _bitacoraBLL.Registrar(
                    _usuarioActual,
                    "Preguntas",
                    "Actualizacion",
                    $"Actualizo una pregunta de la prueba ID {pregunta.IdPrueba}."
                );

                CargarPreguntas(pregunta.IdPrueba);
            }
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvPreguntas.CurrentRow?.DataBoundItem is not Pregunta pregunta)
            {
                MessageBox.Show("Seleccione una pregunta.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var resultado = MessageBox.Show(
                "Desea eliminar la pregunta seleccionada?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (resultado != DialogResult.Yes)
                return;

            try
            {
                _preguntaBll.Eliminar(pregunta.IdPregunta);
                _bitacoraBLL.Registrar(
                    _usuarioActual,
                    "Preguntas",
                    "Eliminacion",
                    $"Elimino la pregunta '{pregunta.Texto}' (ID {pregunta.IdPregunta})."
                );

                CmbPrueba_SelectedIndexChanged(null, EventArgs.Empty);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
