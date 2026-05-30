using System.Globalization;
using System.Text;
using MathAdminApp.LogicaNegocio;
using MathAdminApp.Modelos;

namespace MathAdminApp.Presentacion
{
    internal static class ExportadorBitacoraPdf
    {
        public static void Generar(Usuario administrador, string rutaArchivo)
        {
            UsuarioBLL usuarioBLL = new();
            ProyectoBLL proyectoBLL = new();
            TemaBLL temaBLL = new();
            PruebaBLL pruebaBLL = new();
            ResultadoBLL resultadoBLL = new();
            BitacoraSistemaBLL bitacoraBLL = new();

            List<Usuario> usuarios = usuarioBLL.ObtenerUsuarios();
            List<Usuario> alumnos = usuarioBLL.ObtenerAlumnos();
            List<Proyecto> proyectos = proyectoBLL.ObtenerTodos();
            List<Tema> temas = temaBLL.ObtenerTemas();
            List<Prueba> pruebas = pruebaBLL.ObtenerPorTema();
            List<Resultado> resultados = resultadoBLL.ObtenerResultados();
            List<BitacoraSistema> movimientos;

            try
            {
                movimientos = bitacoraBLL.ObtenerUltimosDias(30);
            }
            catch
            {
                movimientos = new List<BitacoraSistema>();
            }

            string contenido = CrearPaginaResumen(
                administrador,
                usuarios,
                alumnos,
                proyectos,
                temas,
                pruebas,
                resultados,
                movimientos
            );

            EscribirPdf(rutaArchivo, contenido);
        }

        private static string CrearPaginaResumen(
            Usuario administrador,
            List<Usuario> usuarios,
            List<Usuario> alumnos,
            List<Proyecto> proyectos,
            List<Tema> temas,
            List<Prueba> pruebas,
            List<Resultado> resultados,
            List<BitacoraSistema> movimientos)
        {
            StringBuilder sb = new();

            Rect(sb, 0.93F, 0.97F, 1F, 0, 0, 612, 792, true);
            Rect(sb, 0.15F, 0.42F, 0.78F, 40, 700, 532, 58, true);
            Texto(sb, "BITACORA GENERAL - LEARNINGKIDS", 62, 732, 18, 1F, 1F, 1F);
            Texto(sb, $"Generada: {DateTime.Now:dd/MM/yyyy HH:mm}", 62, 713, 10, 1F, 1F, 1F);
            Texto(sb, $"Administrador: {administrador.Nombre} ({administrador.Username})", 355, 713, 10, 1F, 1F, 1F);

            Texto(sb, "Resumen de informacion", 50, 670, 15);

            List<(string Categoria, string Total, string Detalle)> resumen = new()
            {
                ("Usuarios registrados", usuarios.Count.ToString(), "Total de cuentas en el sistema"),
                ("Administradores", usuarios.Count(u => u.IdRol == 1).ToString(), "Usuarios con acceso completo"),
                ("Docentes", usuarios.Count(u => u.IdRol == 2).ToString(), "Usuarios que gestionan alumnos"),
                ("Alumnos", alumnos.Count.ToString(), "Cuentas de estudiantes"),
                ("Proyectos", proyectos.Count.ToString(), "Proyectos creados"),
                ("Temas", temas.Count.ToString(), "Temas disponibles"),
                ("Pruebas", pruebas.Count.ToString(), "Evaluaciones registradas"),
                ("Resultados", resultados.Count.ToString(), "Calificaciones capturadas")
            };

            DibujarTabla(
                sb,
                50,
                640,
                new[] { "Categoria", "Total", "Detalle" },
                resumen.Select(r => new[] { r.Categoria, r.Total, r.Detalle }).ToList(),
                new[] { 210, 80, 242 }
            );

            Texto(sb, "Movimientos por dia", 50, 355, 15);

            List<string[]> filasBitacora = movimientos
                .OrderByDescending(m => m.Fecha)
                .Take(7)
                .Select(m => new[]
                {
                    m.Fecha.ToString("dd/MM/yyyy"),
                    m.Fecha.ToString("HH:mm"),
                    m.Usuario,
                    m.Accion,
                    $"{m.Modulo}: {m.Detalle}"
                })
                .ToList();

            if (filasBitacora.Count == 0)
            {
                filasBitacora.Add(new[]
                {
                    DateTime.Today.ToString("dd/MM/yyyy"),
                    "-",
                    "-",
                    "Sin movimientos",
                    "Aun no hay acciones registradas para mostrar."
                });
            }

            DibujarTabla(
                sb,
                50,
                325,
                new[] { "Dia", "Hora", "Usuario", "Accion", "Detalle" },
                filasBitacora,
                new[] { 75, 55, 95, 85, 222 }
            );

            return sb.ToString();
        }

