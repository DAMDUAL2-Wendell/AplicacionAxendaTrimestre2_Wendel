using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace AplicacionAxendaTrimestre2_Wendel.Controller
{
    public class EventosController
    {


        public static async void EliminarEvento_Click(DataAcceso _dataAccess,DataGrid dataGrid,Contacto _contactoActual, Evento evento)
        {
            if (evento != null)
            {
                MessageBoxResult result = MessageBox.Show("¿Está seguro de querer eliminar el evento " + evento.Titulo +
                    "?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Verificar si hay un contacto asociado al evento y eliminar el evento de su lista de eventos
                        if (_contactoActual != null)
                        {

                            // Eliminar el evento de la base de datos
                            _dataAccess.DbContext.ListaEventos.Remove(evento);

                            // Guardar los cambios en la base de datos
                            await _dataAccess.DbContext.SaveChangesAsync();

                            // Eliminar el evento del dataGrid
                            if (dataGrid.ItemsSource is IList<Evento> eventos)
                            {
                                eventos.Remove(evento);
                                dataGrid.ItemsSource = null;
                                dataGrid.ItemsSource = eventos;
                            }
                        }
                        else
                        {
                            // Eliminar evento de la base de datos
                            await _dataAccess.DbContext.EliminarEventoPorIdAsync(evento.Id);
                            // Eliminar el evento del dataGrid
                            if (dataGrid.ItemsSource is IList<Evento> eventos)
                            {
                                eventos.Remove(evento);
                                dataGrid.ItemsSource = null;
                                dataGrid.ItemsSource = eventos;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al intentar eliminar el evento: " + ex.Message,
                            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("El evento no existe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public static List<Evento> GetListaEventosFiltrados(List<Evento> allEvents,String searchText)
        {
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
            return filteredEvents;
        }


    }
}
