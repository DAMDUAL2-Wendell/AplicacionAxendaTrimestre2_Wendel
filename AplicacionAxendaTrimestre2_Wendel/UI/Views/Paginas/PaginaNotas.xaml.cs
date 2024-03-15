using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.Controller;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Navigation;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Paginas
{
    public partial class PaginaNotas : Page
    {
        private DataAcceso _dataAccess = AppData.DataAccess;

        private Contacto _contactoActual;

        // Constructor Pagina notas normal (Mostrará todas las notas)
        public PaginaNotas()
        {
            InitializeComponent();
            Loaded += PaginaNotas_Loaded;

        }

        // Constructor de PaginaNotas pasando un contacto (Mostrará las notas de ese contacto)
        public PaginaNotas(Contacto contacto)
        {
            InitializeComponent();
            _contactoActual = contacto;
            if (_contactoActual != null)
            {
                AsignarListaNotasActualADataGrid();
                labelTitle.Content += " de " + _contactoActual.FirstName;
                btnAgregarNota.Visibility = Visibility.Visible;
            }
        }

        public DataGrid GetDataGrid()
        {
            return dataGridNotes;
        }

        public TextBox GetTextBoxFindNote()
        {
            return tb_FindNote;
        }

        
        // -------------     GESTION NOTAS --------------

        private async void PaginaNotas_Loaded(object sender, RoutedEventArgs e)
        {
            await AsignarListaADataGrid();
        }

        // Agregar notas del contacto actual al datagrid
        public async void AsignarListaNotasActualADataGrid()
        {
            if (_contactoActual != null)
            {
                // Obtener las notas asociadas al contacto actual
                List<Nota> listaNotas = _contactoActual.Notas;
                dataGridNotes.ItemsSource = listaNotas;
            }
            else
            {
                dataGridNotes.ItemsSource = new List<Nota>();
            }
        }

        // Agregar todas las notas al datagrid
        public async Task AsignarListaADataGrid()
        {
            List<Nota> listaNotas = await _dataAccess.DbContext.ObtenerListaNotasAsync();
            dataGridNotes.ItemsSource = listaNotas;
        }

        // Buscar nota
        public async void Tb_FindNote_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = tb_FindNote.Text.Trim().ToLower();

            // Obtener todas las notas de la base de datos
            var allNotes = await _dataAccess.DbContext.ObtenerListaNotasAsync();

            // Filtrar las notas
            var filteredNotes = NotasController.GetNotasFiltradas(allNotes,searchText);

            // Actualizar el DataGrid con las notas filtradas
            dataGridNotes.ItemsSource = filteredNotes;
        }

        private async void EliminarNota_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el botón que desencadenó el evento
            Button btn = sender as Button;

            // Obtener la nota asociada al botón
            Nota nota = btn.Tag as Nota;

            if (nota != null)
            {
                NotasController.EliminarNota_Click(_dataAccess,dataGridNotes,_contactoActual,nota);
            }
            else
            {
                MessageBox.Show("La nota no existe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /*
        private async Task ActualizarDataGridNotasAsync()
        {
            try
            {
                // Obtener la lista actualizada de notas desde la base de datos
                List<Nota> notas = await _dataAccess.DbContext.ObtenerListaNotasAsync();

                // Asignar la lista de notas al DataGrid
                dataGridNotes.ItemsSource = notas;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el DataGrid de notas: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }*/

        private void Click_AgregarNota(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarARegistroNotaContacto(NavigationService, _contactoActual);
        }

        // -------------     /GESTION NOTAS --------------







        /* ------------       NAVEGACION     ------------------*/
        private void NavegarAtras(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarAtras(NavigationService);
        }

        public static void NavegarPaginaNotas(NavigationService navigationService, Contacto contacto)
        {
            // Crear una instancia de la página PaginaNotas con el contacto actual
            PaginaNotas paginaNotas = new PaginaNotas(contacto);

            // Navegar a la página PaginaNotas en el NavigationService proporcionado
            navigationService.Navigate(paginaNotas);
        }
        /* ------------       /NAVEGACION     ------------------*/




    }
}
