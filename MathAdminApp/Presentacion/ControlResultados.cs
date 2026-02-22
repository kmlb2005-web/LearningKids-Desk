// ============================================================
// Presentacion: ControlResultados (UserControl)
// Vista de solo lectura para consultar resultados de alumnos
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    /// <summary>
    /// Control de usuario para visualizar resultados de examenes.
    /// Solo lectura - el administrador no modifica resultados.
    /// </summary>
    public class ControlResultados : UserControl
    {
        private ComboBox cmbAlumno = null!;
        private Label lblSeleccionar = null!;
        private DataGridView dgvResultados = null!;
        private Panel panelSuperior = null!;
        private Label lblResumen = null!;

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
            this.BackColor = Color.FromArgb(240, 242, 245);

            // --- Panel superior: selector de alumno ---
            panelSuperior = new Panel
            {
                Dock = DockStyle.Top,
                Height = 80,
                BackColor = Color.Transparent
            };

            lblSeleccionar = new Label
            {
                Text = "Seleccionar alumno:",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(0, 15)
            };

            cmbAlumno = new ComboBox
            {
                Font = new Font("Segoe UI", 10),
                Location = new Point(155, 10),
                Size = new Size(350, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbAlumno.SelectedIndexChanged += CmbAlumno_SelectedIndexChanged;

            lblResumen = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.FromArgb(63, 81, 181),
                AutoSize = true,
                Location = new Point(0, 50)
            };

            panelSuperior.Controls.Add(lblSeleccionar);
            panelSuperior.Controls.Add(cmbAlumno);
            panelSuperior.Controls.Add(lblResumen);

            // --- Tabla de resultados ---
            dgvResultados = new DataGridView
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
                GridColor = Color.FromArgb(230, 230, 230)
            };

            dgvResultados.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(63, 81, 181);
            dgvResultados.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvResultados.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvResultados.ColumnHeadersHeight = 40;
            dgvResultados.EnableHeadersVisualStyles = false;
            dgvResultados.DefaultCellStyle.SelectionBackColor = Color.FromArgb(200, 210, 240);
            dgvResultados.DefaultCellStyle.SelectionForeColor = Color.Black;
            dgvResultados.RowTemplate.Height = 35;

            this.Controls.Add(dgvResultados);
            this.Controls.Add(panelSuperior);
        }

        private void CargarAlumnos()
        {
            try
            {
                _alumnos = _usuarioBll.ObtenerAlumnos();
                cmbAlumno.Items.Clear();
                cmbAlumno.Items.Add("-- Todos los alumnos --");
                foreach (var a in _alumnos)
                    cmbAlumno.Items.Add($"{a.Nombre} ({a.Grado})");
                cmbAlumno.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar alumnos:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void CmbAlumno_SelectedIndexChanged(object? sender, EventArgs e)
        {
            try
            {
                List<ResultadoExamen> resultados;

                if (cmbAlumno.SelectedIndex <= 0)
                {
                    resultados = _resultadoBll.ObtenerTodos();
                    lblResumen.Text = "";
                }
                else
                {
                    var alumno = _alumnos[cmbAlumno.SelectedIndex - 1];
                    resultados = _resultadoBll.ObtenerPorAlumno(alumno.Id);

                    // Calcular promedio
                    if (resultados.Count > 0)
                    {
                        var promedio = resultados.Average(r => (double)r.Calificacion);
                        lblResumen.Text = $"Examenes presentados: {resultados.Count}  |  " +
                                         $"Promedio general: {promedio:F1}";
                    }
                    else
                    {
                        lblResumen.Text = "Este alumno no tiene resultados registrados.";
                    }
                }

                dgvResultados.DataSource = null;
                dgvResultados.DataSource = resultados;

                // Configurar columnas visibles
                if (dgvResultados.Columns.Contains("Id"))
                    dgvResultados.Columns["Id"].Visible = false;
                if (dgvResultados.Columns.Contains("UsuarioId"))
                    dgvResultados.Columns["UsuarioId"].Visible = false;
                if (dgvResultados.Columns.Contains("ExamenId"))
                    dgvResultados.Columns["ExamenId"].Visible = false;
                if (dgvResultados.Columns.Contains("NombreAlumno"))
                    dgvResultados.Columns["NombreAlumno"].HeaderText = "Alumno";
                if (dgvResultados.Columns.Contains("NombreExamen"))
                    dgvResultados.Columns["NombreExamen"].HeaderText = "Examen";
                if (dgvResultados.Columns.Contains("NombreUnidad"))
                    dgvResultados.Columns["NombreUnidad"].HeaderText = "Unidad";
                if (dgvResultados.Columns.Contains("Calificacion"))
                    dgvResultados.Columns["Calificacion"].HeaderText = "Calificacion";
                if (dgvResultados.Columns.Contains("FechaPresentacion"))
                    dgvResultados.Columns["FechaPresentacion"].HeaderText = "Fecha";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar resultados:\n{ex.Message}",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
