using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Navigation;
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

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Paginas
{
    /// <summary>
    /// Lógica de interacción para PaginaEventos.xaml
    /// </summary>
    public partial class PaginaEventos : Page
    {
        private DataAccess _dataAccess = AppData.DataAccess;

        public PaginaEventos()
        {
            InitializeComponent();

            Loaded += PaginaEventos_Loaded;
        }

        private async void PaginaEventos_Loaded(object sender, RoutedEventArgs e)
        {
            await AsignarListaADataGrid();
        }

        async private Task AsignarListaADataGrid()
        {
            List<Evento> listaEventos = await _dataAccess.DbContext.ObtenerListaEventosAsync(); 
            dataGrid.ItemsSource = listaEventos;
        }



        private void NavegarAtras(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarAtras(NavigationService);
        }
    }
}
    

