using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Paginas;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Secciones;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
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

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Main
{
    /// <summary>
    /// Lógica de interacción para PaginaPrincipal.xaml
    /// </summary>
    public partial class PaginaPrincipal : Page
    {
        public PaginaPrincipal()
        {
            InitializeComponent();

            // Conectar con base de datos por defecto 'En Memoria' ya que estoy haciendo pruebas y así es más rápido.
            ConectarConBaseDatos(1);

            // Seleccionar comboBox 1 (Base de datos en memoria)
            comboBox.SelectedIndex = 1;
        }

        //public DataAccess _dataAccess = AppData.dataAccess;


        private string STRINGCONEXION = "";

        private void MostrarPaginaContactos2(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana principal
            VistaPrincipalxaml? ventanaPrincipal = Application.Current.MainWindow as VistaPrincipalxaml;

            if (ventanaPrincipal != null)
            {
                // Acceder al Frame desde la ventana principal
                Frame frameContenido = ventanaPrincipal.frameContenido;

                if (frameContenido != null)
                {
                    // Crear una instancia de la otra página
                    PaginaContactos otraPagina = new PaginaContactos();

                    // Navegar a la otra página en el Frame
                    frameContenido.NavigationService.Navigate(otraPagina);
                }
            }
        }

        private void MostrarPaginaContactos(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana principal
            VistaPrincipalxaml? ventanaPrincipal = Application.Current.MainWindow as VistaPrincipalxaml;

            if (ventanaPrincipal != null)
            {
                // Acceder al Frame desde la ventana principal
                Frame frameContenido = ventanaPrincipal.frameContenido;

                if (frameContenido != null)
                {
                    // Crear una instancia de la otra página
                    PagContactos otraPagina = new PagContactos();

                    // Navegar a la otra página en el Frame
                    frameContenido.NavigationService.Navigate(otraPagina);
                }
            }
        }

        private void MostrarPaginaNotas(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana principal
            VistaPrincipalxaml? ventanaPrincipal = Application.Current.MainWindow as VistaPrincipalxaml;

            if (ventanaPrincipal != null)
            {
                // Acceder al Frame desde la ventana principal
                Frame frameContenido = ventanaPrincipal.frameContenido;

                if (frameContenido != null)
                {
                    // Crear una instancia de la otra página
                    PaginaNotas otraPagina = new PaginaNotas();

                    // Navegar a la otra página en el Frame
                    frameContenido.NavigationService.Navigate(otraPagina);
                }
            }
        }

        private void MostrarPaginaEventos(object sender, RoutedEventArgs e)
        {
            // Obtener la ventana principal
            VistaPrincipalxaml? ventanaPrincipal = Application.Current.MainWindow as VistaPrincipalxaml;

            if (ventanaPrincipal != null)
            {
                // Acceder al Frame desde la ventana principal
                Frame frameContenido = ventanaPrincipal.frameContenido;

                if (frameContenido != null)
                {
                    // Crear una instancia de la otra página
                    PaginaEventos otraPagina = new PaginaEventos();

                    // Navegar a la otra página en el Frame
                    frameContenido.NavigationService.Navigate(otraPagina);
                }
            }
        }

        private void ClickBtnConectar(object sender, RoutedEventArgs e)
        {
            ConectarConBaseDatos(comboBox.SelectedIndex);
        }

        private void ConectarConBaseDatos(int comboBoxIndex)
        {
            switch (comboBoxIndex)
            {
                case 0:


                    // Obtener la carpeta del directorio de la aplicación
                    string directorioAplicacion = AppDomain.CurrentDomain.BaseDirectory;
                    // Navegar hacia arriba en la jerarquía de directorios para encontrar el directorio principal del proyecto
                    DirectoryInfo directorioProyecto = Directory.GetParent(directorioAplicacion).Parent.Parent.Parent;

                    string filePath = System.IO.Path.Combine(directorioProyecto.FullName, Configuracion.Configuracion.RUTAFICHERO);
                    STRINGCONEXION = Configuracion.Configuracion.BDFICHERO + directorioProyecto.FullName + "\\" + Configuracion.Configuracion.RUTAFICHERO;

                    if (directorioProyecto != null)
                    {
                        string rutaDirectorioProyecto = directorioProyecto.FullName;
                        //MessageBox.Show($"El directorio principal del proyecto es: {rutaDirectorioProyecto}");
                    }
                    else
                    {
                        //MessageBox.Show("No se pudo encontrar el directorio principal del proyecto.");
                    }

                    if (File.Exists(filePath))
                    {
                        // MessageBox.Show("Se ha cargado el fichero con éxito.");
                    }
                    else
                    {
                        try
                        {
                            // Intenta crear el archivo
                            using (File.Create(filePath)) { }
                            // MessageBox.Show($"Se ha creado el fichero en la ruta: {filePath}");
                        }
                        catch (Exception ex)
                        {
                            // MessageBox.Show($"Error al crear el fichero: {ex.Message}");
                        }
                    }
                    break;
                case 1:
                    STRINGCONEXION = Configuracion.Configuracion.BDMEMORIA;
                    // MessageBox.Show("Se ha cargado la base de datos en memoria.");
                    break;
                case 2:
                    STRINGCONEXION = Configuracion.Configuracion.BDMYSQL;
                    // MessageBox.Show("Se ha cargado la base de datos desde servidor.");
                    break;
            }
            AppData.DataAccess = new DataAccess(STRINGCONEXION);
        }


        private void ClickBtnConectar2(object sender, RoutedEventArgs e)
        {
            switch (comboBox.SelectedIndex)
            {
                case 0:
                    STRINGCONEXION = Configuracion.Configuracion.BDFICHERO + Configuracion.Configuracion.RUTAFICHERO;
                    if (System.IO.File.Exists(Configuracion.Configuracion.RUTAFICHERO))
                    {
                        MessageBox.Show("Se ha cargado el fichero con éxito.");
                    }
                    else
                    {
                        MessageBox.Show("Creando fichero de base de datos en la ruta: " );
                    }
                    break;
                case 1:
                    STRINGCONEXION = Configuracion.Configuracion.BDMEMORIA;
                    MessageBox.Show("Se ha cargado la base de datos en memoria.");
                    break;
                case 2:
                    STRINGCONEXION = Configuracion.Configuracion.BDMYSQL;
                    MessageBox.Show("Se ha cargado la base de datos desde servidor.");
                    break;
            }
            AppData.DataAccess = new DataAccess(STRINGCONEXION);
        }
    }
}
