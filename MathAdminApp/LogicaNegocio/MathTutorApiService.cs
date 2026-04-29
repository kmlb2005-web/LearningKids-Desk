// ============================================================
// LogicaNegocio: MathTutorApiService
// Llama al endpoint POST /api/math/chat del backend
// ============================================================

using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace MathAdminApp.LogicaNegocio
{
    public class MathTutorApiService
    {
        private readonly HttpClient _http;
        private readonly string _baseUrl;

        public MathTutorApiService(string baseUrl = "http://localhost:5125")
        {
            _baseUrl = baseUrl.TrimEnd('/');
            _http = new HttpClient { Timeout = TimeSpan.FromSeconds(30) };
        }

        /// <summary>
        /// Envía una pregunta al tutor de matemáticas y devuelve la respuesta.
        /// </summary>
        public async Task<MathTutorRespuesta> ChatAsync(string pregunta, string? respuestaAlumno = null)
        {
            var payload = new
            {
                query = pregunta,
                studentAnswer = respuestaAlumno
            };

            var json = JsonSerializer.Serialize(payload);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _http.PostAsync($"{_baseUrl}/api/math/chat", content);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<MathTutorRespuesta>();
            return result ?? new MathTutorRespuesta { Text = "Sin respuesta del servidor." };
        }

        /// <summary>
        /// Obtiene un ejercicio generado automáticamente.
        /// </summary>
        public async Task<MathTutorRespuesta> ObtenerEjercicioAsync()
        {
            var response = await _http.GetAsync($"{_baseUrl}/api/math/exercise");
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<MathTutorRespuesta>();
            return result ?? new MathTutorRespuesta { Text = "Sin ejercicio disponible." };
        }
    }

    public class MathTutorRespuesta
    {
        [JsonPropertyName("text")]
        public string Text { get; set; } = string.Empty;

        [JsonPropertyName("grade")]
        public int Grade { get; set; }

        [JsonPropertyName("hasLatex")]
        public bool HasLatex { get; set; }

        [JsonPropertyName("isAnswerVerification")]
        public bool IsAnswerVerification { get; set; }
    }
}
