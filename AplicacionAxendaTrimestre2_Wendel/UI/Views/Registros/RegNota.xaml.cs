using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Navigation;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Navigation;

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Registros
{
    /// <summary>
    /// Lógica de interacción para RegNota.xaml
    /// </summary>
    public partial class RegNota : Page
    {
        private Contacto _contacto;
        private DataAcceso _dataAccess = AppData.DataAccess;

        public RegNota()
        {
            InitializeComponent();
            _contacto = null;
        }

        public RegNota(Contacto c)
        {
            InitializeComponent();
            _contacto = c;
            lblContacto.Content = $"Datos del contacto:\n Id: {_contacto.Id}, Nombre: {_contacto.FirstName}, Apellidos: {_contacto.LastName}";
        }

        private async void RegistrarNota_Click(object sender, RoutedEventArgs e)
        {
            // Validar que los campos obligatorios no estén vacíos
            if (string.IsNullOrWhiteSpace(tb_Titulo.Text) || string.IsNullOrWhiteSpace(tb_Descripcion.Text))
            {
                MessageBox.Show("Los campos Título y Descripción son obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Validar que el contacto no sea null
            if (_contacto == null)
            {
                MessageBox.Show("Debe seleccionar un contacto.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            // Obtener los datos del formulario
            string titulo = tb_Titulo.Text;
            string descripcion = tb_Descripcion.Text;

            // Crear una nueva nota con los datos del formulario
            Nota nuevaNota = new Nota()
            {
                Titulo = titulo,
                Descripcion = descripcion,
                ContactoId = _contacto.Id // Establecer el ID del contacto asociado a la nota
            };

            // Agregar la nota al contacto
            _contacto.Notas.Add(nuevaNota);

            // Agregar la nota a la lista de notasTotales
            //_dataAccess.DbContext.AgregarNota(nuevaNota);

            // Guardar los cambios en la base de datos
            await _dataAccess.DbContext.SaveChangesAsync();

            // Mostrar mensaje de éxito
            MessageBox.Show("Nota registrada correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            // Limpiar los campos del formulario
            LimpiarCampos();

            // Navegar hacia atrás
            Navegacion.NavegarAtras(NavigationService);
        }

        private void LimpiarCampos()
        {
            tb_Titulo.Clear();
            tb_Descripcion.Clear();
            _contacto = null;
            lblContacto.Content = "Datos del Contacto no disponibles";
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            NavigationService.GoBack();
        }
    }
}
