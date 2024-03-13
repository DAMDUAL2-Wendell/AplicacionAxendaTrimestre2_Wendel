using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Secciones
{
    /// <summary>
    /// Lógica de interacción para PaginaNotas.xaml
    /// </summary>
    public partial class PaginaNotas : Page
    {
        private DataAcceso _dataAccess = AppData.DataAccess;
        private Contacto _contactoActual;

        public PaginaNotas()
        {
            InitializeComponent();

            Nota notaPrueba = new Nota();
            notaPrueba.Titulo = "Nota de prueba";
            notaPrueba.Descripcion = "Contenido de prueba";

            if (_dataAccess != null)
            {
                AsignarListaADataGrid();
            }

            AsignarListaADataGrid();

        }

        public PaginaNotas(Contacto c)
        {
            InitializeComponent();
            _contactoActual = c;
            if(_contactoActual != null)
            {
                AsignarListaEventosActualADataGrid();
            }
        }

        private void AsignarListaEventosActualADataGrid()
        {
            // Obtener los eventos asociados al contacto actual
            List<Nota> listaEventos = _contactoActual.Notas;
            dataGrid.ItemsSource = listaEventos;
        }

        private async void AsignarListaADataGrid()
        {
            //var lista = DataAccess.DbContext.Notas.ToListAsync();
            
            //List<Nota> listaNotas = lista;
            //dataGrid.ItemsSource = listaNotas;
        }

        public async Task<List<Nota>> ObtenerListaNotasAsync(int contactoId)
        {
            // Obtener el contacto por su ID
            Contacto contacto = await _dataAccess.DbContext.Contactos
                                        // Incluir la lista de notas del contacto
                                        .Include(c => c.Notas)
                                        .FirstOrDefaultAsync(c => c.Id == contactoId);

            if (contacto != null)
            {
                // Devolver la lista de notas del contacto
                return contacto.Notas;
            }
            else
            {
                // Si el contacto no se encuentra, devolver una lista vacía
                return new List<Nota>();
            }
        }



        private void NavegarAtras(object sender, RoutedEventArgs e)
        {
            if (NavigationService.CanGoBack)
            {
                NavigationService.GoBack();
            }
        }
    }
}
