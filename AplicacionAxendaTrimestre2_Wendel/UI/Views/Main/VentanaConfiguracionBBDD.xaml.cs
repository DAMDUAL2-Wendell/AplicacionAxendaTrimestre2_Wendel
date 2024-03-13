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
    /// Lógica de interacción para VentanaConfiguracionBBDD.xaml
    /// </summary>

    public partial class VentanaConfiguracionBBDD : Window
    {
        public VentanaConfiguracionBBDD()
        {
            InitializeComponent();
        }

        private void ClickAceptar(object sender, RoutedEventArgs e)
        {
            // Obtener los datos ingresados por el usuario
            string urlConexion = textboxUrlConexion.Text;
            string usuario = textboxUsuario.Text;
            string contraseña = passwordboxContraseña.Password;
            string nombreBD = textboxNombreBD.Text;

            // Verificar que los valores no sean nulos o vacíos
            if (!string.IsNullOrEmpty(urlConexion) && !string.IsNullOrEmpty(usuario) && !string.IsNullOrEmpty(nombreBD))
            {
                // Guardar los datos en la configuración
                Configuracion.Configuracion.UrlConexion = urlConexion;
                Configuracion.Configuracion.Usuario = usuario;
                Configuracion.Configuracion.Contraseña = contraseña;
                Configuracion.Configuracion.NombreBD = nombreBD;
                Configuracion.Configuracion.DatosConexionCambiados = true;

                // Cerrar la ventana
                Close();

                // Mostrar mensaje de éxito
                MessageBox.Show("La configuración se ha cambiado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ClickCancelar(object sender, RoutedEventArgs e)
        {
            // Cerrar la ventana sin guardar cambios
            Close();
        }


    }
}
