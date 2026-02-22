// ============================================================
// Presentacion: FormPreguntaDetalle
// Formulario para agregar una pregunta (multiple o abierta)
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    /// <summary>
    /// Formulario modal para agregar una pregunta a un examen.
    /// Soporta preguntas de opcion multiple y abiertas.
    /// </summary>
    public class FormPreguntaDetalle : Form
    {
        private Label lblTexto = null!;
        private Label lblTipo = null!;
        private Label lblOpcionA = null!;
        private Label lblOpcionB = null!;
        private Label lblOpcionC = null!;
        private Label lblOpcionD = null!;
        private Label lblRespuesta = null!;
        private TextBox txtTexto = null!;
        private ComboBox cmbTipo = null!;
        private TextBox txtOpcionA = null!;
        private TextBox txtOpcionB = null!;
        private TextBox txtOpcionC = null!;
        private TextBox txtOpcionD = null!;
        private TextBox txtRespuesta = null!;
        private Button btnGuardar = null!;
        private Button btnCancelar = null!;

        // Controles de opciones para mostrar/ocultar
        private readonly List<Control> _controlesOpciones = new();

        private readonly int _examenId;
        private readonly PreguntaBLL _bll = new();

        public FormPreguntaDetalle(int examenId)
        {
            _examenId = examenId;
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            this.Text = "Agregar Pregunta";
            this.Size = new Size(480, 530);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.BackColor = Color.White;

            int y = 15;
            int x = 20;
            int w = 420;

            // Tipo de pregunta
            lblTipo = CrearLabel("Tipo de pregunta:", x, y);
            y += 22;
            cmbTipo = new ComboBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(x, y),
                Size = new Size(200, 28),
                DropDownStyle = ComboBoxStyle.DropDownList
            };
            cmbTipo.Items.AddRange(new[] { "Multiple", "Abierta" });
            cmbTipo.SelectedIndex = 0;
            cmbTipo.SelectedIndexChanged += CmbTipo_SelectedIndexChanged;

            // Texto de la pregunta
            y += 40;
            lblTexto = CrearLabel("Texto de la pregunta:", x, y);
            y += 22;
            txtTexto = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(x, y),
                Size = new Size(w, 55),
                BorderStyle = BorderStyle.FixedSingle,
                Multiline = true,
                ScrollBars = ScrollBars.Vertical
            };

            // Opciones (solo para opcion multiple)
            y += 65;
            lblOpcionA = CrearLabel("Opcion A:", x, y);
            y += 22;
            txtOpcionA = CrearTextBox(x, y, w);

            y += 32;
            lblOpcionB = CrearLabel("Opcion B:", x, y);
            y += 22;
            txtOpcionB = CrearTextBox(x, y, w);

            y += 32;
            lblOpcionC = CrearLabel("Opcion C:", x, y);
            y += 22;
            txtOpcionC = CrearTextBox(x, y, w);

            y += 32;
            lblOpcionD = CrearLabel("Opcion D:", x, y);
            y += 22;
            txtOpcionD = CrearTextBox(x, y, w);

            // Guardar referencia de controles de opciones
            _controlesOpciones.AddRange(new Control[]
            {
                lblOpcionA, txtOpcionA, lblOpcionB, txtOpcionB,
                lblOpcionC, txtOpcionC, lblOpcionD, txtOpcionD
            });

            // Respuesta correcta
            y += 35;
            lblRespuesta = CrearLabel("Respuesta correcta (A, B, C o D):", x, y);
            y += 22;
            txtRespuesta = CrearTextBox(x, y, w);

            // Botones
            y += 40;
            btnGuardar = new Button
            {
                Text = "Guardar",
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                BackColor = Color.FromArgb(63, 81, 181),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Size = new Size(200, 40),
                Location = new Point(x, y),
                Cursor = Cursors.Hand
            };
            btnGuardar.FlatAppearance.BorderSize = 0;
            btnGuardar.Click += BtnGuardar_Click;

            btnCancelar = new Button
            {
                Text = "Cancelar",
                Font = new Font("Segoe UI", 11),
                BackColor = Color.FromArgb(200, 200, 200),
                ForeColor = Color.FromArgb(60, 60, 60),
                FlatStyle = FlatStyle.Flat,
                Size = new Size(200, 40),
                Location = new Point(240, y),
                Cursor = Cursors.Hand
            };
            btnCancelar.FlatAppearance.BorderSize = 0;
            btnCancelar.Click += (s, e) => { this.DialogResult = DialogResult.Cancel; this.Close(); };

            this.Controls.Add(lblTipo);
            this.Controls.Add(cmbTipo);
            this.Controls.Add(lblTexto);
            this.Controls.Add(txtTexto);
            this.Controls.Add(lblOpcionA);
            this.Controls.Add(txtOpcionA);
            this.Controls.Add(lblOpcionB);
            this.Controls.Add(txtOpcionB);
            this.Controls.Add(lblOpcionC);
            this.Controls.Add(txtOpcionC);
            this.Controls.Add(lblOpcionD);
            this.Controls.Add(txtOpcionD);
            this.Controls.Add(lblRespuesta);
            this.Controls.Add(txtRespuesta);
            this.Controls.Add(btnGuardar);
            this.Controls.Add(btnCancelar);
        }

        private Label CrearLabel(string texto, int x, int y)
        {
            return new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(80, 80, 80),
                AutoSize = true,
                Location = new Point(x, y)
            };
        }

        private TextBox CrearTextBox(int x, int y, int width)
        {
            return new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Location = new Point(x, y),
                Size = new Size(width, 28),
                BorderStyle = BorderStyle.FixedSingle
            };
        }

        /// <summary>
        /// Muestra u oculta las opciones segun el tipo de pregunta.
        /// </summary>
        private void CmbTipo_SelectedIndexChanged(object? sender, EventArgs e)
        {
            bool esMultiple = cmbTipo.SelectedItem?.ToString() == "Multiple";
            foreach (var ctrl in _controlesOpciones)
                ctrl.Visible = esMultiple;

            lblRespuesta.Text = esMultiple
                ? "Respuesta correcta (A, B, C o D):"
                : "Respuesta correcta:";
        }

        private void BtnGuardar_Click(object? sender, EventArgs e)
        {
            try
            {
                var pregunta = new Pregunta
                {
                    ExamenId = _examenId,
                    Texto = txtTexto.Text.Trim(),
                    Tipo = cmbTipo.SelectedItem?.ToString() ?? "Multiple",
                    OpcionA = txtOpcionA.Text.Trim(),
                    OpcionB = txtOpcionB.Text.Trim(),
                    OpcionC = txtOpcionC.Text.Trim(),
                    OpcionD = txtOpcionD.Text.Trim(),
                    RespuestaCorrecta = txtRespuesta.Text.Trim()
                };

                _bll.Agregar(pregunta);
                MessageBox.Show("Pregunta agregada.", "Exito",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(ex.Message, "Validacion",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
