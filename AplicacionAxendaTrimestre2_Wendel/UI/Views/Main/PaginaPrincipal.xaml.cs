using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.DataAccess;
using AplicacionAxendaTrimestre2_Wendel.Models;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Navigation;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Paginas;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using AplicacionAxendaTrimestre2_Wendel.Util;
using Microsoft.EntityFrameworkCore;
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

        ContactoManager contactoManager = null;
        public PaginaPrincipal()
        {
            InitializeComponent();

            // Conectar con base de datos por defecto 'En Memoria' ya que estoy haciendo pruebas y así es más rápido.
            ConectarConBaseDatos(1);

            // Seleccionar comboBox 1 (Base de datos en memoria)
            comboBox.SelectedIndex = 1;

            contactoManager = new ContactoManager();

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
                    // Utilizar la URL de conexión, usuario, contraseña y nombre de base de datos configurados, si están disponibles
                    string urlConexion = Configuracion.Configuracion.UrlConexion ?? "default_url_connection";
                    string usuario = Configuracion.Configuracion.Usuario ?? "default_user";
                    string contraseña = Configuracion.Configuracion.Contraseña ?? "default_password";
                    string nombreBD = Configuracion.Configuracion.NombreBD ?? "default_database_name";

                    // Si los datos de conexión han sido cambiados y todos los datos necesarios están disponibles, se utiliza la nueva cadena de conexión
                    if (Configuracion.Configuracion.DatosConexionCambiados && !string.IsNullOrEmpty(urlConexion) && !string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(nombreBD))
                    {
                        STRINGCONEXION = $"Persist Security Info=False;User ID={usuario};Password={contraseña};Initial Catalog={nombreBD};Server={urlConexion}";
                    }
                    else
                    {
                        // Usar la cadena de conexión predeterminada si los datos de configuración están incompletos
                        STRINGCONEXION = Configuracion.Configuracion.BDMYSQL;
                    }
                    // MessageBox.Show("Se ha cargado la base de datos desde servidor.");
                    break;


            }
            AppData.DataAccess = new DataAcceso(STRINGCONEXION);
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
                        MessageBox.Show("Creando fichero de base de datos en la ruta: ");
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
            try
            {
                AppData.DataAccess = new DataAcceso(STRINGCONEXION);
                MessageBox.Show("Conexión al servidor establecida correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al conectar con el servidor: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void ClickMostrarPaginaHome(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarPaginaHome(NavigationService);
        }

        private void ClickInsertarContactosAleatorios(object sender, RoutedEventArgs e)
        {
            contactoManager.InsertarDatosAleatorios(sender, e);

            // Actualizar página de contactos
            MostrarPaginaContactos(sender, e);
        }

        private async void ClickInsertarEventosAleatorios(object sender, RoutedEventArgs e)
        {
            await contactoManager.AgregarEventosAContactosAsync();
        }


        private async void ClickBorrarDatosBD(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("¿Está seguro de querer borrar todos los datos de la base de datos?", "Confirmación", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    // Eliminar todos los contactos de la base de datos
                    var contactos = await AppData.DataAccess.DbContext.Contactos.ToListAsync();
                    AppData.DataAccess.DbContext.Contactos.RemoveRange(contactos);
                    await AppData.DataAccess.DbContext.SaveChangesAsync();

                    MessageBox.Show("Se han borrado todos los datos de la base de datos correctamente.", "Información", MessageBoxButton.OK, MessageBoxImage.Information);

                    // Actualizar página de contactos
                    MostrarPaginaContactos(sender, e);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ocurrió un error al intentar borrar los datos de la base de datos: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void ClickOpcionesConexionBBDD(object sender, RoutedEventArgs e)
        {
            // Crear una instancia de la ventana de configuración
            VentanaConfiguracionBBDD ventanaConfiguracionBBDD = new VentanaConfiguracionBBDD();

            // Mostrar la ventana de configuración
            ventanaConfiguracionBBDD.ShowDialog();
        }



    }
}