        private static void DibujarTabla(
            StringBuilder sb,
            int x,
            int y,
            string[] encabezados,
            List<string[]> filas,
            int[] anchos)
        {
            const int altoFila = 28;
            int anchoTotal = anchos.Sum();

            Rect(sb, 0.12F, 0.36F, 0.70F, x, y - altoFila, anchoTotal, altoFila, true);
            Rect(sb, 0.12F, 0.36F, 0.70F, x, y - altoFila, anchoTotal, altoFila, false);

            int cursorX = x;
            for (int i = 0; i < encabezados.Length; i++)
            {
                Texto(sb, encabezados[i], cursorX + 10, y - 19, 10, 1F, 1F, 1F);
                cursorX += anchos[i];
            }

            int cursorY = y - altoFila;

            for (int fila = 0; fila < filas.Count; fila++)
            {
                cursorY -= altoFila;

                if (fila % 2 == 0)
                    Rect(sb, 0.98F, 0.99F, 1F, x, cursorY, anchoTotal, altoFila, true);
                else
                    Rect(sb, 1F, 1F, 1F, x, cursorY, anchoTotal, altoFila, true);

                Rect(sb, 0.80F, 0.86F, 0.93F, x, cursorY, anchoTotal, altoFila, false);

                cursorX = x;

                for (int columna = 0; columna < filas[fila].Length; columna++)
                {
                    Texto(
                        sb,
                        Acortar(filas[fila][columna], columna == filas[fila].Length - 1 ? 58 : 28),
                        cursorX + 10,
                        cursorY + 10,
                        9
                    );

                    cursorX += anchos[columna];
                }
            }
        }

        private static void Rect(
            StringBuilder sb,
            float r,
            float g,
            float b,
            int x,
            int y,
            int ancho,
            int alto,
            bool relleno)
        {
            string color = $"{r.ToString("0.###", CultureInfo.InvariantCulture)} " +
                $"{g.ToString("0.###", CultureInfo.InvariantCulture)} " +
                $"{b.ToString("0.###", CultureInfo.InvariantCulture)}";

            sb.Append(relleno ? $"{color} rg\n" : $"{color} RG\n");
            sb.Append($"{x} {y} {ancho} {alto} re {(relleno ? "f" : "S")}\n");
        }

        private static void Texto(
            StringBuilder sb,
            string texto,
            int x,
            int y,
            int tamano,
            float r = 0.10F,
            float g = 0.16F,
            float b = 0.28F)
        {
            sb.Append("BT\n");
            sb.Append($"{r.ToString("0.###", CultureInfo.InvariantCulture)} " +
                $"{g.ToString("0.###", CultureInfo.InvariantCulture)} " +
                $"{b.ToString("0.###", CultureInfo.InvariantCulture)} rg\n");
            sb.Append($"/F1 {tamano} Tf\n");
            sb.Append($"{x} {y} Td\n");
            sb.Append($"({EscaparPdf(Normalizar(texto))}) Tj\n");
            sb.Append("ET\n");
        }

        private static void EscribirPdf(string rutaArchivo, string contenido)
        {
            byte[] contenidoBytes = Encoding.ASCII.GetBytes(contenido);

            List<string> objetos = new()
            {
                "1 0 obj\n<< /Type /Catalog /Pages 2 0 R >>\nendobj\n",
                "2 0 obj\n<< /Type /Pages /Kids [3 0 R] /Count 1 >>\nendobj\n",
                "3 0 obj\n<< /Type /Page /Parent 2 0 R /MediaBox [0 0 612 792] " +
                "/Resources << /Font << /F1 5 0 R >> >> /Contents 4 0 R >>\nendobj\n",
                "4 0 obj\n" +
                $"<< /Length {contenidoBytes.Length} >>\n" +
                "stream\n" +
                contenido +
                "endstream\n" +
                "endobj\n",
                "5 0 obj\n<< /Type /Font /Subtype /Type1 /BaseFont /Helvetica >>\nendobj\n"
            };

            EscribirObjetosPdf(rutaArchivo, objetos, 5);
        }

        private static void EscribirObjetosPdf(
            string rutaArchivo,
            List<string> objetos,
            int ultimoObjetoId)
        {
            using FileStream fs = new(rutaArchivo, FileMode.Create, FileAccess.Write);
            using StreamWriter writer = new(fs, Encoding.ASCII);

            writer.Write("%PDF-1.4\n");

            List<long> offsets = new() { 0 };

            foreach (string objeto in objetos)
            {
                writer.Flush();
                offsets.Add(fs.Position);
                writer.Write(objeto);
            }

            writer.Flush();
            long xrefOffset = fs.Position;

            writer.Write($"xref\n0 {ultimoObjetoId + 1}\n");
            writer.Write("0000000000 65535 f \n");

            for (int i = 1; i <= ultimoObjetoId; i++)
            {
                writer.Write($"{offsets[i]:0000000000} 00000 n \n");
            }

            writer.Write("trailer\n");
            writer.Write($"<< /Size {ultimoObjetoId + 1} /Root 1 0 R >>\n");
            writer.Write("startxref\n");
            writer.Write($"{xrefOffset}\n");
            writer.Write("%%EOF");
        }

        private static string Acortar(string texto, int maximo)
        {
            if (texto.Length <= maximo)
                return texto;

            return $"{texto[..Math.Max(0, maximo - 3)]}...";
        }

        private static string EscaparPdf(string texto)
        {
            return texto
                .Replace("\\", "\\\\")
                .Replace("(", "\\(")
                .Replace(")", "\\)");
        }

        private static string Normalizar(string texto)
        {
            StringBuilder sb = new();
            string textoPlano = texto.Normalize(NormalizationForm.FormD);

            foreach (char c in textoPlano)
            {
                if (CharUnicodeInfo.GetUnicodeCategory(c) == UnicodeCategory.NonSpacingMark)
                    continue;

                sb.Append(c <= 127 ? c : ' ');
            }

            return sb.ToString();
        }
    }
}
