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

namespace AplicacionRelojWendel
{
    /// <summary>
    /// Lógica de interacción para VentanaAlarma.xaml
    /// </summary>
    public partial class VentanaAlarma : Window
    {

        // Mensaje de la alarma
        private String mensajeAlarma;

        // Indica si la ventana de alarma está abierta o cerrada
        private Boolean ventanaAbierta = false;

        // Evento que se dispara cuando se cierra la ventana de alarma
        public event EventHandler AlarmaVentanaCerrada;



        // Constructor que recibe un mensaje para la alarma
        public VentanaAlarma(String mensaje)
        {
            // Inicializa la ventana de alarma y establece el mensaje
            InitializeComponent();
            labelAlarma.Content = mensaje;
        }


        // Propiedad para acceder o modificar el mensaje de la alarma
        public string MensajeAlarma { get => mensajeAlarma; set => mensajeAlarma = value; }



        // Propiedad para verificar si la ventana de alarma está abierta
        public bool VentanaAbierta { get => ventanaAbierta; set => ventanaAbierta = value; }



        // Maneja el evento de clic en el botón de cerrar la ventana
        private void btnCerrar_Click(object sender, RoutedEventArgs e)
        {
            // Marca la ventana como cerrada
            this.ventanaAbierta = false;

            // Dispara el evento de cierre de ventana
            AlarmaVentanaCerrada?.Invoke(this, EventArgs.Empty);

            // Cierra la ventana
            this.Close();
        }


    }
}
