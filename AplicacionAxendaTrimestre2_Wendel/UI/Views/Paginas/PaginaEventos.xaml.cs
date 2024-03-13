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
            }
        }

        private void AsignarListaEventosActualADataGrid()
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

        async private Task AsignarListaADataGrid()
        {
            List<Evento> listaEventos = await _dataAccess.DbContext.ObtenerListaEventosAsync(); 
            dataGrid.ItemsSource = listaEventos;
        }



        private async void Tb_FindEvent_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = tb_FindEvent.Text.Trim().ToLower();

            // Obtener todos los eventos de la base de datos
            var allEvents = await _dataAccess.DbContext.ObtenerListaEventosAsync();

            // Filtrar los eventos en memoria
            var filteredEvents = allEvents
                                    .Where(ev => ev.Id.ToString().Contains(searchText) ||
                                                 ev.Titulo.ToLower().Contains(searchText) ||
                                                 ev.Descripcion.ToLower().Contains(searchText) ||
                                                 ev.FechaInicio.ToString().Contains(searchText) ||
                                                 ev.FechaFin.ToString().Contains(searchText) ||
                                                 ev.Contacto.FirstName.ToLower().Contains(searchText) ||
                                                 ev.Contacto.LastName.ToLower().Contains(searchText) ||
                                                 ev.Contacto.Nickname.ToLower().Contains(searchText) ||
                                                 ev.Contacto.Emails.Any(em => em.Address.ToLower().Contains(searchText)) ||
                                                 ev.Contacto.Address.ToLower().Contains(searchText) ||
                                                 ev.Contacto.Note.ToLower().Contains(searchText) ||
                                                 ev.Contacto.Age.ToString().Contains(searchText) ||
                                                 (ev.Contacto.BirthDate != null && ev.Contacto.BirthDate.Value.ToString().Contains(searchText)) ||
                                                 ev.Contacto.ContactType.ToLower().Contains(searchText) ||
                                                 ev.Contacto.Numbers.Any(num => num.Number.ToLower().Contains(searchText)))
                                    .ToList();

            // Actualizar el DataGrid con los eventos filtrados
            dataGrid.ItemsSource = filteredEvents;
        }


        /* ------------       NAVEGACION     ------------------*/
        private void NavegarAtras(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarAtras(NavigationService);
        }

        public static void NavegarPaginaEventos(NavigationService navigationService, Contacto contacto)
        {
            // Crear una instancia de la página PaginaEventos con el contacto actual
            PaginaEventos paginaEventos = new PaginaEventos(contacto);

            // Navegar a la página PaginaEventos en el NavigationService proporcionado
            navigationService.Navigate(paginaEventos);
        }



    }
}
    

