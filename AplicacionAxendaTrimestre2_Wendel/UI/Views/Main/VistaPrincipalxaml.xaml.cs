﻿using AplicacionAxendaTrimestre2_Wendel.bbdd;
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
using System.Diagnostics;
using System.IO;
using SQLitePCL;




namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Main
{
    /// <summary>
    /// Lógica de interacción para VistaPrincipalxaml.xaml
    /// </summary>
    public partial class VistaPrincipalxaml : MetroWindow
    {

        private readonly List<string> baseThemes = new List<string> { "Dark", "Light" };

        // Manuales
        private static String MANUAL_USUARIO_PDF = @"Manuales\ManualUsuario.pdf";
        private static String MANUAL_USUARIO_CHM = @"Manuales\ManualUsuario.chm";
        private static String MANUAL_INSTALACION_PDF = @"Manuales\ManualInstalacion.pdf";
        private static String MANUAL_INSTALACION_CHM = @"Manuales\ManualInstalacion.chm";


        // Rutas ejecutables programas
        private static String PROGRAMA_INTERNET_EXPLORER = @"C:\Program Files\Internet Explorer\iexplore.exe";
        private static String PROGRAMA_ABRIR_AYUDA = @"hh.exe";


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

    

        // Método para abrir el manual en la ruta especificada
        private void AbrirManual(string ruta)
        {
            try
            {
                Process.Start(ruta);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al abrir el manual: " + ex.Message);
            }
        }

        // Método para abrir la ayuda
        private void HelpClickChm(object sender, RoutedEventArgs e)
        {
            string helpFileName = bingPathToAppDir(@"Recursos\Help\help.chm");
            try
            {
                if (System.IO.File.Exists(helpFileName))
                {
                    //Help.ShowHelp(null, helpFileName);
                }
                else
                {
                    System.Windows.MessageBox.Show("Fichero de ayuda no encontrado");
                }
            }
            catch (Exception ex) { System.Windows.MessageBox.Show("Error al abrir la ayuda."); };
        }

       


        // ------     MANUALES    --------------------


       

        private void AbrirManualInstalacion_ClickPDF(object sender, RoutedEventArgs e)
        {
            AbrirFichero(PROGRAMA_INTERNET_EXPLORER,MANUAL_INSTALACION_PDF);
        }

        private void AbrirManualUsuario_ClickPDF(object sender, RoutedEventArgs e)
        {
            AbrirFichero(PROGRAMA_INTERNET_EXPLORER,MANUAL_USUARIO_PDF);
        }

        // Método para abrir el Manual de Instalación
        private void AbrirManualInstalacion_ClickCHM(object sender, RoutedEventArgs e)
        {
            AbrirFichero(PROGRAMA_ABRIR_AYUDA,MANUAL_INSTALACION_CHM);
        }

        // Método para abrir el Manual de Usuario
        private void AbrirManualUsuario_ClickCHM(object sender, RoutedEventArgs e)
        {
            AbrirFichero(PROGRAMA_ABRIR_AYUDA,MANUAL_USUARIO_CHM);
        }

        // Método para manejar el evento KeyDown (tecla F1)
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);
            if (e.Key == Key.F1)
            {
                AbrirFichero(PROGRAMA_ABRIR_AYUDA, MANUAL_USUARIO_CHM);
            }
            if (e.Key == Key.F2)
            {
                AbrirFichero(PROGRAMA_ABRIR_AYUDA, MANUAL_INSTALACION_CHM);
            }
            if (e.Key == Key.F3)
            {
                AbrirFichero(PROGRAMA_INTERNET_EXPLORER, MANUAL_USUARIO_PDF);
            }
            if (e.Key == Key.F4)
            {
                AbrirFichero(PROGRAMA_INTERNET_EXPLORER, MANUAL_INSTALACION_PDF);
            }
        }


        // Método para abrir un documento PDF de ayuda con el programa Internet Explorer.
        private void AbrirFichero(String ejecutable, String rutaFichero)
        {
            // Coger la ruta raiz del proyecto y agregar la ruta del fichero.
            string helpFileName = bingPathToAppDir(rutaFichero);
            try
            {
                if (System.IO.File.Exists(helpFileName))
                {
                    Process.Start(ejecutable, helpFileName);
                }
                else
                {
                    System.Windows.MessageBox.Show("Fichero no encontrado");
                }
            }
            catch (Exception ex) { System.Windows.MessageBox.Show("Error al abrir el fichero." + ex.Message); };
        }

        // Método que devuelve la ruta donde está el proyecto + la ruta que se le pasa como parámetro.
        public static string bingPathToAppDir(string localPath)
        {
            string currentDir = Environment.CurrentDirectory;
            DirectoryInfo directory = new DirectoryInfo(
                System.IO.Path.GetFullPath(System.IO.Path.Combine(currentDir, @"..\..\..\" + localPath)));
            return directory.ToString();
        }


    }
}
