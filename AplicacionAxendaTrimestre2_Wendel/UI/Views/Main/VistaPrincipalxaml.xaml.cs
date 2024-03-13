using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.UI.Navigation;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Paginas;
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
using ControlzEx.Theming;
using MahApps.Metro.Controls;

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Main
{
    /// <summary>
    /// Lógica de interacción para VistaPrincipalxaml.xaml
    /// </summary>
    public partial class VistaPrincipalxaml : MetroWindow
    {

        private readonly List<string> baseThemes = new List<string> { "Dark", "Light" };

        private readonly List<string> accentColors = new List<string>
            {
                "Red", "Green", "Blue", "Purple", "Orange", "Lime", "Emerald", "Teal", "Cyan",
                "Cobalt", "Indigo", "Violet", "Pink", "Magenta", "Crimson", "Amber", "Yellow",
                "Brown", "Olive", "Steel", "Mauve", "Taupe", "Sienna"
            };

        public VistaPrincipalxaml()
        {
            InitializeComponent();
            //MostrarPaginaPrincipal();

            Navegacion.FrameNavegacion();

            PagPrincipalRelojYEventos pag = new PagPrincipalRelojYEventos();
            frameContenido.NavigationService.Navigate(pag);


            // Suscribirse al evento Loaded para esperar hasta que la ventana esté completamente cargada
            Loaded += MainWindow_Loaded;

        }

        private void BaseThemeComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Aplicar los colores de acento correspondientes al tema base seleccionado
            ApplyAccentColors(baseThemeComboBox.SelectedItem as string);
        }

        private void MostrarPaginaPrincipal()
        {
            // Crear una instancia de PaginaPrincipal
            PaginaPrincipal paginaPrincipal = new PaginaPrincipal();

            // Navegar a PaginaPrincipal en el Frame
            frameContenido.NavigationService.Navigate(paginaPrincipal);
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

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            // Luego de cargar la ventana, se deben cargar los colores de acento correspondientes
            ApplyAccentColors(baseThemeComboBox.SelectedItem as string);
        }


        private void ApplyAccentColors(string baseTheme)
        {
            // Limpiar los items anteriores
            accentColorComboBox.Items.Clear();

            foreach (var color in accentColors)
            {
                accentColorComboBox.Items.Add(color);
            }

            // Seleccionar el primer color de 'accentColors' por defecto
            accentColorComboBox.SelectedIndex = 0;
        }

        private void ApplyTheme_Click(object sender, RoutedEventArgs e)
        {
            // Obtener el tema base y el color de acento seleccionados

            string baseTheme = null;

            // Recorrer los elementos del ComboBox baseThemeComboBox
            foreach (var item in baseThemeComboBox.Items)
            {
                // Verificar si el elemento actual está seleccionado
                if (item is ComboBoxItem comboBoxItem && comboBoxItem.IsSelected)
                {
                    // Obtener el contenido del ComboBoxItem seleccionado
                    baseTheme = comboBoxItem.Content as string;
                    break;
                }
            }


            var accentColor = accentColorComboBox.SelectedItem as string;

            //MessageBox.Show(baseTheme + ", " + accentColor);

            // Verificar que los valores no sean nulos antes de llamar al método ChangeTheme
            if (baseTheme != null && accentColor != null)
            {
                // Aplicar el tema a la aplicación
                ThemeManager.Current.ChangeTheme(this, $"{baseTheme}.{accentColor}");
            }
            else
            {
                MessageBox.Show("Seleccione un tema y un color de acento antes de aplicar el tema.");
            }
        }




    }
}
