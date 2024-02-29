using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Navigation;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Lógica de interacción para PagContactos.xaml
    /// </summary>
    public partial class PagContactos : Page
    {

        private DataAccess _dataAccess = AppData.DataAccess;

        public PagContactos()
        {
            InitializeComponent();

            Loaded += PagContactos_Loaded;
        }


        private async void PagContactos_Loaded(object sender, RoutedEventArgs e)
        {
            if (_dataAccess != null)
            {
                await MostrarContactosAsync();
            }
        }

        private async Task MostrarContactosAsync()
        {
            var contactos = await _dataAccess.DbContext.Contactos.ToListAsync();
            lb_ContactBox.ItemsSource = contactos;
        }



        // ContactsCollection Contacts = new ContactsCollection();


        ///Тип Сортировки
        // Comparison<Contact> SortType = ((a, b) => { return a.FullName.CompareTo(b.FullName); });

        ///Counter для Contacts
        public int CounterID = 0;

        /*
        public MainWindow()
        {
            InitializeComponent();

            lb_ContactBox.ItemsSource = Contacts.Contacts;

            CounterID = Contacts.GetMaxID() + 1;
        }
        */



        private void Lb_ContactBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (lb_ContactBox.SelectedItems.Count > 0)
                {
                    List<Contacto> selectedContacts = lb_ContactBox.SelectedItems.Cast<Contacto>().ToList();
                    var contactList = (List<Contacto>)lb_ContactBox.ItemsSource;
                    foreach (var contact in selectedContacts)
                    {
                        contactList.Remove(contact);
                    }
                    lb_ContactBox.ItemsSource = null;
                    lb_ContactBox.ItemsSource = contactList;
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
                                                c.Email.ToLower().Contains(searchText) ||
                                                c.Address.ToLower().Contains(searchText) ||
                                                c.Note.ToLower().Contains(searchText) ||
                                                c.Age.ToString().Contains(searchText) ||
                                                (c.BirthDate != null && c.BirthDate.Value.ToString().Contains(searchText)) ||
                                                c.ContactType.ToLower().Contains(searchText) ||
                                                c.Numbers.Any(n => n.Number.ToLower().Contains(searchText)))
                                    .ToList();

            // Actualizar el ListBox con los contactos filtrados
            lb_ContactBox.ItemsSource = filteredContacts;
        }



        private void Btn_EditContact_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si se seleccionó un contacto para editar
            if (lb_ContactBox.SelectedItem != null && lb_ContactBox.SelectedItem is Contacto)
            {
                // Obtener el contacto seleccionado
                Contacto contactoSeleccionado = (Contacto)lb_ContactBox.SelectedItem;

                // Aquí puedes pasar el contacto seleccionado como parámetro a la página de registro
                // para que se pueda cargar en la página de edición
                // Puedes usar NavigationService para navegar a la página de registro y pasar el contacto seleccionado como parámetro
                Navegacion.NavegarPaginaEdicion(NavigationService, contactoSeleccionado);
            }
        }



        private async void Btn_DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si se seleccionaron contactos para eliminar
            if (lb_ContactBox.SelectedItems.Count > 0)
            {
                // Eliminar los contactos seleccionados de la base de datos
                foreach (var item in lb_ContactBox.SelectedItems)
                {
                    _dataAccess.DbContext.Contactos.Remove((Contacto)item);
                }

                // Guardar los cambios en la base de datos
                await _dataAccess.DbContext.SaveChangesAsync();

                // Actualizar el ListBox
                var contactos = await _dataAccess.DbContext.Contactos.ToListAsync();
                lb_ContactBox.ItemsSource = contactos;
            }
        }


        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //SaveJson
           // Contacts.SaveToJson();
        }

        private void Lb_ContactBox_KeyDown2(object sender, KeyEventArgs e)
        {
            /*
            //Remove Selected Contacts
            if (e.Key == Key.Delete)
            {
                if (lb_ContactBox.SelectedItems.Count != 0)
                {
                    List<int> selectedIDs = new List<int>();
                    for (int i = 0; i < lb_ContactBox.SelectedItems.Count; i++)
                    {
                        selectedIDs.Add(((Contact)lb_ContactBox.SelectedItems[i]).ID);
                    }
                    //Remove Contacts
                    Contacts.RemoveContactsByID(selectedIDs);
                    lb_ContactBox.Items.Refresh();
                }
            }
            //Refresh Getting Contacts From json
            else if ((Keyboard.Modifiers == ModifierKeys.Control) && (e.Key == Key.R))
            {
                Contacts.GetContactsFromJson();
                Contacts.Sort(SortType);
                lb_ContactBox.ItemsSource = Contacts.Contacts;
            }
            */
        }

        private void Btn_AddContact_Click(object sender, RoutedEventArgs e)
        {

            Navegacion.NavegarPaginaRegistro(NavigationService);


        }
        private void Btn_EditContact_Click2(object sender, RoutedEventArgs e)
        {
            /*
            if (lb_ContactBox.SelectedIndex != -1)
            {
                ContactInfo w = new ContactInfo();
                w.Fill((Contact)lb_ContactBox.SelectedItem, ContactsCollection.ContactTypes);
                w.ShowDialog();

                //Getting Contact From
                Contacts.Contacts.Remove((Contact)lb_ContactBox.SelectedItems[0]);
                var tContact = w.GetContact();
                tContact.ID = CounterID++;
                Contacts.Add(tContact);

                //Sorting Contacts
                Contacts.Sort(SortType);
                lb_ContactBox.Items.Refresh();

                //SaveJson
                Contacts.SaveToJson();

            }
            */
        }

        private void Btn_DeleteContact_Click2(object sender, RoutedEventArgs e)
        {
            /*
            //Remove Selected Contacts
            if (lb_ContactBox.SelectedItems.Count != 0)
            {
                List<int> selectedIDs = new List<int>();
                for (int i = 0; i < lb_ContactBox.SelectedItems.Count; i++)
                {
                    selectedIDs.Add(((Contact)lb_ContactBox.SelectedItems[i]).ID);
                }

                //Remove Contacts
                Contacts.RemoveContactsByID(selectedIDs);
                lb_ContactBox.Items.Refresh();

                //SaveJson
                Contacts.SaveToJson();
            }
            */
        }

        // Ordenar por Id
        private void SortByID_Click(object sender, RoutedEventArgs e)
        {
            SortContactsByField("Id");
        }

        // Ordenar por nombre
        private void SortByFirstName_Click(object sender, RoutedEventArgs e)
        {
            SortContactsByField("FirstName");
        }

        //  Ordenar por apellido
        private void SortByLastName_Click(object sender, RoutedEventArgs e)
        {
            SortContactsByField("LastName");
        }

        // Ordenar por Nombre completo
        private void SortByFullName_Click(object sender, RoutedEventArgs e)
        {
            SortContactsByField("FullName");
        }

        //  Ordenar por Nickname
        private void SortByNickname_Click(object sender, RoutedEventArgs e)
        {
            SortContactsByField("Nickname");
        }

        //  Ordenar por Email
        private void SortByEmail_Click(object sender, RoutedEventArgs e)
        {
            SortContactsByField("Email");
        }

        //  Ordenar por Direccion
        private void SortByAddress_Click(object sender, RoutedEventArgs e)
        {
            SortContactsByField("Address");
        }

        //  Ordenar por nota
        private void SortByNote_Click(object sender, RoutedEventArgs e)
        {
            SortContactsByField("Note");
        }

        //  Ordenar por edad
        private void SortByAge_Click(object sender, RoutedEventArgs e)
        {
            SortContactsByField("Age");
        }

        //  Ordenar por Cumpleaños
        private void SortByBirthDate_Click(object sender, RoutedEventArgs e)
        {
            SortContactsByField("BirthDate");
        }

        // Ordenar por tipo de contacto
        private void SortByContactType_Click(object sender, RoutedEventArgs e)
        {
            SortContactsByField("ContactType");
        }



        private void SortContactsByField(string fieldName)
        {
            switch (fieldName)
            {
                case "Id":
                    lb_ContactBox.ItemsSource = ((List<Contacto>)lb_ContactBox.ItemsSource)
                        .OrderBy(c => c.Id)
                        .ToList();
                    break;
                case "FirstName":
                    lb_ContactBox.ItemsSource = ((List<Contacto>)lb_ContactBox.ItemsSource)
                        .OrderBy(c => c.FirstName)
                        .ToList();
                    break;
                case "LastName":
                    lb_ContactBox.ItemsSource = ((List<Contacto>)lb_ContactBox.ItemsSource)
                        .OrderBy(c => c.LastName)
                        .ToList();
                    break;
                case "FullName":
                    lb_ContactBox.ItemsSource = ((List<Contacto>)lb_ContactBox.ItemsSource)
                        .OrderBy(c => c.FullName)
                        .ToList();
                    break;
                case "Nickname":
                    lb_ContactBox.ItemsSource = ((List<Contacto>)lb_ContactBox.ItemsSource)
                        .OrderBy(c => c.Nickname)
                        .ToList();
                    break;
                case "Email":
                    lb_ContactBox.ItemsSource = ((List<Contacto>)lb_ContactBox.ItemsSource)
                        .OrderBy(c => c.Email)
                        .ToList();
                    break;
                case "Address":
                    lb_ContactBox.ItemsSource = ((List<Contacto>)lb_ContactBox.ItemsSource)
                        .OrderBy(c => c.Address)
                        .ToList();
                    break;
                case "Note":
                    lb_ContactBox.ItemsSource = ((List<Contacto>)lb_ContactBox.ItemsSource)
                        .OrderBy(c => c.Note)
                        .ToList();
                    break;
                case "Age":
                    lb_ContactBox.ItemsSource = ((List<Contacto>)lb_ContactBox.ItemsSource)
                        .OrderBy(c => c.Age)
                        .ToList();
                    break;
                case "BirthDate":
                    lb_ContactBox.ItemsSource = ((List<Contacto>)lb_ContactBox.ItemsSource)
                        .OrderBy(c => c.BirthDate)
                        .ToList();
                    break;
                case "ContactType":
                    lb_ContactBox.ItemsSource = ((List<Contacto>)lb_ContactBox.ItemsSource)
                        .OrderBy(c => c.ContactType)
                        .ToList();
                    break;
                default:
                    break;
            }
        }



        private void NavegarAtras(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarAtras(NavigationService);
        }



    }
}
