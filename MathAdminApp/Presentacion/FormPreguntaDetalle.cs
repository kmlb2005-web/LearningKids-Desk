// ============================================================
// Presentacion: FormPreguntaDetalle (MODERNO)
// SOLO CAMBIO VISUAL
// ============================================================

using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    public class FormPreguntaDetalle : Form
    {
        // =====================================================
        // CONTROLES
        // =====================================================

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

        // Mostrar/Ocultar controles
        private readonly List<Control> _controlesOpciones = new();

        // =====================================================
        // LOGICA (NO CAMBIADA)
        // =====================================================

        private readonly int _examenId;

        private readonly PreguntaBLL _bll = new();

        // =====================================================
        // CONSTRUCTOR
        // =====================================================

        public FormPreguntaDetalle(int examenId)
        {
            _examenId = examenId;

            InicializarComponentes();
        }

        // =====================================================
        // DISEÑO MODERNO
        // =====================================================

        private void InicializarComponentes()
        {
            // =================================================
            // FORMULARIO
            // =================================================

            this.Text = "Nueva Pregunta";

            this.Size = new Size(760, 760);

            this.StartPosition = FormStartPosition.CenterParent;

            this.FormBorderStyle = FormBorderStyle.FixedDialog;

            this.MaximizeBox = false;

            this.MinimizeBox = false;

            this.BackColor =
                Color.FromArgb(245, 248, 255);

            this.AutoScroll = true;

            // =================================================
            // PANEL CONTENEDOR
            // =================================================

            Panel panelContenido = new Panel
            {
                Dock = DockStyle.Fill,

                AutoScroll = true,

                Padding = new Padding(40),

                BackColor = Color.Transparent
            };

            // =================================================
            // IMAGEN SUPERIOR
            // =================================================

            PictureBox picRobot = new PictureBox
            {
                Image = Image.FromFile("Resources/Louz.png"),

                SizeMode = PictureBoxSizeMode.Zoom,

                Size = new Size(90, 90),

                Location = new Point(20, 10),

                BackColor = Color.Transparent
            };

            // =================================================
            // TITULO
            // =================================================

            Label lblTitulo = new Label
            {
                Text = "📝 Nueva Pregunta",

                Font = new Font(
                    "Segoe UI",
                    26,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(15, 35, 90),

                AutoSize = true,

                Location = new Point(130, 20)
            };

            // =================================================
            // SUBTITULO
            // =================================================

            Label lblSubtitulo = new Label
            {
                Text =
                    "Crea preguntas para el examen fácilmente ✨",

                Font = new Font("Segoe UI", 12),

                ForeColor =
                    Color.FromArgb(110, 120, 150),

                AutoSize = true,

                Location = new Point(135, 70)
            };

            // =================================================
            // POSICIONES
            // =================================================

            int y = 140;

            int x = 20;

            int w = 620;

            // =================================================
            // TIPO
            // =================================================

            lblTipo = CrearLabel(
                "🧩 Tipo de pregunta",
                x,
                y
            );

            y += 35;

            cmbTipo = new ComboBox
            {
                Font = new Font("Segoe UI", 12),

                Location = new Point(x, y),

                Size = new Size(300, 45),

                DropDownStyle =
                    ComboBoxStyle.DropDownList,

                BackColor = Color.White,

                FlatStyle = FlatStyle.Flat
            };

            cmbTipo.Items.AddRange(
                new[] { "Multiple", "Abierta" });

            cmbTipo.SelectedIndex = 0;

            cmbTipo.SelectedIndexChanged +=
                CmbTipo_SelectedIndexChanged;

            // =================================================
            // TEXTO PREGUNTA
            // =================================================

            y += 70;

            lblTexto = CrearLabel(
                "📝 Texto de la pregunta",
                x,
                y
            );

            y += 35;

            txtTexto = new TextBox
            {
                Font = new Font("Segoe UI", 12),

                Location = new Point(x, y),

                Size = new Size(w, 90),

                Multiline = true,

                BorderStyle = BorderStyle.FixedSingle,

                ScrollBars = ScrollBars.Vertical
            };

            // =================================================
            // OPCION A
            // =================================================

            y += 110;

            lblOpcionA = CrearLabel(
                "🔤 Opción A",
                x,
                y
            );

            y += 35;

            txtOpcionA = CrearTextBox(x, y, w);

            // =================================================
            // OPCION B
            // =================================================

            y += 65;

            lblOpcionB = CrearLabel(
                "🔤 Opción B",
                x,
                y
            );

            y += 35;

            txtOpcionB = CrearTextBox(x, y, w);

            // =================================================
            // OPCION C
            // =================================================

            y += 65;

            lblOpcionC = CrearLabel(
                "🔤 Opción C",
                x,
                y
            );

            y += 35;

            txtOpcionC = CrearTextBox(x, y, w);

            // =================================================
            // OPCION D
            // =================================================

            y += 65;

            lblOpcionD = CrearLabel(
                "🔤 Opción D",
                x,
                y
            );

            y += 35;

            txtOpcionD = CrearTextBox(x, y, w);

            // =================================================
            // CONTROLES MULTIPLE
            // =================================================

            _controlesOpciones.AddRange(
                new Control[]
                {
                    lblOpcionA, txtOpcionA,
                    lblOpcionB, txtOpcionB,
                    lblOpcionC, txtOpcionC,
                    lblOpcionD, txtOpcionD
                });

            // =================================================
            // RESPUESTA
            // =================================================

            y += 75;

            lblRespuesta = CrearLabel(
                "✅ Respuesta correcta",
                x,
                y
            );

            y += 35;

            txtRespuesta = CrearTextBox(x, y, w);

            // =================================================
            // BOTONES
            // =================================================

            y += 90;

            btnGuardar = new Button
            {
                Text = "💾 Guardar",

                Font = new Font(
                    "Segoe UI",
                    12,
                    FontStyle.Bold
                ),

                BackColor =
                    Color.FromArgb(50, 120, 255),

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Size = new Size(260, 55),

                Location = new Point(40, y),

                Cursor = Cursors.Hand
            };

            btnGuardar.FlatAppearance.BorderSize = 0;

            btnGuardar.Click += BtnGuardar_Click;

            // Hover
            btnGuardar.MouseEnter += (s, e) =>
            {
                btnGuardar.BackColor =
                    Color.FromArgb(70, 140, 255);
            };

            btnGuardar.MouseLeave += (s, e) =>
            {
                btnGuardar.BackColor =
                    Color.FromArgb(50, 120, 255);
            };

            // =================================================
            // CANCELAR
            // =================================================

            btnCancelar = new Button
            {
                Text = "❌ Cancelar",

                Font = new Font(
                    "Segoe UI",
                    12,
                    FontStyle.Bold
                ),

                BackColor =
                    Color.FromArgb(255, 80, 120),

                ForeColor = Color.White,

                FlatStyle = FlatStyle.Flat,

                Size = new Size(260, 55),

                Location = new Point(330, y),

                Cursor = Cursors.Hand
            };

            btnCancelar.FlatAppearance.BorderSize = 0;

            btnCancelar.Click += (s, e) =>
            {
                this.DialogResult = DialogResult.Cancel;

                this.Close();
            };

            // Hover
            btnCancelar.MouseEnter += (s, e) =>
            {
                btnCancelar.BackColor =
                    Color.FromArgb(255, 110, 145);
            };

            btnCancelar.MouseLeave += (s, e) =>
            {
                btnCancelar.BackColor =
                    Color.FromArgb(255, 80, 120);
            };

            // =================================================
            // AGREGAR CONTROLES
            // =================================================

            panelContenido.Controls.Add(picRobot);

            panelContenido.Controls.Add(lblTitulo);

            panelContenido.Controls.Add(lblSubtitulo);

            panelContenido.Controls.Add(lblTipo);

            panelContenido.Controls.Add(cmbTipo);

            panelContenido.Controls.Add(lblTexto);

            panelContenido.Controls.Add(txtTexto);

            panelContenido.Controls.Add(lblOpcionA);

            panelContenido.Controls.Add(txtOpcionA);

            panelContenido.Controls.Add(lblOpcionB);

            panelContenido.Controls.Add(txtOpcionB);

            panelContenido.Controls.Add(lblOpcionC);

            panelContenido.Controls.Add(txtOpcionC);

            panelContenido.Controls.Add(lblOpcionD);

            panelContenido.Controls.Add(txtOpcionD);

            panelContenido.Controls.Add(lblRespuesta);

            panelContenido.Controls.Add(txtRespuesta);

            panelContenido.Controls.Add(btnGuardar);

            panelContenido.Controls.Add(btnCancelar);

            this.Controls.Add(panelContenido);
        }

        // =====================================================
        // LABEL MODERNO
        // =====================================================

        private Label CrearLabel(
            string texto,
            int x,
            int y
        )
        {
            return new Label
            {
                Text = texto,

                Font = new Font(
                    "Segoe UI",
                    12,
                    FontStyle.Bold
                ),

                ForeColor =
                    Color.FromArgb(20, 35, 90),

                AutoSize = true,

                Location = new Point(x, y)
            };
        }

        // =====================================================
        // TEXTBOX MODERNO
        // =====================================================

        private TextBox CrearTextBox(
            int x,
            int y,
            int width
        )
        {
            return new TextBox
            {
                Font = new Font("Segoe UI", 12),

                Location = new Point(x, y),

                Size = new Size(width, 42),

                BorderStyle = BorderStyle.FixedSingle,

                BackColor = Color.White,

                ForeColor =
                    Color.FromArgb(50, 70, 120)
            };
        }

        // =====================================================
        // MOSTRAR/OCULTAR OPCIONES
        // =====================================================

        private void CmbTipo_SelectedIndexChanged(
            object? sender,
            EventArgs e
        )
        {
            bool esMultiple =
                cmbTipo.SelectedItem?.ToString()
                == "Multiple";

            foreach (var ctrl in _controlesOpciones)
                ctrl.Visible = esMultiple;

            lblRespuesta.Text = esMultiple
                ? "✅ Respuesta correcta (A, B, C o D)"
                : "✅ Respuesta correcta";
        }

        // =====================================================
        // GUARDAR (LOGICA ORIGINAL)
        // =====================================================

        private void BtnGuardar_Click(
            object? sender,
            EventArgs e
        )
        {
            try
            {
                var pregunta = new Pregunta
                {
                    ExamenId = _examenId,

                    Texto = txtTexto.Text.Trim(),

                    Tipo =
                        cmbTipo.SelectedItem?.ToString()
                        ?? "Multiple",

                    OpcionA =
                        txtOpcionA.Text.Trim(),

                    OpcionB =
                        txtOpcionB.Text.Trim(),

                    OpcionC =
                        txtOpcionC.Text.Trim(),

                    OpcionD =
                        txtOpcionD.Text.Trim(),

                    RespuestaCorrecta =
                        txtRespuesta.Text.Trim()
                };

                _bll.Agregar(pregunta);

                MessageBox.Show(
                    "Pregunta agregada.",
                    "Éxito",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                this.DialogResult = DialogResult.OK;

                this.Close();
            }
            catch (ArgumentException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Validación",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Error: {ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }
    }
}