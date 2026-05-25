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
        private Button btnEliminar = null!;

        private readonly ResultadoBLL _resultadoBll = new();
        private readonly UsuarioBLL _usuarioBll = new();
        private List<Usuario> _alumnos = new();

        public ControlResultados()
        {
            InicializarComponentes();
            CargarAlumnos();
        }

        private void InicializarComponentes()
        {
            BackColor = Color.FromArgb(245, 250, 255);

            Label lblTitulo = new()
            {
                Text = "Gestion de Resultados",
                Font = new Font("Segoe UI", 22, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 80),
                AutoSize = true,
                Location = new Point(20, 20)
            };

            Label lblSubtitulo = new()
            {
                Text = "Consulta y administra resultados",
                Font = new Font("Segoe UI", 12),
                ForeColor = Color.FromArgb(100, 120, 150),
                AutoSize = true,
                Location = new Point(25, 65)
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

            btnAgregar = CrearBoton("Agregar", Color.FromArgb(66, 133, 244));
            btnAgregar.Location = new Point(20, 190);
            btnAgregar.Click += BtnAgregar_Click;

            btnEditar = CrearBoton("Editar", Color.FromArgb(52, 199, 89));
            btnEditar.Location = new Point(210, 190);
            btnEditar.Click += BtnEditar_Click;

            btnEliminar = CrearBoton("Eliminar", Color.FromArgb(255, 80, 120));
            btnEliminar.Location = new Point(400, 190);
            btnEliminar.Click += BtnEliminar_Click;

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
            Controls.Add(btnEliminar);
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
                _alumnos = _usuarioBll.ObtenerAlumnos();
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
                    : _resultadoBll.ObtenerResultados();

                dgvResultados.DataSource = null;
                dgvResultados.DataSource = resultados;

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
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar resultados:\n{ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbAlumno_SelectedIndexChanged(object? sender, EventArgs e)
        {
            CargarResultados();
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            using var form = new FormResultadoDetalle();

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

            using var form = new FormResultadoDetalle(resultado);

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
