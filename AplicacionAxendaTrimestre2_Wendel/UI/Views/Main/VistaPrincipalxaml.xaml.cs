using AplicacionAxendaTrimestre2_Wendel.bbdd;
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

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Main
{
    /// <summary>
    /// Lógica de interacción para VistaPrincipalxaml.xaml
    /// </summary>
    public partial class VistaPrincipalxaml : Window
    {
        public VistaPrincipalxaml()
        {
            InitializeComponent();
            MostrarPaginaPrincipal();
        }

        private void MostrarPaginaPrincipal()
        {
            // Crear una instancia de PaginaPrincipal
            PaginaPrincipal paginaPrincipal = new PaginaPrincipal();

            // Navegar a PaginaPrincipal en el Frame
            frameContenido.NavigationService.Navigate(paginaPrincipal);
        }





    }
}
