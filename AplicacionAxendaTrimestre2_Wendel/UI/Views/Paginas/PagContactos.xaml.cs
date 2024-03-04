﻿using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.Models;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Navigation;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using AplicacionAxendaTrimestre2_Wendel.Util;
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

            try
            {
                if (_dataAccess.DbContext.Contactos.Count() == 0)
                {
                    InsertarContactosPrueba(TestDataGenerator.GenerateContactos(5));

                }
            }
            catch (Exception ex) { }




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
            var contactos = await _dataAccess.DbContext.Contactos.Where(c => c != null).ToListAsync();
            dataGrid.ItemsSource = contactos;
        }



        // Método para insertar contactos de prueba
        public void InsertarContactosPrueba(List<Contacto> contactosPrueba)
        {
            // Verificar si _dataAccess y _dataAccess.DbContext están inicializados
            if (_dataAccess != null && _dataAccess.DbContext != null)
            {
                // Agregar los contactos de prueba y guardar los cambios en la base de datos
                _dataAccess.DbContext.Contactos.AddRange(contactosPrueba);
                _dataAccess.DbContext.SaveChanges();
            }
        }

        public static List<Contacto> GetContactosPrueba()
        {
            // Datos de prueba para los contactos
            var contactosPrueba = new List<Contacto>
        {
            new Contacto
            {
                FirstName = "Juan",
                LastName = "García",
                Nickname = "Juani",
                Email = "juani@example.com",
                Address = "Calle Principal 123",
                Note = "Amigo de la infancia",
                Age = 35,
                BirthDate = new DateTime(1989, 5, 15),
                ContactType = "Amigo",
                Numbers = new List<PhoneNumber>
                {
                    new PhoneNumber { Number = "123456789" },
                    new PhoneNumber { Number = "987654321" }
                }
            },
            new Contacto
            {
                FirstName = "María",
                LastName = "López",
                Nickname = "Mary",
                Email = "mary@example.com",
                Address = "Avenida Central 456",
                Note = "Compañera de trabajo",
                Age = 30,
                BirthDate = new DateTime(1992, 8, 25),
                ContactType = "Colega",
                Numbers = new List<PhoneNumber>
                {
                    new PhoneNumber { Number = "111222333" }
                }
            },
        };

            return contactosPrueba;
        }




        private async void Eliminar_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que desencadenó el evento
            Button btn = sender as Button;

            // Obtener el DataContext del botón (que es el objeto Contacto en este caso)
            Contacto contacto = btn.DataContext as Contacto;

            if(contacto!= null)
            {
                // Eliminar el contacto de la base de datos
                _dataAccess.DbContext.Contactos.Remove(contacto);

                // Guardar los cambios en la base de datos
                await _dataAccess.DbContext.SaveChangesAsync();

                // Volver a cargar los contactos en el DataGrid
                await MostrarContactosAsync();
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
                                                c.Email.ToLower().Contains(searchText) ||
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
            // Verificar si se seleccionó un contacto para editar
            if (dataGrid.SelectedItem != null && dataGrid.SelectedItem is Contacto)
            {
                // Obtener el contacto seleccionado
                Contacto contactoSeleccionado = (Contacto)dataGrid.SelectedItem;

                // Aquí puedes pasar el contacto seleccionado como parámetro a la página de registro
                // para que se pueda cargar en la página de edición
                // Puedes usar NavigationService para navegar a la página de registro y pasar el contacto seleccionado como parámetro
                Navegacion.NavegarPaginaEdicion(NavigationService, contactoSeleccionado);
            }
        }




        private async void Btn_DeleteContact_Click(object sender, RoutedEventArgs e)
        {
            // Verificar si se seleccionaron contactos para eliminar
            if (dataGrid.SelectedItems.Count > 0)
            {
                // Eliminar los contactos seleccionados de la base de datos
                foreach (var item in dataGrid.SelectedItems)
                {
                    _dataAccess.DbContext.Contactos.Remove((Contacto)item);
                }

                // Guardar los cambios en la base de datos
                await _dataAccess.DbContext.SaveChangesAsync();

                // Actualizar el DataGrid
                var contactos = await _dataAccess.DbContext.Contactos.ToListAsync();
                dataGrid.ItemsSource = contactos;
            }
        }


        private void Btn_AddContact_Click(object sender, RoutedEventArgs e)
        {

            Navegacion.NavegarPaginaRegistro(NavigationService);


        }


        // ----------   Navegación   -------------

        private void NavegarAtras(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarAtras(NavigationService);
        }



    }
}
