using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AplicacionAxendaTrimestre2_Wendel.Controller
{
    public class NotasController
    {

        public static List<Nota> GetNotasFiltradas(List<Nota> allNotes, String searchText)
        {
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
            return filteredNotes;
        }

        public static async void EliminarNota_Click(DataAcceso _dataAccess, DataGrid dataGridNotes, Contacto _contactoActual, Nota nota)
        {

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
                MessageBox.Show("La nota no existe.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }




    }
}
