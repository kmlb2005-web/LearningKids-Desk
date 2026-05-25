using System.IO.Compression;
using System.Text;
using System.Xml;

namespace MathAdminApp.Presentacion
{
    internal static class ExportadorExcel
    {
        public static void Exportar(DataGridView grid, string nombreBase)
        {
            if (grid.Rows.Cast<DataGridViewRow>().All(fila => fila.IsNewRow))
            {
                MessageBox.Show(
                    "No hay datos para exportar.",
                    "Exportar a Excel",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );

                return;
            }

            using SaveFileDialog dialogo = new()
            {
                Filter = "Archivo de Excel (*.xlsx)|*.xlsx",
                FileName = $"{nombreBase}_{DateTime.Now:yyyyMMdd_HHmm}.xlsx",
                Title = "Guardar archivo de Excel"
            };

            if (dialogo.ShowDialog() != DialogResult.OK)
            {
                return;
            }

            try
            {
                CrearArchivo(grid, dialogo.FileName);

                MessageBox.Show(
                    "Datos exportados correctamente.",
                    "Exportar a Excel",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"No se pudo exportar el archivo.\n{ex.Message}",
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private static void CrearArchivo(DataGridView grid, string ruta)
        {
            using FileStream archivo = new(ruta, FileMode.Create, FileAccess.Write);
            using ZipArchive zip = new(archivo, ZipArchiveMode.Create);

            AgregarEntrada(zip, "[Content_Types].xml", ObtenerTiposContenido());
            AgregarEntrada(zip, "_rels/.rels", ObtenerRelacionesRaiz());
            AgregarEntrada(zip, "xl/workbook.xml", ObtenerLibro());
            AgregarEntrada(zip, "xl/_rels/workbook.xml.rels", ObtenerRelacionesLibro());
            AgregarEntrada(zip, "xl/styles.xml", ObtenerEstilos());
            AgregarEntrada(zip, "xl/worksheets/sheet1.xml", ObtenerHoja(grid));
        }

        private static void AgregarEntrada(ZipArchive zip, string nombre, string contenido)
        {
            ZipArchiveEntry entrada = zip.CreateEntry(nombre);

            using StreamWriter writer = new(
                entrada.Open(),
                new UTF8Encoding(false)
            );

            writer.Write(contenido);
        }

        private static string ObtenerHoja(DataGridView grid)
        {
            List<DataGridViewColumn> columnas = grid.Columns
                .Cast<DataGridViewColumn>()
                .Where(columna => columna.Visible)
                .OrderBy(columna => columna.DisplayIndex)
                .ToList();

            StringBuilder filas = new();
            int indiceFila = 1;

            filas.Append("<row r=\"1\">");

            for (int i = 0; i < columnas.Count; i++)
            {
                filas.Append(CeldaTexto(indiceFila, i + 1, columnas[i].HeaderText, true));
            }

            filas.Append("</row>");
            indiceFila++;

            foreach (DataGridViewRow fila in grid.Rows)
            {
                if (fila.IsNewRow)
                {
                    continue;
                }

                filas.Append($"<row r=\"{indiceFila}\">");

                for (int i = 0; i < columnas.Count; i++)
                {
                    object? valor = fila.Cells[columnas[i].Index].FormattedValue;
                    filas.Append(CeldaTexto(indiceFila, i + 1, valor?.ToString() ?? string.Empty));
                }

                filas.Append("</row>");
                indiceFila++;
            }

            return $"""
                <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
                <worksheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main">
                  <sheetData>{filas}</sheetData>
                </worksheet>
                """;
        }

        private static string CeldaTexto(
            int fila,
            int columna,
            string texto,
            bool encabezado = false)
        {
            string referencia = $"{NombreColumna(columna)}{fila}";
            string estilo = encabezado ? " s=\"1\"" : string.Empty;

            return
                $"<c r=\"{referencia}\" t=\"inlineStr\"{estilo}><is><t>{XmlEscape(texto)}</t></is></c>";
        }

        private static string NombreColumna(int indice)
        {
            StringBuilder nombre = new();

            while (indice > 0)
            {
                indice--;
                nombre.Insert(0, (char)('A' + indice % 26));
                indice /= 26;
            }

            return nombre.ToString();
        }

        private static string XmlEscape(string texto)
        {
            StringBuilder limpio = new();

            foreach (char caracter in texto)
            {
                if (XmlConvert.IsXmlChar(caracter))
                {
                    limpio.Append(caracter);
                }
            }

            return System.Security.SecurityElement.Escape(limpio.ToString())
                ?? string.Empty;
        }

        private static string ObtenerTiposContenido()
        {
            return """
                <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
                <Types xmlns="http://schemas.openxmlformats.org/package/2006/content-types">
                  <Default Extension="rels" ContentType="application/vnd.openxmlformats-package.relationships+xml"/>
                  <Default Extension="xml" ContentType="application/xml"/>
                  <Override PartName="/xl/workbook.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.sheet.main+xml"/>
                  <Override PartName="/xl/worksheets/sheet1.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.worksheet+xml"/>
                  <Override PartName="/xl/styles.xml" ContentType="application/vnd.openxmlformats-officedocument.spreadsheetml.styles+xml"/>
                </Types>
                """;
        }

        private static string ObtenerRelacionesRaiz()
        {
            return """
                <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
                <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
                  <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/officeDocument" Target="xl/workbook.xml"/>
                </Relationships>
                """;
        }

        private static string ObtenerLibro()
        {
            return """
                <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
                <workbook xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main" xmlns:r="http://schemas.openxmlformats.org/officeDocument/2006/relationships">
                  <sheets>
                    <sheet name="Datos" sheetId="1" r:id="rId1"/>
                  </sheets>
                </workbook>
                """;
        }

        private static string ObtenerRelacionesLibro()
        {
            return """
                <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
                <Relationships xmlns="http://schemas.openxmlformats.org/package/2006/relationships">
                  <Relationship Id="rId1" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/worksheet" Target="worksheets/sheet1.xml"/>
                  <Relationship Id="rId2" Type="http://schemas.openxmlformats.org/officeDocument/2006/relationships/styles" Target="styles.xml"/>
                </Relationships>
                """;
        }

        private static string ObtenerEstilos()
        {
            return """
                <?xml version="1.0" encoding="UTF-8" standalone="yes"?>
                <styleSheet xmlns="http://schemas.openxmlformats.org/spreadsheetml/2006/main">
                  <fonts count="2">
                    <font><sz val="11"/><name val="Calibri"/></font>
                    <font><b/><sz val="11"/><name val="Calibri"/></font>
                  </fonts>
                  <fills count="1"><fill><patternFill patternType="none"/></fill></fills>
                  <borders count="1"><border/></borders>
                  <cellStyleXfs count="1"><xf numFmtId="0" fontId="0" fillId="0" borderId="0"/></cellStyleXfs>
                  <cellXfs count="2">
                    <xf numFmtId="0" fontId="0" fillId="0" borderId="0" xfId="0"/>
                    <xf numFmtId="0" fontId="1" fillId="0" borderId="0" xfId="0" applyFont="1"/>
                  </cellXfs>
                </styleSheet>
                """;
        }
    }
}
