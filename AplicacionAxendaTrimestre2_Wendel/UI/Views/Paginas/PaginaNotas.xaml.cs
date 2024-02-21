using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Secciones
{
    /// <summary>
    /// Lógica de interacción para PaginaNotas.xaml
    /// </summary>
    public partial class PaginaNotas : Page
    {
        private DataAccess _dataAccess = AppData.DataAccess;


        public PaginaNotas()
        {
            InitializeComponent();

            Nota notaPrueba = new Nota();
            notaPrueba.Titulo = "Nota de prueba";
            notaPrueba.Descripcion = "Contenido de prueba";

            if (_dataAccess != null)
            {
                AsignarListaADataGrid();
            }

            AsignarListaADataGrid();

        }

        private async void AsignarListaADataGrid()
        {
            //var lista = DataAccess.DbContext.Notas.ToListAsync();
            
            //List<Nota> listaNotas = lista;
            //dataGrid.ItemsSource = listaNotas;
        }

        public async Task<List<Nota>> ObtenerListaNotasAsync()
        {
            return await _dataAccess.DbContext.Notas.ToListAsync();
        }

        private void NavegarAtras(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
