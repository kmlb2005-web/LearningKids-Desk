// ============================================================
// Presentacion: ControlTutorIA
// Panel de chat con el tutor de matemáticas (llama al API)
// ============================================================

using MathAdminApp.LogicaNegocio;

namespace MathAdminApp.Presentacion
{
    public class ControlTutorIA : UserControl
    {
        private Panel panelChat = null!;
        private FlowLayoutPanel panelMensajes = null!;
        private TextBox txtPregunta = null!;
        private Button btnEnviar = null!;
        private Button btnEjercicio = null!;
        private Label lblEstado = null!;

        private readonly MathTutorApiService _service = new();

        public ControlTutorIA()
        {
            InicializarComponentes();
            AgregarMensaje("Tutor IA", "¡Hola! Soy tu tutor de matemáticas. Puedes hacerme cualquier pregunta o pedir un ejercicio. 😊", esBot: true);
        }

        private void InicializarComponentes()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(240, 242, 245);

            // --- Título ---
            var lblTitulo = new Label
            {
                Text = "🤖 Tutor IA de Matemáticas",
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                ForeColor = Color.FromArgb(50, 50, 50),
                AutoSize = true,
                Location = new Point(0, 0),
                Margin = new Padding(0, 0, 0, 10)
            };

            // --- Panel de mensajes (scroll) ---
            panelMensajes = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.TopDown,
                WrapContents = false,
                AutoScroll = true,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Location = new Point(0, 40),
                Size = new Size(800, 440)
            };

            // --- Label estado (cargando...) ---
            lblEstado = new Label
            {
                Text = "",
                Font = new Font("Segoe UI", 9, FontStyle.Italic),
                ForeColor = Color.Gray,
                AutoSize = true,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left,
                Location = new Point(0, 495)
            };

            // --- Input de texto ---
            txtPregunta = new TextBox
            {
                Font = new Font("Segoe UI", 11),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                Location = new Point(0, 515),
                Size = new Size(630, 30),
                PlaceholderText = "Escribe tu pregunta aquí...",
                BorderStyle = BorderStyle.FixedSingle
            };
            txtPregunta.KeyDown += (s, e) =>
            {
                if (e.KeyCode == Keys.Enter && !e.Shift)
                {
                    e.SuppressKeyPress = true;
                    _ = EnviarPreguntaAsync();
                }
            };

            // --- Botón Enviar ---
            btnEnviar = new Button
            {
                Text = "Enviar",
                Font = new Font("Segoe UI", 10, FontStyle.Bold),
                BackColor = Color.FromArgb(255, 235, 59),
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(640, 513),
                Size = new Size(90, 34)
            };
            btnEnviar.FlatAppearance.BorderSize = 0;
            btnEnviar.Click += async (s, e) => await EnviarPreguntaAsync();

            // --- Botón Ejercicio ---
            btnEjercicio = new Button
            {
                Text = "Nuevo Ejercicio",
                Font = new Font("Segoe UI", 9),
                BackColor = Color.FromArgb(100, 181, 246),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Cursor = Cursors.Hand,
                Anchor = AnchorStyles.Bottom | AnchorStyles.Right,
                Location = new Point(740, 513),
                Size = new Size(120, 34)
            };
            btnEjercicio.FlatAppearance.BorderSize = 0;
            btnEjercicio.Click += async (s, e) => await PedirEjercicioAsync();

            this.Controls.Add(lblTitulo);
            this.Controls.Add(panelMensajes);
            this.Controls.Add(lblEstado);
            this.Controls.Add(txtPregunta);
            this.Controls.Add(btnEnviar);
            this.Controls.Add(btnEjercicio);

            this.Resize += (s, e) => AjustarLayout();
        }

        private void AjustarLayout()
        {
            panelMensajes.Size = new Size(this.Width - 40, this.Height - 130);
            lblEstado.Location = new Point(0, panelMensajes.Bottom + 5);
            txtPregunta.Location = new Point(0, panelMensajes.Bottom + 25);
            txtPregunta.Width = this.Width - 230;
            btnEnviar.Location = new Point(txtPregunta.Right + 10, txtPregunta.Top - 2);
            btnEjercicio.Location = new Point(btnEnviar.Right + 10, txtPregunta.Top - 2);
        }

        private async Task EnviarPreguntaAsync()
        {
            var texto = txtPregunta.Text.Trim();
            if (string.IsNullOrEmpty(texto)) return;

            AgregarMensaje("Tú", texto, esBot: false);
            txtPregunta.Clear();
            SetCargando(true);

            try
            {
                var respuesta = await _service.ChatAsync(texto);
                AgregarMensaje("Tutor IA", respuesta.Text, esBot: true);
            }
            catch (Exception ex)
            {
                AgregarMensaje("Error", $"No se pudo contactar al servidor: {ex.Message}", esBot: true, esError: true);
            }
            finally
            {
                SetCargando(false);
            }
        }

        private async Task PedirEjercicioAsync()
        {
            SetCargando(true);
            try
            {
                var respuesta = await _service.ObtenerEjercicioAsync();
                AgregarMensaje("Tutor IA", "📝 Ejercicio: " + respuesta.Text, esBot: true);
            }
            catch (Exception ex)
            {
                AgregarMensaje("Error", $"No se pudo obtener ejercicio: {ex.Message}", esBot: true, esError: true);
            }
            finally
            {
                SetCargando(false);
            }
        }

        private void AgregarMensaje(string remitente, string texto, bool esBot, bool esError = false)
        {
            var burbuja = new Panel
            {
                BackColor = esError ? Color.FromArgb(255, 205, 210)
                          : esBot ? Color.FromArgb(232, 245, 233)
                          : Color.FromArgb(255, 253, 231),
                Padding = new Padding(12, 8, 12, 8),
                Margin = new Padding(esBot ? 5 : 80, 5, esBot ? 80 : 5, 5),
                AutoSize = true,
                MaximumSize = new Size(panelMensajes.Width - 100, 0)
            };

            var lblRemitente = new Label
            {
                Text = remitente,
                Font = new Font("Segoe UI", 8, FontStyle.Bold),
                ForeColor = esBot ? Color.FromArgb(46, 125, 50) : Color.FromArgb(130, 100, 0),
                AutoSize = true,
                Location = new Point(12, 6)
            };

            var lblTexto = new Label
            {
                Text = texto,
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(30, 30, 30),
                AutoSize = true,
                MaximumSize = new Size(panelMensajes.Width - 120, 0),
                Location = new Point(12, 24)
            };

            burbuja.Controls.Add(lblRemitente);
            burbuja.Controls.Add(lblTexto);
            burbuja.Height = lblTexto.Bottom + 10;
            burbuja.Width = panelMensajes.Width - (esBot ? 90 : 90);

            panelMensajes.Controls.Add(burbuja);
            panelMensajes.ScrollControlIntoView(burbuja);
        }

        private void SetCargando(bool cargando)
        {
            lblEstado.Text = cargando ? "El tutor está pensando..." : "";
            btnEnviar.Enabled = !cargando;
            btnEjercicio.Enabled = !cargando;
            txtPregunta.Enabled = !cargando;
        }
    }
}
