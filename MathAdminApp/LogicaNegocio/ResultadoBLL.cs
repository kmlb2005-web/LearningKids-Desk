// ============================================================
// Capa de Logica de Negocio: ResultadoBLL
// Logica de negocio para consulta de resultados
// ============================================================

using MathAdminApp.AccesoDatos;
using MathAdminApp.Modelos;

namespace MathAdminApp.LogicaNegocio
{
    /// <summary>
    /// Clase de logica de negocio para ResultadoExamen.
    /// Solo lectura.
    /// </summary>
    public class ResultadoBLL
    {
        private readonly ResultadoDAL _dal = new();

        public List<ResultadoExamen> ObtenerPorAlumno(int usuarioId) => _dal.ObtenerPorAlumno(usuarioId);

        public List<ResultadoExamen> ObtenerTodos() => _dal.ObtenerTodos();
    }
}
