using AplicacionAxendaTrimestre2_Wendel.bbdd;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace AplicacionAxendaTrimestre2_Wendel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        public DataAccess _dataAccess;

        private string BDMEMORIA = "DataSource=:memory:;Cache=shared";
        private string RUTAFICHERO = "..\\..\\..\\bbdd\\MiBaseDeDatosLocal.db";
        private string BDFICHERO = "DataSource=";
        private string BDMYSQL = "Server=localhost:3306;Database=myDataBase;Uid=root;Pwd=;";

        private string STRINGCONEXION = "";
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ClickBtnContactos(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Visibility = Visibility.Hidden;
            Window contactos = new Contactos(_dataAccess);
            contactos.Show();
        }

        private void ClickBtnNotas(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Visibility = Visibility.Hidden;
            Window notas = new Notas();
            notas.Show();
        }

        private void ClickBtnEventos(object sender, RoutedEventArgs e)
        {
            Application.Current.MainWindow.Visibility = Visibility.Hidden;
            Window eventos = new Eventos();
            eventos.Show();
        }

        private void ClickBtnConectar(object sender, RoutedEventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    STRINGCONEXION = BDFICHERO + RUTAFICHERO;
                    if (System.IO.File.Exists(RUTAFICHERO))
                    {
                        MessageBox.Show("Se ha cargado el fichero con éxito.");
                    }
                    else
                    {
                        MessageBox.Show("Creando fichero de base de datos");
                    }
                    break;
                case 1:
                    STRINGCONEXION = BDMEMORIA;
                    MessageBox.Show("Se ha cargado la base de datos en memoria.");
                    break;
                case 2:
                    STRINGCONEXION = BDMYSQL;
                    MessageBox.Show("Se ha cargado la base de datos desde servidor.");
                    break;
            }
            _dataAccess = new DataAccess(STRINGCONEXION);
        }
    }
}
