using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;
using Path = System.IO.Path;
using SpreadsheetLight;
using iText;
using iText.Html2pdf;
using iText.Bouncycastle;
using iText.Kernel.Pdf;
using System.IO;
using iText.Layout;
using System.Data;

namespace AplicacionAxendaTrimestre2_Wendel.Util
{
    public class SaveFiles
    {

        // Método para convertir el DataGrid en un objeto DataTable
        public static DataTable DataGridToDataTable(DataGrid dataGrid)
        {
            // Inicializamos un objeto DataTable
            var dt = new DataTable();
            // Iteramos sobre las columnas del DataGrid y agregamos las cabeceras al datatable
            foreach (DataGridColumn dataGridColumn in dataGrid.Columns)
            {
                if (dataGridColumn is DataGridTextColumn)
                {
                    DataGridTextColumn dataGridTextColumn = (DataGridTextColumn)dataGridColumn;
                    string header = GetHeader(dataGridColumn);
                    dt.Columns.Add(header);
                }
            }

            // Iteramos sobre los items del dataGrid, obtenemos los valores de las celdas y lo agregamos
            // en un DataRow (Fila).
            foreach (var item in dataGrid.Items)
            {
                DataRow dataRow = dt.NewRow();
                foreach (var column in dataGrid.Columns)
                {
                    var propertyName = ((DataGridColumn)column).SortMemberPath;
                    var propertyInfo = item.GetType().GetProperty(propertyName);
                    string header = GetHeader(column);
                    if (propertyInfo != null)
                    {
                        // Obtenemos el valor de la celda
                        var value = propertyInfo.GetValue(item, null);
                        // Agregar el valor al DataRow en la posicion de la columna
                        dataRow[header] = value;

                    }
                    else if (item is string[])
                    {
                        var value = ((string[])item)[dataGrid.Columns.IndexOf(column)];
                        dataRow[header] = value;
                    }
                }

                // Agregamos las filas al DataTable
                dt.Rows.Add(dataRow);
            }
            // Devolvemos el objeto DataTable
            return dt;
        }

        // Metodo para obtener el valor de un DataGridcolumn en formato de String.
        private static string GetHeader(DataGridColumn dataGridColumn)
        {
            if (dataGridColumn != null)
            {
                return $"{((DataGridColumn)dataGridColumn).Header}";
            }
            else
            {
                return "Dato Desconocido.";
            }
            return dataGridColumn.Header.ToString();
        }

        // Guarda un DataTable en un fichero de Excel con formato .xlsx.
        public static void SaveDataTableExcel(System.Data.DataTable dataTable, string titulo)
        {
            SLDocument excel = new SLDocument();

            // Random para el color de fondo
            Random random = new Random();

            // Ancho máximo de celda por columna
            double maxAncho = 0;


            // Aplicar estilos al encabezado
            SLStyle estiloEncabezado = new SLStyle();
            estiloEncabezado.SetFontBold(true);
            estiloEncabezado.Fill.SetPatternType(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid);
            estiloEncabezado.Fill.SetPatternForegroundColor(System.Drawing.Color.FromArgb(0, 128, 0));

            for (int col = 1; col <= dataTable.Columns.Count; col++)
            {
                excel.SetCellValue(1, col, dataTable.Columns[col - 1].ColumnName);
                excel.SetCellStyle(1, col, estiloEncabezado);
                // Valor ancho maximo de la cabecera
                double maxAnchoCabecera = dataTable.Columns[col - 1].ColumnName.Length * 1.2;
                if (maxAnchoCabecera > maxAncho)
                {
                    maxAncho = maxAnchoCabecera;
                }
            }

            // Aplicar estilos al contenido
            SLStyle estiloCelda = new SLStyle();
            //estiloCelda.SetHorizontalAlignment(DocumentFormat.OpenXml.Spreadsheet.HorizontalAlignmentValues.Center);

            for (int row = 0; row < dataTable.Rows.Count; row++)
            {
                estiloCelda.Fill.SetPatternType(DocumentFormat.OpenXml.Spreadsheet.PatternValues.Solid);
                estiloCelda.Fill.SetPatternForegroundColor(System.Drawing.Color.FromArgb(random.Next(150, 256), random.Next(150, 256), random.Next(150, 256)));

                for (int col = 0; col < dataTable.Columns.Count; col++)
                {
                    object valorCelda = dataTable.Rows[row][col];
                    // Estimacion del ancho de una celda
                    double anchoCelda = (valorCelda != null) ? valorCelda.ToString().Length * 1.2 : 1.0;

                    // Si es mas grande asignarle el valor al valor máximo de ancho.
                    if (anchoCelda > maxAncho)
                    {
                        maxAncho = anchoCelda;
                    }

                    if (valorCelda != null)
                    {
                        if (valorCelda is bool)
                        {
                            excel.SetCellValue(row + 2, col + 1, (bool)valorCelda);
                        }
                        else if (valorCelda is DateTime)
                        {
                            excel.SetCellValue(row + 2, col + 1, (DateTime)valorCelda);
                        }
                        else
                        {
                            excel.SetCellValue(row + 2, col + 1, valorCelda.ToString());
                        }
                    }
                    else
                    {
                        excel.SetCellValue(row + 2, col + 1, String.Empty);
                    }

                    excel.SetCellStyle(row + 2, col + 1, estiloCelda);

                    // Establecer el ancho de columna.
                    excel.SetColumnWidth(col, maxAncho);

                }
            }

            excel.ImportDataTable(1, 1, dataTable, true);

            // crear archivo con fecha y hora.
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + titulo + ".xlsx";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Guardar fichero en la ruta especificada.
                excel.SaveAs(saveFileDialog.FileName);
            }
        }


