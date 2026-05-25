using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class FormPreguntaDetalle : Form
    {
        private TextBox txtTexto = null!;
        private DataGridView dgvRespuestas = null!;
        private Button btnAgregarRespuesta = null!;
        private Button btnQuitarRespuesta = null!;
        private Button btnGuardar = null!;
        private Button btnCancelar = null!;

        private readonly int _idPrueba;
        private readonly PreguntaBLL _preguntaBll = new();
        private readonly RespuestaBLL _respuestaBll = new();
        private readonly Pregunta? _pregunta;
        private readonly bool _esEdicion;

        public FormPreguntaDetalle(int idPrueba, Pregunta? pregunta = null)
        {
            _idPrueba = idPrueba;
            _pregunta = pregunta;
            _esEdicion = pregunta != null;

            InicializarComponentes();

            if (_esEdicion)
                CargarDatos();
            else
                AgregarRespuestasIniciales();
        }

        private void InicializarComponentes()
        {
            Text = _esEdicion ? "Editar Pregunta" : "Nueva Pregunta";
            Size = new Size(900, 660);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;
            MinimizeBox = false;
            BackColor = Color.FromArgb(245, 248, 255);

            Label lblTitulo = new()
            {
                Text = Text,
                Font = new Font("Segoe UI", 26, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 35, 90),
                AutoSize = true,
                Location = new Point(40, 30)
            };

            Label lblTexto = CrearLabel("Texto de la pregunta", 40, 110);

            txtTexto = new TextBox
            {
                Font = new Font("Segoe UI", 12),
                Location = new Point(40, 140),
                Size = new Size(760, 95),
                Multiline = true,
                BorderStyle = BorderStyle.FixedSingle,
                ScrollBars = ScrollBars.Vertical
            };

            Label lblRespuestas = CrearLabel("Respuestas", 40, 260);

            dgvRespuestas = new DataGridView
            {
                Location = new Point(40, 290),
                Size = new Size(800, 230),
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                RowHeadersVisible = false,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                MultiSelect = false,
                Font = new Font("Segoe UI", 11)
            };

            dgvRespuestas.Columns.Add(new DataGridViewTextBoxColumn
            {
                Name = "Texto",
                HeaderText = "Respuesta",
                FillWeight = 78
            });

            dgvRespuestas.Columns.Add(new DataGridViewCheckBoxColumn
            {
                Name = "EsCorrecta",
                HeaderText = "Correcta",
                FillWeight = 22
            });

            btnAgregarRespuesta = CrearBoton("Agregar respuesta", Color.FromArgb(66, 133, 244), 40, 545, 210);
            btnAgregarRespuesta.Click += (s, e) => dgvRespuestas.Rows.Add(string.Empty, false);

            btnQuitarRespuesta = CrearBoton("Quitar respuesta", Color.FromArgb(255, 120, 70), 265, 545, 210);
            btnQuitarRespuesta.Click += BtnQuitarRespuesta_Click;

            btnGuardar = CrearBoton("Guardar", Color.FromArgb(50, 120, 255), 510, 545, 150);
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = CrearBoton("Cancelar", Color.FromArgb(255, 80, 120), 690, 545, 150);
            btnCancelar.Click += (s, e) =>
            {
                DialogResult = DialogResult.Cancel;
                Close();
            };

            Controls.Add(lblTitulo);
            Controls.Add(lblTexto);
            Controls.Add(txtTexto);
            Controls.Add(lblRespuestas);
            Controls.Add(dgvRespuestas);
            Controls.Add(btnAgregarRespuesta);
            Controls.Add(btnQuitarRespuesta);
            Controls.Add(btnGuardar);
            Controls.Add(btnCancelar);
        }

        private static Label CrearLabel(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                ForeColor = Color.FromArgb(20, 35, 90),
                AutoSize = true,
                Location = new Point(x, y)
            };
        }

        private static Button CrearBoton(string texto, Color color, int x, int y, int width)
        {
            Button btn = new()
            {
                Text = texto,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = color,
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(width, 45),
                Location = new Point(x, y),
                Cursor = Cursors.Hand
            };

            btn.FlatAppearance.BorderSize = 0;
            return btn;
        }

        private void AgregarRespuestasIniciales()
        {
            dgvRespuestas.Rows.Add(string.Empty, false);
            dgvRespuestas.Rows.Add(string.Empty, false);
        }

        private void CargarDatos()
        {
            if (_pregunta == null)
                return;

            txtTexto.Text = _pregunta.Texto;

            var respuestas = _respuestaBll.ObtenerPorPregunta(_pregunta.IdPregunta);
            dgvRespuestas.Rows.Clear();

            foreach (var respuesta in respuestas)
                dgvRespuestas.Rows.Add(respuesta.Texto, respuesta.EsCorrecta);

            if (dgvRespuestas.Rows.Count == 0)
                AgregarRespuestasIniciales();
        }

        private void BtnQuitarRespuesta_Click(object? sender, EventArgs e)
        {
            if (dgvRespuestas.CurrentRow == null)
                return;

            dgvRespuestas.Rows.Remove(dgvRespuestas.CurrentRow);
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                Pregunta pregunta = _pregunta ?? new Pregunta();
                pregunta.Texto = txtTexto.Text.Trim();
                pregunta.IdPrueba = _idPrueba;

                if (_esEdicion)
                    _preguntaBll.Editar(pregunta);
                else
                    _preguntaBll.Agregar(pregunta);

                _respuestaBll.ReemplazarPorPregunta(
                    pregunta.IdPregunta,
                    ObtenerRespuestasDesdeGrid(pregunta.IdPregunta));

                DialogResult = DialogResult.OK;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private List<Respuesta> ObtenerRespuestasDesdeGrid(int idPregunta)
        {
            List<Respuesta> respuestas = new();

            foreach (DataGridViewRow row in dgvRespuestas.Rows)
            {
                string texto = row.Cells["Texto"].Value?.ToString()?.Trim() ?? string.Empty;

                if (string.IsNullOrWhiteSpace(texto))
                    continue;

                bool esCorrecta = row.Cells["EsCorrecta"].Value is bool valor && valor;

                respuestas.Add(new Respuesta
                {
                    Texto = texto,
                    EsCorrecta = esCorrecta,
                    IdPregunta = idPregunta
                });
            }

            return respuestas;
        }
    }
}
