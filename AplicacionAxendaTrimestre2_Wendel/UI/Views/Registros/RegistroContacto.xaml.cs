using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Navigation;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using Org.BouncyCastle.Utilities;
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

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Registros
{
    /// <summary>
    /// Lógica de interacción para RegistroContacto.xaml
    /// </summary>
    public partial class RegistroContacto : Page
    {

        private DataAcceso _dataAccess = AppData.DataAccess;
        public RegistroContacto()
        {
            InitializeComponent();
        }

        private void RegistroNuevoContacto(object sender, RoutedEventArgs e)
        {
            var nombre = textboxNombre.Text;
            var apellidos = textBoxApellidos.Text;
            var edad = textboxEdad.Text;

            if (nombre != null && apellidos!=null && edad!=null &&
                nombre != "" && apellidos != "" && edad!= "")
            {
                Contacto c = new Contacto(nombre,apellidos,ConvertirAEntero(edad));
                AgregarContactoAsync(c);
            }
        }

        public static int ConvertirAEntero(string texto)
        {
            int numero;

            if (int.TryParse(texto, out numero))
            {
                return numero;
            }
            else
            {
                Console.WriteLine("La cadena no es un número válido.");
                return 0;
            }
        }

        public async Task AgregarContactoAsync(Contacto p)
        {
            await _dataAccess.DbContext.Contactos.AddAsync(p);
            GuardarCambios();
        }

        private async void GuardarCambios()
        {
            await _dataAccess.DbContext.SaveChangesAsync();
        }

        private void VolverAtras(object sender, RoutedEventArgs e)
        {
            Navegacion.NavegarAtras(NavigationService);
        }

        private void BorrarDatosIntroducidos(object sender, RoutedEventArgs e)
        {
           textboxNombre.Text = "";
            textBoxApellidos.Text = "";
            textboxEdad.Text = "";
        }
    }
}
