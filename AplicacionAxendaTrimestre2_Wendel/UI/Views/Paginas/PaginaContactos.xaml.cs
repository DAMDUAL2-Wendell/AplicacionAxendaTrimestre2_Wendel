using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Navigation;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Paginas;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Registros;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
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

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Main
{
    /// <summary>
    /// Lógica de interacción para PaginaContactos.xaml
    /// </summary>
    public partial class PaginaContactos : Page
    {
        private DataAcceso _dataAccess = AppData.DataAccess;

        public PaginaContactos()
        {
            InitializeComponent();

            // Cargar El DataGrid de Contactos de forma Asíncrona
            Loaded += PaginaContactos_Loaded;
            
        }

        private async void PaginaContactos_Loaded(object sender, RoutedEventArgs e)
        {
            if (_dataAccess != null)
            {
                await AgregarContactoAsync(GetContactoPrueba());
                AsignarListaADataGrid();
            }
        }


        public Contacto GetContactoPrueba()
        {
            Contacto prueba = new Contacto();
            prueba.FirstName = "pepe";
            prueba.LastName = "dominguez";
            prueba.Age = 30;
            return prueba;
        }

        private async void GuardarCambios()
        {
            await _dataAccess.DbContext.SaveChangesAsync();
        }

        private async void AsignarListaADataGrid()
        {
            // Obtener la lista de personas después de agregar
            List<Contacto> listaPersonas = await ObtenerListaContactosAsync();
            dataGrid.ItemsSource = listaPersonas;
        }

        public async Task<List<Contacto>> ObtenerListaContactosAsync()
        {
            return await _dataAccess.DbContext.Contactos.ToListAsync();
        }

        public async Task AgregarContactoAsync(Contacto p)
        {
            await _dataAccess.DbContext.Contactos.AddAsync(p);
            GuardarCambios();
        }


        // NAVEGACION

        private void MostrarPaginaRegistro(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarPaginaRegistro(NavigationService);
        }

        private void NavegarAtras(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarAtras(NavigationService);
        }


    }
}