        public static void SaveToPdfButton_Click(DataGrid dataGrid, string tituloHtml)
        {

            string htmlConStilos = ConvertDataGridToHtml(dataGrid, tituloHtml);

            string tempHtmlFile = Path.Combine(Path.GetTempPath(), "temp.html");
            File.WriteAllText(tempHtmlFile, htmlConStilos);

            SaveFileDialog guardarPDF = new SaveFileDialog();

            guardarPDF.FileName = DateTime.Now.ToString("yyyyMMdd-HHmm") + tituloHtml + ".pdf";

            if (guardarPDF.ShowDialog() == true)
            {
                PdfWriter writer = new PdfWriter(guardarPDF.FileName);

                var pdfWriter = new PdfWriter(writer);

                var pdfDocument = new PdfDocument(pdfWriter);

                HtmlConverter.ConvertToPdf(htmlConStilos, pdfWriter);

                string rutaGuardado = guardarPDF.FileName;

                System.Windows.MessageBox.Show($"El PDF se ha guardado en: " + rutaGuardado);

            }

        }
        /*
         * Funcion para convertir un DataGrid a HTML
         */
        public static string ConvertDataGridToHtml(DataGrid dataGrid, string tituloHtml)
        {
            // StringBuilder para almacenar el contenido.
            StringBuilder htmlBuilder = new StringBuilder();

            // Encabezado del HTML
            htmlBuilder.AppendLine("<html>");
            htmlBuilder.AppendLine("<head>");

            // Aplicar estilos para la tabla
            htmlBuilder.AppendLine("<style>");
            htmlBuilder.AppendLine("table {border-collapse: collapse; width: 100%; margin: auto; border-radius: 5px; overflow: hidden;}");
            htmlBuilder.AppendLine("th, td { border: 1px solid #ddd; padding: 10px; text-align: center; font-family: 'Arial', sans-serif}");
            htmlBuilder.AppendLine("th { background-color: #4CAF50; color: white; font-weight: bold; font-family: 'Georgia', serif; } ");
            htmlBuilder.AppendLine("tr:nth-child(even){background-color: #f2f2f2} ");
            htmlBuilder.AppendLine(".euros {text-align: right;}");
            htmlBuilder.AppendLine("h1{text-align: center; font-family: 'verdana', sans-serif;}");
            htmlBuilder.AppendLine("</style>");

            htmlBuilder.AppendLine("</head>");
            htmlBuilder.AppendLine("<body>");

            // Titulo
            htmlBuilder.AppendLine("<h1>" + tituloHtml + "</h1>");

            // Tabla
            htmlBuilder.AppendLine("<table>");
            htmlBuilder.AppendLine("<tr>");

            // Agregar los encabezados de la tabla en negrita (nombre de las columnas).
            foreach (var column in dataGrid.Columns)
            {
                htmlBuilder.AppendLine($"<th>{((DataGridColumn)column).Header}</th>");
            }
            htmlBuilder.AppendLine("</tr>");


            // Contenido de la tabla
            foreach (var item in dataGrid.Items)
            {
                htmlBuilder.AppendLine("<tr>");

                // Iteramos sobre las columnas para obtener el valor de las celdas.
                foreach (var column in dataGrid.Columns)
                {
                    // Obtener el nombre la propiedad de la columna
                    var propertyName = ((DataGridColumn)column).SortMemberPath;

                    // Obtener información en el item sobre esa propiedad.
                    var propertyInfo = item.GetType().GetProperty(propertyName);

                    // Si no es null obtenemos el valor de esa celda y lo agregamos a la tabla
                    if (propertyInfo != null)
                    {
                        var value = propertyInfo.GetValue(item, null);
                        if (value != null && value.ToString().Contains("€"))
                        {
                            htmlBuilder.AppendLine($"<td class='euros'>{value}</td>");
                        }
                        else
                        {
                            htmlBuilder.AppendLine($"<td>{value}</td>");
                        }
                    }
                    else if (item is string[])
                    {
                        var value = ((string[])item)[dataGrid.Columns.IndexOf(column)];
                        htmlBuilder.AppendLine($"<td>{value}</td>");
                    }
                    else
                    {
                        htmlBuilder.AppendLine("<td></td>");
                    }
                }

                htmlBuilder.AppendLine("</tr>");
            }

            // Cerrar etiquetas
            htmlBuilder.AppendLine("</table>");
            htmlBuilder.AppendLine("</body>");
            htmlBuilder.AppendLine("</html>");

            // Devolver un String con todo el HTML.
            return htmlBuilder.ToString();

        }

        /*
        * Función para guardar un archivo HTML
        */
        public static void SaveToHtml(string htmlContent, string filePath)
        {
            try
            {
                // Escribir el contenido HTML en el archivo
                File.WriteAllText(filePath, htmlContent);
            }
            catch (Exception ex)
            {
                // Manejar cualquier excepción
                Console.WriteLine("Error al guardar el archivo HTML: " + ex.Message);
            }
        }

        /*
         * Función para guardar un archivo HTML con SaveFileDialog
         */
        public static void GuardarFicheroHTML(DataGrid dataGrid, string tituloHtml)
        {
            // Convertir DataGrid a HTML
            string htmlContent = ConvertDataGridToHtml(dataGrid, tituloHtml);

            // Mostrar el diálogo para guardar el archivo
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.FileName = DateTime.Now.ToString("yyyyMMdd-HHmmss") + tituloHtml + ".html";
            if (saveFileDialog.ShowDialog() == true)
            {
                // Guardar el archivo en la ruta especificada
                SaveToHtml(htmlContent, saveFileDialog.FileName);
            }
        }


    }
}
