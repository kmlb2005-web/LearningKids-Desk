using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class ControlResultados : UserControl
    {
        private ComboBox cmbAlumno = null!;
        private DataGridView dgvResultados = null!;
        private Button btnAgregar = null!;
        private Button btnEditar = null!;
        private Button btnDesactivar = null!;
        private Button btnExportar = null!;
        private TextBox txtBuscar = null!;

        private readonly ResultadoBLL _resultadoBll = new();
        private readonly UsuarioBLL _usuarioBll = new();
        private readonly PruebaBLL _pruebaBll = new();
        private readonly Usuario _usuarioActual;
        private List<Usuario> _alumnos = new();
        private List<Prueba> _pruebas = new();

        public ControlResultados(Usuario usuarioActual)
        {
            _usuarioActual = usuarioActual;

            InicializarComponentes();
            CargarAlumnos();
        }

        private void InicializarComponentes()
        {
            BackColor = Color.FromArgb(245, 250, 255);

            Label lblTitulo = new()
            {
                Text = "Gestion de Resultados",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 80),
                AutoSize = true,
                Location = new Point(25, 15)
            };

            Label lblSubtitulo = new()
            {
                Text = "Administra los resultados registrados",
                Font = new Font("Segoe UI", 13),
                ForeColor = Color.FromArgb(100, 120, 150),
                AutoSize = true,
                Location = new Point(30, 82)
            };

            Label lblAlumno = new()
            {
                Text = "Seleccionar alumno:",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 90),
                AutoSize = true,
                Location = new Point(25, 125)
            };

            cmbAlumno = new ComboBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(250, 118),
                Size = new Size(420, 40),
                DropDownStyle = ComboBoxStyle.DropDownList,
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.FromArgb(50, 70, 120)
            };
            cmbAlumno.SelectedIndexChanged += CmbAlumno_SelectedIndexChanged;

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
                PlaceholderText = "Buscar alumno o prueba..."
            };

            txtBuscar.TextChanged += TxtBuscar_TextChanged;


            dgvResultados = new DataGridView
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

            dgvResultados.EnableHeadersVisualStyles = false;
            dgvResultados.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(240, 247, 255);
            dgvResultados.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(50, 70, 120);
            dgvResultados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            dgvResultados.ColumnHeadersHeight = 55;
            dgvResultados.DefaultCellStyle.SelectionBackColor = Color.FromArgb(220, 235, 255);
            dgvResultados.DefaultCellStyle.SelectionForeColor = Color.FromArgb(20, 35, 80);
            dgvResultados.RowTemplate.Height = 50;

            Controls.Add(lblTitulo);
            Controls.Add(lblSubtitulo);
            Controls.Add(lblAlumno);
            Controls.Add(cmbAlumno);
            Controls.Add(btnAgregar);
            Controls.Add(btnEditar);
            Controls.Add(btnDesactivar);
            Controls.Add(txtBuscar);
            Controls.Add(btnExportar);
            Controls.Add(dgvResultados);
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

        private void CargarAlumnos()
        {
            try
            {
                _alumnos = _usuarioActual.IdRol == 1
                    ? _usuarioBll.ObtenerAlumnos()
                    : _usuarioBll.ObtenerAlumnosPorDocente(_usuarioActual.IdUsuario);

                _pruebas = _pruebaBll.ObtenerVisibles(_usuarioActual);

                cmbAlumno.Items.Clear();
                cmbAlumno.Items.Add("-- Todos los alumnos --");

                foreach (var alumno in _alumnos)
                    cmbAlumno.Items.Add(alumno.Nombre);

                cmbAlumno.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar alumnos:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CargarResultados()
        {
            try
            {
                List<Resultado> resultados = cmbAlumno.SelectedIndex > 0
                    ? _resultadoBll.ObtenerPorAlumno(_alumnos[cmbAlumno.SelectedIndex - 1].IdUsuario)
                    : _resultadoBll.ObtenerVisibles(_usuarioActual);

                dgvResultados.DataSource = null;
                dgvResultados.DataSource = resultados;

                ConfigurarColumnas();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar resultados:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbAlumno_SelectedIndexChanged(object? sender, EventArgs e)
        {
            BuscarResultados();
        }

        private void TxtBuscar_TextChanged(object? sender, EventArgs e)
        {
            BuscarResultados();
        }

        private void BtnBuscar_Click(object? sender, EventArgs e)
        {
            BuscarResultados();
        }

        private void BuscarResultados()
        {
            try
            {
                List<Resultado> resultados = cmbAlumno.SelectedIndex > 0
                    ? _resultadoBll.ObtenerPorAlumno(_alumnos[cmbAlumno.SelectedIndex - 1].IdUsuario)
                    : _resultadoBll.ObtenerVisibles(_usuarioActual);

                string texto = txtBuscar.Text.Trim().ToLower();

                if (!string.IsNullOrWhiteSpace(texto))
                {
                    resultados = resultados
                        .Where(r =>
                        {
                            string alumno = _alumnos
                                .FirstOrDefault(a => a.IdUsuario == r.IdAlumno)
                                ?.Nombre
                                .ToLower()
                                ?? string.Empty;

                            string prueba = _pruebas
                                .FirstOrDefault(p => p.IdPrueba == r.IdPrueba)
                                ?.Titulo
                                .ToLower()
                                ?? string.Empty;

                            return alumno.Contains(texto)
                                || prueba.Contains(texto);
                        })
                        .ToList();
                }

                dgvResultados.DataSource = null;
                dgvResultados.DataSource = resultados;
                ConfigurarColumnas();
            }
            catch
            {
            }
        }

        private void BtnExportar_Click(object? sender, EventArgs e)
        {
            ExportadorExcel.Exportar(dgvResultados, "resultados");
        }

        private void ConfigurarColumnas()
        {
            if (dgvResultados.Columns.Contains("IdResultado"))
                dgvResultados.Columns["IdResultado"].Visible = false;
            if (dgvResultados.Columns.Contains("IdAlumno"))
                dgvResultados.Columns["IdAlumno"].HeaderText = "Alumno";
            if (dgvResultados.Columns.Contains("IdPrueba"))
                dgvResultados.Columns["IdPrueba"].HeaderText = "Prueba";
            if (dgvResultados.Columns.Contains("Calificacion"))
                dgvResultados.Columns["Calificacion"].HeaderText = "Calificacion";
            if (dgvResultados.Columns.Contains("Fecha"))
                dgvResultados.Columns["Fecha"].HeaderText = "Fecha";
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            using var form = new FormResultadoDetalle(null, _usuarioActual);

            if (form.ShowDialog() == DialogResult.OK)
                CargarResultados();
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            if (dgvResultados.CurrentRow?.DataBoundItem is not Resultado resultado)
            {
                MessageBox.Show("Seleccione un resultado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var form = new FormResultadoDetalle(resultado, _usuarioActual);

            if (form.ShowDialog() == DialogResult.OK)
                CargarResultados();
        }

        private void BtnEliminar_Click(object? sender, EventArgs e)
        {
            if (dgvResultados.CurrentRow?.DataBoundItem is not Resultado resultado)
            {
                MessageBox.Show("Seleccione un resultado.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var confirmacion = MessageBox.Show(
                "Desea eliminar el resultado seleccionado?",
                "Confirmar",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (confirmacion != DialogResult.Yes)
                return;

            try
            {
                _resultadoBll.EliminarResultado(resultado.IdResultado);
                CargarResultados();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
