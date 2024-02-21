using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
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
using System.Windows.Shapes;

namespace AplicacionAxendaTrimestre2_Wendel
{
    /// <summary>
    /// Lógica de interacción para Contactos.xaml
    /// </summary>
    public partial class Contactos : Window
    {

        private DataAccess _dataAccess;

        public Contactos(DataAccess dataAccess)
        {
            InitializeComponent();

            _dataAccess = dataAccess;

            Contacto prueba = new Contacto();
            prueba.FirstName = "pepe";
            prueba.LastName = "dominguez";
            prueba.Age = 30;

            if(_dataAccess != null)
            {
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
            this.Close();
            Application.Current.MainWindow.Visibility = Visibility.Visible;
        }

    }
}
