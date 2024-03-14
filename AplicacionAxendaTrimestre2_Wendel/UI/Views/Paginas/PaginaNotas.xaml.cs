using AplicacionAxendaTrimestre2_Wendel.bbdd;
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

        public PaginaNotas()
        {
            InitializeComponent();
            Loaded += PaginaNotas_Loaded;

        }

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

        private async void PaginaNotas_Loaded(object sender, RoutedEventArgs e)
        {
            await AsignarListaADataGrid();
        }

        public async Task AsignarListaADataGrid()
        {
            List<Nota> listaNotas = await _dataAccess.DbContext.ObtenerListaNotasAsync();
            dataGridNotes.ItemsSource = listaNotas;
        }

        public async void Tb_FindNote_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = tb_FindNote.Text.Trim().ToLower();

            // Obtener todas las notas de la base de datos
            var allNotes = await _dataAccess.DbContext.ObtenerListaNotasAsync();

            // Filtrar las notas en memoria
            var filteredNotes = allNotes
                .Where(note => note.Id.ToString().Contains(searchText) ||
                                note.Titulo.ToLower().Contains(searchText) ||
                                note.Descripcion.ToLower().Contains(searchText) ||
                                note.Contacto.FirstName.ToLower().Contains(searchText) ||
                                note.Contacto.LastName.ToLower().Contains(searchText) ||
                                note.Contacto.Nickname.ToLower().Contains(searchText) ||
                                note.Contacto.Emails.Any(em => em.Address.ToLower().Contains(searchText)) ||
                                note.Contacto.Address.ToLower().Contains(searchText) ||
                                note.Contacto.Age.ToString().Contains(searchText) ||
                                (note.Contacto.BirthDate != null && note.Contacto.BirthDate.Value.ToString().Contains(searchText)) ||
                                note.Contacto.ContactType.ToLower().Contains(searchText) ||
                                note.Contacto.Numbers.Any(num => num.Number.ToLower().Contains(searchText)))
                .ToList();

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
                MessageBoxResult result = MessageBox.Show("¿Está seguro de querer eliminar la nota " + nota.Titulo +
                    "?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    try
                    {
                        // Verificar si hay un contacto asociado a la nota y eliminar la nota de su lista de notas
                        if (_contactoActual != null)
                        {
                            MessageBox.Show("Borrando nota del contacto actual:"
                                + _contactoActual + ", Nota: " + nota.Descripcion);
                            _contactoActual.Notas.Remove(nota);

                            // Eliminar la nota de la base de datos
                            _dataAccess.DbContext.listaNotas.Remove(nota);

                            // Guardar los cambios en la base de datos
                            await _dataAccess.DbContext.SaveChangesAsync();

                            // Eliminar la nota del DataGrid
                            if (dataGridNotes.ItemsSource is IList<Nota> notas)
                            {
                                notas.Remove(nota);
                                dataGridNotes.ItemsSource = null;
                                dataGridNotes.ItemsSource = notas;
                            }
                        }
                        else
                        {
                            // Eliminar la nota de la base de datos
                            await _dataAccess.DbContext.EliminarNotaPorIdAsync(nota.Id);

                            // Eliminar la nota del DataGrid
                            if (dataGridNotes.ItemsSource is IList<Nota> notas)
                            {
                                notas.Remove(nota);
                                dataGridNotes.ItemsSource = null;
                                dataGridNotes.ItemsSource = notas;
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Ocurrió un error al intentar eliminar la nota: " + ex.Message,
                            "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("La nota es nula.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


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
        }

        private void Click_AgregarNota(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarARegistroNota(NavigationService, _contactoActual);
        }

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
    }
}
