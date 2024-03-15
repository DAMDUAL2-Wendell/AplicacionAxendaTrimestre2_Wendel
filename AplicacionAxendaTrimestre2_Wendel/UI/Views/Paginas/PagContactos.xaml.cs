using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.Models;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Navigation;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using AplicacionAxendaTrimestre2_Wendel.Util;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Linq.Expressions;
using System.Threading;
using Syncfusion.UI.Xaml.Grid;
using System.Collections;
using AplicacionAxendaTrimestre2_Wendel.Controller;



namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Paginas
{
    /// <summary>
    /// Lógica de interacción para PagContactos.xaml
    /// </summary>
    public partial class PagContactos : Page
    {

        private DataAcceso _dataAccess = AppData.DataAccess;
        private Contacto _contactoActual;

        SpeechController _speechController;


        public PagContactos()
        {
            InitializeComponent();
            _contactoActual = null;

            _speechController = new SpeechController();

            Loaded += PagContactos_Loaded;

        }


        private async void PagContactos_Loaded(object sender, RoutedEventArgs e)
        {
            if (_dataAccess != null)
            {
                await MostrarContactosAsync();
            }
        }



        // -------  GESTION CONTACTOS  ---------------

        // Agregar todos los contactos al DataGrid
        public async Task MostrarContactosAsync()
        {
            // Obtener todos los contactos de la base de datos, incluyendo las propiedades de navegación relacionadas
            var contactos = await _dataAccess.DbContext.Contactos
                                .Include(c => c.Numbers)
                                .Include(c => c.Emails)
                                .Include(c => c.Notas)
                                .Include(c => c.Eventos)
                                .Where(c => c != null)
                                .ToListAsync();

            dataGrid.ItemsSource = contactos;
        }


        //  Eliminar contacto con el boton en el datagrid
        private async void Click_BtnDataGrid(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que desencadenó el evento
            Button btn = sender as Button;

            // Obtener el DataContext del botón (que es el objeto Contacto en este caso)
            Contacto contacto = btn.DataContext as Contacto;

            if (contacto != null)
            {
                MessageBoxResult result = MessageBox.Show("¿Está seguro de querer eliminar al usuario " + contacto.FirstName +
                    "?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    // Eliminar el contacto de la base de datos
                    _dataAccess.DbContext.Contactos.Remove(contacto);

                    // Guardar los cambios en la base de datos
                    await _dataAccess.DbContext.SaveChangesAsync();

                    // Volver a cargar los contactos en el DataGrid
                    await MostrarContactosAsync();
                }

            }
        }


        private async void Lb_ContactBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (dataGrid.SelectedItems.Count > 0)
                {
                    List<Contacto> selectedContacts = dataGrid.SelectedItems.Cast<Contacto>().ToList();
                    foreach (var contact in selectedContacts)
                    {
                        _dataAccess.DbContext.Contactos.Remove(contact);
                    }
                    await _dataAccess.DbContext.SaveChangesAsync();

                    // Actualizar el DataGrid
                    await MostrarContactosAsync();
                }
            }
        }


        private void Tb_FindContact_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = tb_FindContact.Text.Trim().ToLower();

            // Obtener todos los contactos de la base de datos
            var allContacts = _dataAccess.DbContext.Contactos.ToList();

            // Filtrar los contactos en memoria
            var filteredContacts = allContacts
                                    .Where(c => c.Id.ToString().Contains(searchText) ||
                                                c.FirstName.ToLower().Contains(searchText) ||
                                                c.LastName.ToLower().Contains(searchText) ||
                                                c.Nickname.ToLower().Contains(searchText) ||
                                                c.Emails.Any(e => e.Address.ToLower().Contains(searchText)) ||
                                                c.Address.ToLower().Contains(searchText) ||
                                                c.Note.ToLower().Contains(searchText) ||
                                                c.Age.ToString().Contains(searchText) ||
                                                (c.BirthDate != null && c.BirthDate.Value.ToString().Contains(searchText)) ||
                                                c.ContactType.ToLower().Contains(searchText) ||
                                                c.Numbers.Any(n => n.Number.ToLower().Contains(searchText)))
                                    .ToList();

            // Actualizar el ListBox con los contactos filtrados
            dataGrid.ItemsSource = filteredContacts;
        }


        private void Btn_EditContact_Click(object sender, RoutedEventArgs e)
        {
            Contacto contacto = ContactoController.GetcontactoSeleccinoadoDataGrid(dataGrid);
            if (contacto != null)
            {
                 Navegacion.NavegarPaginaEdicion(NavigationService, contacto);
            }
        }


        private async void Btn_DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            Contacto contacto = ContactoController.GetcontactoSeleccinoadoDataGrid(dataGrid);
            if (contacto != null)
            {
                ContactoController.EliminaContacto(_dataAccess, dataGrid, contacto);
            }
        }

        private void LeerContacto(object sender, RoutedEventArgs e)
        {
            Contacto contacto = ContactoController.GetcontactoSeleccinoadoDataGrid(dataGrid);
            if (contacto != null)
            {
                _speechController.LeerContacto(contacto);
            }
        }


        private void Btn_AddContact_Click(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarPaginaRegistro(NavigationService);
        }


        //  Duplicar contacto seleccionado del dataGrid
        private async void ClickDuplicarContacto(object sender, RoutedEventArgs e)
        {
            // Cogemos el contacto que queremos duplicar (El seleccionado del datagrid)
            Contacto contactoADuplicar = ContactoController.GetContactoDataGridSeleccionado(dataGrid);

            // Duplicamos el contacto
            ContactoController.DuplicarContacto(_dataAccess, dataGrid, contactoADuplicar);
        }


        // Asignar el contacto seleccionado del dataGrid al  _contactoActual
        private void DataGrid_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            // Verificar si se seleccionó un contacto del DataGrid
            if (dataGrid.SelectedItem != null && dataGrid.SelectedItem is Contacto)
            {
                // Asignar el contacto seleccionado a la variable _contactoActual
                _contactoActual = (Contacto)dataGrid.SelectedItem;

            }
        }

        // -------  /GESTION CONTACTOS  ---------------






        // -------   Funciones VOZ  ---------------

        // Función para pausar la voz
        private void btn_pausaLeer_Click(object sender, RoutedEventArgs e)
        {
            _speechController.PausarLectura();
        }

        // Función para continuar la voz
        private async void btn_continuaLeer_Click(object sender, RoutedEventArgs e)
        {
            _speechController.ContinuarLectura();
        }
        // -------   /Funciones VOZ  ---------------





        // -------      GUARDAR INFORMES PDF EXCEL HTML     ----------
        private void Click_Save_PDF(object sender, RoutedEventArgs e)
        {
            DataGridHelper.EliminarEncabezadosPorIndices(dataGrid, new int[] { 0, 1, 5, 11, 12, 13, 14 });

            SaveFiles.SaveToPdfButton_Click(dataGrid, "Contactos");

            Navegacion.NavegarPaginaContactos(NavigationService);

        }

        private void Click_Save_EXCEL(object sender, RoutedEventArgs e)
        {
            SaveFiles.SaveDataTableExcel(SaveFiles.DataGridToDataTable(dataGrid), "Contactos");

            Navegacion.NavegarPaginaContactos(NavigationService);
        }

        private void Click_Save_HTML(object sender, RoutedEventArgs e)
        {
            DataGridHelper.EliminarEncabezadosPorIndices(dataGrid, new int[] { 0, 1, 5, 11, 12, 13, 14 });

            SaveFiles.GuardarFicheroHTML(dataGrid, "Contactos");

            Navegacion.NavegarPaginaContactos(NavigationService);
        }
        // ---------   /GUARDAR INFORMES    ---------------






        // ----------   Navegación   -------------
        private void NavegarAtras(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarAtras(NavigationService);
        }

        private void MostrarEventos_Click(object sender, RoutedEventArgs e)
        {
            if (_contactoActual != null)
            {
                Navegacion.NavegarPaginaEventosContacto(NavigationService, _contactoActual);
            }
        }

        private void MostrarNotas_Click(object sender, RoutedEventArgs e)
        {
            if (_contactoActual != null)
            {
                Navegacion.NavegarPaginaNotasContacto(NavigationService, _contactoActual);
            }
        }
        // ----------   /Navegación   -------------





    }
}
