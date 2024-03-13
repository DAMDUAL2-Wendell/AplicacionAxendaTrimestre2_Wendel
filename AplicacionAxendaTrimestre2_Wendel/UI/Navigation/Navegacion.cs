using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Main;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Paginas;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Registros;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AplicacionAxendaTrimestre2_Wendel.UI.Navigation
{
    public static class Navegacion
    {


        public static void NavegarPaginaRegistro2(NavigationService navigationService) {
            // Obtener la ventana principal
            VistaPrincipalxaml? ventanaPrincipal = Application.Current.MainWindow as VistaPrincipalxaml;

            if (ventanaPrincipal != null)
            {
                // Acceder al Frame desde la ventana principal
                Frame frameContenido = ventanaPrincipal.frameContenido;

                if (frameContenido != null)
                {
                    // Crear una instancia de la otra página
                    RegistroContacto otraPagina = new RegistroContacto();

                    // Navegar a la otra página en el Frame
                    frameContenido.NavigationService.Navigate(otraPagina);
                }
            }
        }

        public static void FrameNavegacion()
        {
            // Obtener la ventana principal
            VistaPrincipalxaml? ventanaPrincipal = Application.Current.MainWindow as VistaPrincipalxaml;

            if (ventanaPrincipal != null)
            {
                // Acceder al Frame desde la ventana principal
                Frame frameNavegacion = ventanaPrincipal.frameNavegacion;

                if (frameNavegacion != null)
                {
                    // Crear una instancia de la otra página
                    PaginaPrincipal otraPagina = new PaginaPrincipal();

                    // Navegar a la otra página en el Frame
                    frameNavegacion.NavigationService.Navigate(otraPagina);
                }
            }
        }

        public static void NavegarPaginaRegistro(NavigationService navigationService)
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
                    RegContacto otraPagina = new RegContacto();

                    // Navegar a la otra página en el Frame
                    frameContenido.NavigationService.Navigate(otraPagina);
                }
            }
        }

        public static void NavegarPaginaContactos(NavigationService navigationService)
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

        public static void NavegarPaginaHome(NavigationService navigationService)
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
                    PagPrincipalRelojYEventos otraPagina = new PagPrincipalRelojYEventos();

                    // Navegar a la otra página en el Frame
                    frameContenido.NavigationService.Navigate(otraPagina);
                }
            }
        }

        public static void NavegarPaginaEdicion(NavigationService navigationService, Contacto contacto)
        {
            // Obtener la ventana principal
            VistaPrincipalxaml? ventanaPrincipal = Application.Current.MainWindow as VistaPrincipalxaml;

            if (ventanaPrincipal != null)
            {
                // Acceder al Frame desde la ventana principal
                Frame frameContenido = ventanaPrincipal.frameContenido;

                if (frameContenido != null)
                {
                    // Crear una instancia de la página de registro
                    RegContacto paginaEdicion = new RegContacto(contacto);


                    // Navegar a la página de registro (edición) en el Frame
                    frameContenido.NavigationService.Navigate(paginaEdicion);
                }
            }
        }



        public static void NavegarAtras(NavigationService navigationService)
        {
            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }



    }
}
