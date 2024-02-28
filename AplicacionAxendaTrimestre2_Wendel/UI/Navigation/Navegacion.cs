using AplicacionAxendaTrimestre2_Wendel.UI.Views.Main;
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

        public static void NavegarAtras(NavigationService navigationService)
        {
            if (navigationService.CanGoBack)
            {
                navigationService.GoBack();
            }
        }



    }
}
