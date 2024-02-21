using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
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
        private DataAccess _dataAccess = null;

        public PaginaContactos()
        {
            InitializeComponent();

            _dataAccess = AppData.DataAccess;

            Contacto prueba = new Contacto();
            prueba.FirstName = "pepe";
            prueba.LastName = "dominguez";
            prueba.Age = 30;

            if (_dataAccess != null)
            {
                //MessageBox.Show("DataAccess no es null");
                _dataAccess.AgregarPersonaAsync(prueba);
                AsignarListaADataGrid();
            }
        }

        private void AsignarListaADataGrid()
        {
            // Obtener la lista de personas después de agregar
            List<Contacto> listaPersonas = _dataAccess.ObtenerPersonas();
            dataGrid.ItemsSource = listaPersonas;
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Application.Current.MainWindow.Visibility = Visibility.Visible;
        }

        private void ClickBtnAtras(object sender, RoutedEventArgs e)
        {
            //this.Close();
            //Application.Current.MainWindow.Visibility = Visibility.Visible;

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

