using Syncfusion.UI.Xaml.Grid;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Data;

using System.Windows.Controls;
using System.Collections.ObjectModel;

namespace AplicacionAxendaTrimestre2_Wendel.Util
{
    public class DataGridHelper
    {

        public static DataGrid EliminarEncabezadoPorIndice(DataGrid dataGrid, int indice)
        {
            // Verificar si el índice es válido
            if (indice >= 0 && indice < dataGrid.Columns.Count)
            {
                // Eliminar la columna en el índice especificado
                dataGrid.Columns.RemoveAt(indice);
            }
            return dataGrid;
        }

        public static DataGrid EliminarEncabezadosPorIndices(DataGrid dataGrid, int[] indices)
        {
            // Ordenar los índices de columna de mayor a menor
            Array.Sort(indices);
            Array.Reverse(indices);

            // Eliminar las columnas en los índices especificados
            foreach (int indice in indices)
            {
                if (indice >= 0 && indice < dataGrid.Columns.Count)
                {
                    dataGrid.Columns.RemoveAt(indice);
                }
            }
            return dataGrid;
        }


        public static DataGrid DuplicarDataGrid(DataGrid dataGridOriginal)
        {
            if (dataGridOriginal == null)
            {
                throw new ArgumentNullException(nameof(dataGridOriginal), "El argumento 'dataGridOriginal' no puede ser nulo.");
            }

            // Crear un nuevo DataGrid
            DataGrid dataGridModificado = new DataGrid();

            // Configurar columnas (opcional, dependiendo de tu caso)
            if (dataGridOriginal.Columns != null)
            {
                foreach (var column in dataGridOriginal.Columns)
                {
                    dataGridModificado.Columns.Add(new DataGridTextColumn()
                    {
                        Header = column.Header,
                        Binding = (column as DataGridTextColumn).Binding
                    });
                }
            }

            // Convertir ItemCollection a IEnumerable<object> y luego copiar los datos
            if (dataGridOriginal.Items != null)
            {
                dataGridModificado.ItemsSource = (dataGridOriginal.Items as IEnumerable).Cast<object>().ToList();
            }

            return dataGridModificado;
        }







        public static DataGrid ConvertToWpfDataGrid(SfDataGrid sfDataGrid)
        {
            // Crear un nuevo DataGrid
            DataGrid dataGrid = new DataGrid();

            // Asignar el origen de los datos
            if (sfDataGrid.ItemsSource != null)
            {
                // Convertir el origen de datos al tipo adecuado para DataGrid
                var sourceCollection = sfDataGrid.ItemsSource as IEnumerable;
                dataGrid.ItemsSource = sourceCollection;
            }

            // Copiar propiedades
            dataGrid.AutoGenerateColumns = sfDataGrid.AutoGenerateColumns;
            dataGrid.CanUserDeleteRows = sfDataGrid.AllowDeleting;
            dataGrid.CanUserResizeColumns = sfDataGrid.AllowResizingColumns;
            dataGrid.IsReadOnly = !sfDataGrid.AllowEditing; // Invertir el valor de AllowEditing para ReadOnly

            // Verificar si hay columnas generadas automáticamente
            if (sfDataGrid.AutoGenerateColumns)
            {
                // No es necesario copiar columnas si se generan automáticamente
                return dataGrid;
            }

            // Copiar columnas
            foreach (var column in sfDataGrid.Columns)
            {
                // Convertir SfDataGridColumn a DataGridColumn
                DataGridColumn wpfColumn = null;
                if (column is GridTemplateColumn templateColumn)
                {
                    // Si es una columna de plantilla, copiar la plantilla
                    wpfColumn = new DataGridTemplateColumn
                    {
                        Header = templateColumn.HeaderText,
                        CellTemplate = templateColumn.CellTemplate
                    };
                }
                else
                {
                    // Si es una columna de texto, copiar el encabezado y el enlace
                    wpfColumn = new DataGridTextColumn
                    {
                        Header = column.HeaderText,
                        Binding = new Binding(column.MappingName)
                    };
                }

                // Agregar la columna al DataGrid
                dataGrid.Columns.Add(wpfColumn);
            }

            return dataGrid;
        }
    }


}
