using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    public class RespuestaBLL
    {
        private readonly RespuestaDAO _dao = new();

        public List<Respuesta> ObtenerPorPregunta(int idPregunta)
        {
            if (idPregunta <= 0)
                throw new Exception("ID de pregunta invalido.");

            return _dao.ObtenerPorPregunta(idPregunta);
        }

        public bool ReemplazarPorPregunta(int idPregunta, List<Respuesta> respuestas)
        {
            if (idPregunta <= 0)
                throw new Exception("ID de pregunta invalido.");

            respuestas = respuestas
                .Where(r => !string.IsNullOrWhiteSpace(r.Texto))
                .Select(r => new Respuesta
                {
                    IdPregunta = idPregunta,
                    Texto = r.Texto.Trim(),
                    EsCorrecta = r.EsCorrecta
                })
                .ToList();

            if (respuestas.Count < 2)
                throw new Exception("Agregue al menos dos respuestas.");

            if (!respuestas.Any(r => r.EsCorrecta))
                throw new Exception("Marque al menos una respuesta correcta.");

            return _dao.ReemplazarPorPregunta(idPregunta, respuestas);
        }
    }
}
