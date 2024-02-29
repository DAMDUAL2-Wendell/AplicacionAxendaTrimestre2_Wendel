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

        private DataAccess _dataAccess = AppData.DataAccess;

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

                // Llena los números en la lista
                FillNumbersInLb_Numbers(contacto.Numbers);

                // Establece el índice seleccionado en el ComboBox
                cb_ContactType.SelectedItem = contacto.ContactType;
            }
        }

        private List<PhoneNumber> GetNumbersFromLB_Numbers()
        {
            List<PhoneNumber> Numbers = new List<PhoneNumber>();

            for (int i = 0; i < lb_Numbers.Items.Count; i++)
            {
                Numbers.Add(new PhoneNumber(lb_Numbers?.Items[i]?.ToString() ?? "Sin teléfono.",_ContactoActual.Id));
            }

            return Numbers;
        }
        private void FillNumbersInLb_Numbers(List<PhoneNumber> numbers)
        {
            for (int i = 0; i < numbers.Count; i++)
            {
                lb_Numbers.Items.Add(numbers[i]);
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
            tb_FullName.Text = $"{_ContactoActual.FullName}";
        }

        private void Tb_FirstName_TextChanged(object sender, TextChangedEventArgs e)
        {
            _ContactoActual.FirstName = $"{tb_FirstName.Text}";
            tb_FullName.Text = $"{_ContactoActual.FullName}";
        }

        private void cb_ContactType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _ContactoActual.ContactType = cb_ContactType.SelectedItem.ToString();
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

        private void Btn_AddNumber_Click(object sender, RoutedEventArgs e)
        {
            if (!String.IsNullOrEmpty(tb_Number.Text))
            {
                if (!lb_Numbers.Items.Contains(tb_Number.Text))
                {
                    lb_Numbers.Items.Add(tb_Number.Text);
                    tb_Number.Text = "";
                }
            }
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
                // Se está editando un contacto existente, actualiza sus propiedades
                _ContactoActual.FirstName = tb_FirstName.Text;
                _ContactoActual.LastName = tb_LastName.Text;
                _ContactoActual.Age = CalcularEdad(dp_Birthday.SelectedDate);
                _ContactoActual.Nickname = tb_Nickname.Text;
                _ContactoActual.Email = tb_Email.Text;
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
                    Email = tb_Email.Text,
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
                }
                catch (Exception ex)
                {
                    // Muestra un mensaje de error si ocurre algún problema al guardar el contacto
                    MessageBox.Show($"Error al registrar el contacto: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }



        /* ------------       NAVEGACION     ------------------*/
        private void NavegarAtras(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarAtras(NavigationService);
        }

    }
}
