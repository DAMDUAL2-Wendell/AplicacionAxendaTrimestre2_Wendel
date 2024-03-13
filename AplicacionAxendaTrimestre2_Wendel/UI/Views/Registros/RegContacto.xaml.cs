using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.Models;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Navigation;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Registros
{
    /// <summary>
    /// Lógica de interacción para RegContacto.xaml
    /// </summary>
    public partial class RegContacto : Page
    {

        private Contacto _ContactoActual;

        private DataAcceso _dataAccess = AppData.DataAccess;

        // Constructor
        public RegContacto()
        {
            InitializeComponent();

            // Inicializa un nuevo contacto
            _ContactoActual = new Contacto();

            // Establece el DataContext para toda la página
            DataContext = _ContactoActual;

        }

        // Constructor sobrecargado para edición de un contacto utilizando la misma ventana de Registro
        public RegContacto(Contacto contacto) : this()
        {
            // Verifica si se ha pasado un contacto para edición
            if (contacto != null)
            {
                // Utiliza el contacto pasado para la edición
                _ContactoActual = contacto;

                // Actualiza el DataContext con el nuevo contacto
                DataContext = _ContactoActual;

                // Llena los números de telefonos en la lista
                FillNumbersInLb_Numbers(contacto.Numbers);

                // Llena los correos electrónicos en la lista
                FillEmailsInLb_Emails(contacto.Emails);


                // Llama al método para establecer el Binding del ContactType
                SetContactTypeBinding(contacto);

                // Establece el índice seleccionado en el ComboBox
                cb_ContactType.SelectedItem = contacto.ContactType;

                // Modificación del título de la página
                labelTitle.Content = "Editar usuario";

                // Modificación del contenido del botón de registro
                btnRegistrar.Content = "Actualizar";
            }
        }

        // Selecciona el Item del ComboBox si coincide con el asignado en el contacto
        private void SetContactTypeBinding(Contacto contacto)
        {
            // Verifica si el contacto tiene un tipo de contacto establecido
            if (!string.IsNullOrEmpty(contacto.ContactType))
            {
                // Busca el tipo de contacto en los items del ComboBox
                ComboBoxItem selectedItem = cb_ContactType.Items.Cast<ComboBoxItem>().FirstOrDefault(item => item.Content.ToString() == contacto.ContactType);

                // Si se encontró el tipo de contacto, selecciona el elemento correspondiente en el ComboBox
                if (selectedItem != null)
                {
                    cb_ContactType.SelectedItem = selectedItem;
                }
            }
        }


        private List<PhoneNumber> GetNumbersFromLB_Numbers()
        {
            List<PhoneNumber> Numbers = new List<PhoneNumber>();

            for (int i = 0; i < lb_Numbers.Items.Count; i++)
            {
                PhoneNumber phoneNumber = new PhoneNumber
                {
                    Number = lb_Numbers.Items[i]?.ToString() ?? "Sin teléfono.",
                    ContactoId = _ContactoActual.Id
                };
                Numbers.Add(phoneNumber);
            }

            return Numbers;
        }

        private void FillNumbersInLb_Numbers(List<PhoneNumber> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                lb_Numbers.Items.Add(numbers[i].Number);
            }
        }

        private void FillEmailsInLb_Emails(List<Email> emails)
        {
            foreach (var email in emails)
            {
                lb_Emails.Items.Add(email.Address);
            }
        }

        public void Fill(Contacto contact, List<PhoneNumber> contactTypes)
        {
            _ContactoActual = contact;
            sp_Info.DataContext = _ContactoActual;
            FillNumbersInLb_Numbers(contact.Numbers);

            cb_ContactType.ItemsSource = contactTypes;
            if (cb_ContactType.Items.Count != 0)
                cb_ContactType.SelectedIndex = cb_ContactType.Items.IndexOf(contact.ContactType);
        }
        public Contacto GetContact()
        {
            _ContactoActual.Numbers = GetNumbersFromLB_Numbers();
            return _ContactoActual;
        }
        private void Tb_LastName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ContactoActual.LastName = $"{tb_LastName.Text}";
        }

        private void Tb_FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ContactoActual.FirstName = $"{tb_FirstName.Text}";
        }

        private void cb_ContactType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ContactoActual.ContactType = cb_ContactType?.SelectedItem?.ToString() ?? "";
        }


        private void lb_Numbers_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (lb_Numbers.SelectedItems.Count != 0)
                {
                    int selectedIndex = lb_Numbers.SelectedIndex;
                    //Remove
                    lb_Numbers.Items.RemoveAt(selectedIndex);
                }
            }
        }

        private void Btn_AddEmail_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tb_Email.Text))
            {
                if (!EmailExistsInDatabase(tb_Email.Text))
                {
                    if (!lb_Emails.Items.Contains(tb_Email.Text))
                    {
                        lb_Emails.Items.Add(tb_Email.Text);
                        _ContactoActual.Emails.Add(new Email { Address = tb_Email.Text });
                        tb_Email.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("El correo electrónico ya existe en la base de datos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        private void lb_Emails_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Delete)
            {
                if (lb_Emails.SelectedItems.Count != 0)
                {
                    // Crear una lista de índices de elementos seleccionados
                    List<int> selectedIndexes = new List<int>();
                    foreach (var item in lb_Emails.SelectedItems)
                    {
                        selectedIndexes.Add(lb_Emails.Items.IndexOf(item));
                    }

                    // Eliminar elementos seleccionados de la ListBox
                    foreach (int index in selectedIndexes.OrderByDescending(i => i))
                    {
                        lb_Emails.Items.RemoveAt(index);
                    }
                }
            }
        }



        private void Btn_AddNumber_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tb_Number.Text))
            {
                if (!PhoneNumberExistsInDatabase(tb_Number.Text))
                {
                    if (!lb_Numbers.Items.Contains(tb_Number.Text))
                    {
                        lb_Numbers.Items.Add(tb_Number.Text);
                        _ContactoActual.Numbers.Add(new PhoneNumber { Number = tb_Number.Text });
                        tb_Number.Text = "";
                    }
                }
                else
                {
                    MessageBox.Show("El número ya existe en la base de datos.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }


        // Método para verificar si el número de teléfono ya existe en la base de datos
        private bool PhoneNumberExistsInDatabase(string phoneNumber)
        {
            // Realizar una consulta para verificar si el número de teléfono ya existe en la base de datos
            bool exists = _dataAccess.DbContext.Contactos.Any(c => c.Numbers.Any(n => n.Number == phoneNumber));
            return exists;
        }

        private void Btn_DeleteNumber_Click(object sender, RoutedEventArgs e)
        {
            if (lb_Numbers.SelectedItems.Count != 0)
            {
                int selectedIndex = lb_Numbers.SelectedIndex;
                //Remove
                lb_Numbers.Items.RemoveAt(selectedIndex);
            }
        }

        private void Btn_DeleteEmail_Click(object sender, RoutedEventArgs e)
        {
            if (lb_Emails.SelectedItems.Count != 0)
            {
                int selectedIndex = lb_Emails.SelectedIndex;
                // Remove
                lb_Emails.Items.RemoveAt(selectedIndex);
            }
        }




        public static int CalcularEdad(DateTime? fechaNacimiento)
        {
            int edad = -1;
            // Verificar si la fecha de nacimiento es nula
            if (!fechaNacimiento.HasValue)
            {
                MessageBox.Show("La fecha de cumpleaños no puede ser nula");
            }
            else
            {
                // Obtener la fecha actual
                DateTime fechaActual = DateTime.Today;

                // Calcular la edad
                edad = fechaActual.Year - fechaNacimiento.Value.Year;

                // Si el día actual es anterior al día de cumpleaños en este año,
                // se resta un año a la edad
                if (fechaNacimiento.Value > fechaActual.AddYears(-edad))
                {
                    edad--;
                }
            }

            return edad;
        }

        private void RegistrarContacto_Click(object sender, RoutedEventArgs e)
        {
            // Verifica si se está editando un contacto existente
            if (_ContactoActual.Id != 0)
            {
                // Actualiza las propiedades del contacto
                _ContactoActual.FirstName = tb_FirstName.Text;
                _ContactoActual.LastName = tb_LastName.Text;
                _ContactoActual.Age = CalcularEdad(dp_Birthday.SelectedDate);
                _ContactoActual.Nickname = tb_Nickname.Text;
                _ContactoActual.Address = tb_Address.Text;
                _ContactoActual.Note = tb_Note.Text;
                _ContactoActual.BirthDate = dp_Birthday.SelectedDate;
                _ContactoActual.ContactType = (cb_ContactType.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Sin Tipo";

                try
                {
                    // Actualiza la lista de números de teléfono del contacto
                    _ContactoActual.Numbers = GetNumbersFromListBox(lb_Numbers);

                    // Actualiza la lista de correos electrónicos del contacto
                    _ContactoActual.Emails = GetEmailsFromListBox(lb_Emails);

                    // Guarda los cambios en la base de datos
                    _dataAccess.DbContext.SaveChanges();

                    MessageBox.Show("Contacto actualizado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    Navegacion.NavegarAtras(NavigationService);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al actualizar el contacto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Crea un nuevo objeto de Contacto
                Contacto nuevoContacto = new Contacto()
                {
                    FirstName = tb_FirstName.Text,
                    LastName = tb_LastName.Text,
                    Age = CalcularEdad(dp_Birthday.SelectedDate),
                    Nickname = tb_Nickname.Text,
                    Address = tb_Address.Text,
                    Note = tb_Note.Text,
                    BirthDate = dp_Birthday.SelectedDate,
                    ContactType = (cb_ContactType.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Sin Tipo",
                    // Lista de números de teléfono
                    Numbers = GetNumbersFromListBox(lb_Numbers),
                    // Lista de correos electrónicos
                    Emails = GetEmailsFromListBox(lb_Emails)
                };

                try
                {
                    // Agrega el nuevo contacto a tu contexto de base de datos
                    _dataAccess.DbContext.Contactos.Add(nuevoContacto);

                    // Guarda los cambios en la base de datos
                    _dataAccess.DbContext.SaveChanges();

                    MessageBox.Show("Contacto registrado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                    Navegacion.NavegarAtras(NavigationService);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error al registrar el contacto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private List<PhoneNumber> GetNumbersFromListBox(ListBox listBox)
        {
            var numbers = new List<PhoneNumber>();
            foreach (var item in listBox.Items)
            {
                numbers.Add(new PhoneNumber { Number = item.ToString() });
            }
            return numbers;
        }

        private List<Email> GetEmailsFromListBox(ListBox listBox)
        {
            var emails = new List<Email>();
            foreach (var item in listBox.Items)
            {
                emails.Add(new Email { Address = item.ToString() });
            }
            return emails;
        }


        private void RegistrarContacto_Click2(object sender, RoutedEventArgs e)
        {
            // Verifica si se está editando un contacto existente
            if (_ContactoActual.Id != 0)
            {
                // Si se está editando un contacto existente, actualiza sus propiedades
                _ContactoActual.FirstName = tb_FirstName.Text;
                _ContactoActual.LastName = tb_LastName.Text;
                _ContactoActual.Age = CalcularEdad(dp_Birthday.SelectedDate);
                _ContactoActual.Nickname = tb_Nickname.Text;
                _ContactoActual.Address = tb_Address.Text;
                _ContactoActual.Note = tb_Note.Text;
                _ContactoActual.BirthDate = dp_Birthday.SelectedDate;
                _ContactoActual.ContactType = (cb_ContactType.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Sin Tipo";

                try
                {
                    // Guarda los cambios en la base de datos
                    _dataAccess.DbContext.SaveChanges();

                    // Muestra un mensaje de éxito al usuario
                    MessageBox.Show("Contacto actualizado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Navegar atrás luego de actualizar un contacto con éxito
                    Navegacion.NavegarAtras(NavigationService);
                }
                catch (Exception ex)
                {
                    // Muestra un mensaje de error si ocurre algún problema al actualizar el contacto
                    MessageBox.Show($"Error al actualizar el contacto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                // Crea un nuevo objeto de Contacto
                Contacto nuevoContacto = new Contacto()
                {
                    FirstName = tb_FirstName.Text,
                    LastName = tb_LastName.Text,
                    Age = CalcularEdad(dp_Birthday.SelectedDate),
                    Nickname = tb_Nickname.Text,
                    Address = tb_Address.Text,
                    Note = tb_Note.Text,
                    BirthDate = dp_Birthday.SelectedDate,
                    ContactType = (cb_ContactType.SelectedItem as ComboBoxItem)?.Content?.ToString() ?? "Sin Tipo"
                };

                try
                {
                    // Agrega el nuevo contacto a tu contexto de base de datos
                    _dataAccess.DbContext.Contactos.Add(nuevoContacto);

                    // Guarda los cambios en la base de datos
                    _dataAccess.DbContext.SaveChanges();

                    // Muestra un mensaje de éxito al usuario
                    MessageBox.Show("Contacto registrado correctamente", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Navegar atrás luego de un registro con éxito
                    Navegacion.NavegarAtras(NavigationService);
                }
                catch (Exception ex)
                {
                    // Muestra un mensaje de error si ocurre algún problema al guardar el contacto
                    MessageBox.Show($"Error al registrar el contacto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private bool EmailExistsInDatabase(string email)
        {
            // Realiza una consulta para verificar si el correo electrónico ya existe en la base de datos
            bool exists = _dataAccess.DbContext.Contactos.Any(c => c.Emails.Any(e => e.Address == email));
            return exists;
        }



        /* ------------       NAVEGACION     ------------------*/
        private void NavegarAtras(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarAtras(NavigationService);
        }


        // Evento para seleccionar una imagen de un archivo
        private void SelectImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Archivos de imagen|*.jpg;*.jpeg;*.png;*.gif";

            if (openFileDialog.ShowDialog() == true)
            {
                // Obtener la ruta del archivo seleccionado
                string selectedImagePath = openFileDialog.FileName;

                // Establecer la imagen seleccionada como origen de la imagen
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(selectedImagePath);
                bitmap.EndInit();
                contactImage.Source = bitmap;
            }
        }
    }
}
