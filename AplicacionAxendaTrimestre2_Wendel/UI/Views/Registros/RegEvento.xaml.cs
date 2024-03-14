using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Navigation;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using Microsoft.EntityFrameworkCore;
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
    /// Lógica de interacción para RegEvento.xaml
    /// </summary>
    public partial class RegEvento : Page
    {
        private Contacto _contacto;
        private DataAcceso _dataAccess = AppData.DataAccess;
        public RegEvento()
        {
            InitializeComponent();
            _contacto = null;
        }

        public RegEvento(Contacto c)
        {
            InitializeComponent();
            _contacto = c;
            lblContacto.Content = "Datos del contacto:\n Id: " + _contacto.Id + ", Nombre: " + _contacto.FirstName + ", Apellidos: " + _contacto.LastName;
            //MessageBox.Show("Entrando en reg evento con contacto: " + c.FirstName);
        }

        private async void RegistrarEvento_Click(object sender, RoutedEventArgs e)
        {
            // Validar que los campos obligatorios no estén vacíos
            if (string.IsNullOrWhiteSpace(tb_Titulo.Text) ||
                string.IsNullOrWhiteSpace(tb_Descripcion.Text) ||
                dp_FechaInicio.SelectedDate == null)
            {
                MessageBox.Show("Los campos Título, Descripción y Fecha de inicio son obligatorios.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
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
            DateTime fechaInicio = dp_FechaInicio.SelectedDate.Value;
            DateTime? fechaFin = dp_FechaFin.SelectedDate;

            // Crear un nuevo evento con los datos del formulario
            Evento nuevoEvento = new Evento()
            {
                Titulo = titulo,
                Descripcion = descripcion,
                FechaInicio = fechaInicio,
                FechaFin = fechaFin ?? fechaInicio, // Si la fecha fin es null, se asigna la fecha inicio
                ContactoId = _contacto.Id // Establecer el ID del contacto asociado al evento
            };

            // Agregar el evento al contacto
            _contacto.Eventos.Add(nuevoEvento);

            // Agregar el evento a la lista de eventosTotales
            _dataAccess.DbContext.AgregarEvento(nuevoEvento);

            // Guardar los cambios en la base de datos
            await _dataAccess.DbContext.SaveChangesAsync();

            // Mostrar mensaje de éxito
            MessageBox.Show("Evento registrado correctamente.", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);

            // Limpiar los campos del formulario
            LimpiarCampos();

            Navegacion.NavegarAtras(NavigationService);
        }



        private void LimpiarCampos()
        {
            tb_Titulo.Clear();
            tb_Descripcion.Clear();
            dp_FechaInicio.SelectedDate = null;
            dp_FechaFin.SelectedDate = null;
            //tb_SearchContact.Clear();
            _contacto = null;
            lblContacto.Content = "Datos del Contacto no disponibles";
        }

        private void Cancelar_Click(object sender, RoutedEventArgs e)
        {
            LimpiarCampos();
            NavigationService.GoBack();
        }

        private void Tb_SearchContact_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
