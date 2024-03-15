using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.Controller;
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
        private DataAcceso _dataAccess = AppData.DataAccess;

        private Contacto _contactoActual;

        public PaginaEventos()
        {
            InitializeComponent();

            Loaded += PaginaEventos_Loaded;
        }

        public PaginaEventos(Contacto contacto)
        {
            InitializeComponent();
            _contactoActual = contacto;
            if(_contactoActual != null)
            {
                AsignarListaEventosActualADataGrid();
                labelTitulo.Content += " de " + _contactoActual.FirstName;
                btnAgregarEvento.Visibility = Visibility.Visible;
            }
        }

        private void Click_AgregarEvento(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarARegistroEventoContacto(NavigationService, _contactoActual);
        }

        public DataGrid GetDataGrid()
        {
            return dataGrid;
        }

        public TextBox GetTextBoxFindEven()
        {
            return tb_FindEvent;
        }

        public void AsignarListaEventosActualADataGrid()
        {
            if (_contactoActual != null)
            {
                // Obtener los eventos asociados al contacto actual
                List<Evento> listaEventos = _contactoActual.Eventos;
                dataGrid.ItemsSource = listaEventos;
            }
            else
            {
                dataGrid.ItemsSource = new List<Evento>();
            }
        }




        private async void PaginaEventos_Loaded(object sender, RoutedEventArgs e)
        {
            await AsignarListaADataGrid();
        }

        async public Task AsignarListaADataGrid()
        {
            List<Evento> listaEventos = await _dataAccess.DbContext.ObtenerListaEventosAsync(); 
            dataGrid.ItemsSource = listaEventos;
        }



        public async void Tb_FindEvent_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = tb_FindEvent.Text.Trim().ToLower();

            // Obtener todos los eventos de la base de datos
            var allEvents = await _dataAccess.DbContext.ObtenerListaEventosAsync();

            // Filtrar los eventos en memoria
            var filteredEvents = EventosController.GetListaEventosFiltrados(allEvents,searchText);

            // Actualizar el DataGrid con los eventos filtrados
            dataGrid.ItemsSource = filteredEvents;
        }

        private async void EliminarEvento_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que desencadenó el evento
            Button btn = sender as Button;

            // Obtener el evento asociado al botón
            Evento evento = btn.Tag as Evento;

            if (evento != null)
            {
                EventosController.EliminarEvento_Click(_dataAccess,dataGrid,_contactoActual,evento);
            }
            else
            {
                MessageBox.Show("El evento no existe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }



        /* ------------       NAVEGACION     ------------------*/
        private void NavegarAtras(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarAtras(NavigationService);
        }

    


    }
}
    

