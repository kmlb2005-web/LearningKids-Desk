// ============================================================
// Presentacion: ControlUsuarios (UserControl)
// Vista para gestionar alumnos (CRUD)
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    /// <summary>
    /// Control de usuario para la gestion de alumnos.
    /// Se incrusta en el panel de contenido del Dashboard.
    /// </summary>
    public class ControlUsuarios : UserControl
    {
        private DataGridView dgvUsuarios = null!;
        private Button btnAgregar = null!;
        private Button btnEditar = null!;
        private Button btnDesactivar = null!;
        private Panel panelBotones = null!;
        private readonly UsuarioBLL _bll = new();

        public ControlUsuarios()
        {
            InicializarComponentes();
            CargarDatos();
        }

        private void InicializarComponentes()
        {
            this.BackColor = Color.FromArgb(240, 242, 245);

            // --- Barra de botones ---
            panelBotones = new Panel
            {
                Dock = DockStyle.Top,
                Height = 50,
                BackColor = Color.Transparent,
                Padding = new Padding(0, 5, 0, 5)
            };

            btnAgregar = CrearBoton("Agregar Alumno", Color.FromArgb(63, 81, 181));
            btnAgregar.Location = new Point(0, 8);
            btnAgregar.Click += BtnAgregar_Click;

            btnEditar = CrearBoton("Editar", Color.FromArgb(0, 150, 136));
            btnEditar.Location = new Point(170, 8);
            btnEditar.Click += BtnEditar_Click;

            btnDesactivar = CrearBoton("Desactivar", Color.FromArgb(211, 47, 47));
            btnDesactivar.Location = new Point(310, 8);
            btnDesactivar.Click += BtnDesactivar_Click;

            panelBotones.Controls.Add(btnAgregar);
            panelBotones.Controls.Add(btnEditar);
            panelBotones.Controls.Add(btnDesactivar);

            // --- Tabla de usuarios ---
            dgvUsuarios = new DataGridView
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
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                RowHeadersVisible = false,
                Font = new Font("Segoe UI", 10),
                GridColor = Color.FromArgb(255, 179, 0)
            };
            // Desactivar estilo visual del sistema
            dgvUsuarios.EnableHeadersVisualStyles = false;

            // Encabezado fondo amarillo fuerte
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 179, 0); // Amarillo fuerte

            // Texto del encabezado en negro
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;

            // Color cuando se selecciona el encabezado
            dgvUsuarios.ColumnHeadersDefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 179, 0);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.SelectionForeColor = Color.Black;

            // Estilo del encabezado
            dgvUsuarios.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(255, 241, 118);
            dgvUsuarios.ColumnHeadersDefaultCellStyle.ForeColor = Color.Black;
            dgvUsuarios.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvUsuarios.ColumnHeadersHeight = 40;
            dgvUsuarios.EnableHeadersVisualStyles = false;

            // Estilo de seleccion
            dgvUsuarios.DefaultCellStyle.SelectionBackColor = Color.FromArgb(255, 202, 40);
            dgvUsuarios.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvUsuarios.RowTemplate.Height = 35;

            this.Controls.Add(dgvUsuarios);
            this.Controls.Add(panelBotones);
        }

        private Button CrearBoton(string texto, Color color)
        {
            var btn = new Button
            {
                Text = texto,
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 179, 0),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(130, 35),
                Cursor = Cursors.Hand
            };
            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        /// <summary>
        /// Carga los datos de alumnos en la tabla.
        /// </summary>
        private void CargarDatos()
        {
            try
            {
                var alumnos = _bll.ObtenerAlumnos();
                dgvUsuarios.DataSource = null;
                dgvUsuarios.DataSource = alumnos;

                // Ocultar columnas innecesarias
                if (dgvUsuarios.Columns.Contains("Contrasena"))
                    dgvUsuarios.Columns["Contrasena"].Visible = false;
                if (dgvUsuarios.Columns.Contains("Rol"))
                    dgvUsuarios.Columns["Rol"].Visible = false;

                // Renombrar columnas visibles
                if (dgvUsuarios.Columns.Contains("Id"))
                    dgvUsuarios.Columns["Id"].HeaderText = "ID";
                if (dgvUsuarios.Columns.Contains("NombreUsuario"))
                    dgvUsuarios.Columns["NombreUsuario"].HeaderText = "Usuario";
                if (dgvUsuarios.Columns.Contains("FechaCreacion"))
                    dgvUsuarios.Columns["FechaCreacion"].HeaderText = "Fecha Registro";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar alumnos:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnAgregar_Click(object? sender, EventArgs e)
        {
            var form = new FormUsuarioDetalle();
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarDatos();
            }
        }

        private void BtnEditar_Click(object? sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un alumno para editar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var usuario = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;
            var form = new FormUsuarioDetalle(usuario);
            if (form.ShowDialog() == DialogResult.OK)
            {
                CargarDatos();
            }
        }

        private void BtnDesactivar_Click(object? sender, EventArgs e)
        {
            if (dgvUsuarios.CurrentRow == null)
            {
                MessageBox.Show("Seleccione un alumno para desactivar.", "Aviso",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var usuario = (Usuario)dgvUsuarios.CurrentRow.DataBoundItem;
            var resultado = MessageBox.Show($"Desea desactivar al alumno '{usuario.Nombre}'?",
                "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resultado == DialogResult.Yes)
            {
                try
                {
                    _bll.DesactivarAlumno(usuario.Id);
                    CargarDatos();
                    MessageBox.Show("Alumno desactivado correctamente.", "Exito",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error: {ex.Message}", "Error",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
