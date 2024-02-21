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
    /// Lógica de interacción para Notas.xaml
    /// </summary>
    public partial class Notas : Window
    {
        public Notas()
        {
            InitializeComponent();
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
