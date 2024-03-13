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
using System.Windows.Threading;

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Reloj
{
    /// <summary>
    /// Lógica de interacción para Reloj.xaml
    /// </summary>
    public partial class Reloj : UserControl
    {
        // Variables para ajustar la hora de la alarma.
        private int horaAlarma = 0;
        private int minutosAlarma = 0;

        // Variables para ajustar la hora del reloj.
        private int hora = 0;
        private int minutos = 0;
        private int segundos = 0;

        // Variable booleana para saber si la alarma esta activada.
        private Boolean alarmaActivada = false;

        // Variable booleana para saber si la ventana de notificación de la alarma esta abierta.
        private Boolean ventanaAbierta = false;


        // Variable para saber si el formato de la hora es 24h o 12h.
        private Boolean formato24h = true;

        // Mensaje predeterminado
        private string mensajeAlarma = "Alarma";

        // Booleano para controlar el inicio o parar el reloj.
        Boolean iniciar = true;


        /**
         * Alarma
         */
        public int HoraAlarma { get => horaAlarma; set => horaAlarma = value; }
        public int MinutosAlarma { get => minutosAlarma; set => minutosAlarma = value; }
        public bool AlarmaActivada { get => alarmaActivada; set => alarmaActivada = value; }


        /**
         * Hora
         */
        public int Hora { get => hora; set => hora = value; }
        public int Minutos { get => minutos; set => minutos = value; }
        public int Segundos { get => segundos; set => segundos = value; }

        /**
         * Mensaje
         */
        public string MensajeAlarma { get => mensajeAlarma; set => mensajeAlarma = value; }
        /**
         * Formato
         */
        public bool Formato24h { get => formato24h; set => formato24h = value; }

        public bool Iniciar { get => iniciar; set => iniciar = value; }
        public bool VentanaAbierta { get => ventanaAbierta; set => ventanaAbierta = value; }


        // Temporizador que actualiza el tiempo cada segundo
        DispatcherTimer timer = new DispatcherTimer();

        // Constructor de la clase
        public Reloj()
        {
            InitializeComponent();

            // Configuración del temporizador
            timer.Tick += new EventHandler(timer_Tick);
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Start();
            //labelCronometro.Content = hora;

        }

        // Método para iniciar o detener el temporizador
        public void clickIniciarParar()
        {
            Iniciar = !Iniciar;
            if (!Iniciar)
            {
                this.timer.Stop();
                labelCronometro.Background = Brushes.Gray;
            }
            else
            {
                this.timer = new DispatcherTimer();
                timer.Tick += new EventHandler(timer_Tick);
                timer.Interval = TimeSpan.FromSeconds(1);
                timer.Start();
                labelCronometro.Background = null;
            }
        }


        // Método que se ejecuta cada segundo
        private async void timer_Tick(object sender, EventArgs e)

        {
            // Verifica si es hora de activar la alarma
            verificaAlarma();

            // Incrementa el tiempo
            incrementarTiempo();

            // Actualiza la interfaz con la hora formateada
            labelCronometro.Content = getHoraFormateada();


            CommandManager.InvalidateRequerySuggested();

        }

        // Verifica si es hora de activar la alarma
        private void verificaAlarma()
        {
            if (!this.VentanaAbierta)
            {
                if (comprobarCoincidenciaHoraArlarma())
                {
                    // Muestra la ventana de alarma si es el momento exacto
                    //VentanaAlarma ventanaAlarma = new VentanaAlarma(this.mensajeAlarma);
                    //ventanaAlarma.AlarmaVentanaCerrada += VentanaAlarmaCerrada;
                    //ventanaAlarma.Show();
                    //this.VentanaAbierta = true;
                }
            }
        }

        // Se ejecuta cuando se cierra la ventana de alarma
        private void VentanaAlarmaCerrada(object sender, EventArgs e)
        {
            this.ventanaAbierta = false;
        }

        // Comprueba si es el momento exacto de activar la alarma
        private Boolean comprobarCoincidenciaHoraArlarma()
        {
            return (alarmaActivada == true && hora == horaAlarma && minutos == minutosAlarma && segundos == 0);
        }


        // Reinicia el temporizador y establece el tiempo a cero
        public void clickReiniciar(object sender, RoutedEventArgs e)
        {
            if (!Iniciar)
            {

                this.timer.Stop();
                this.hora = 0;
                this.minutos = 0;
                this.segundos = 0;

                labelCronometro.Content = String.Format("{0:00}:{1:00}:{2:00}", this.hora, this.minutos, this.segundos);
                colorTemporal(500);

            }
        }

        // Incrementa el tiempo
        private void incrementarTiempo()
        {
            if (this.Segundos < 59)
            {
                this.Segundos++;
            }
            else
            {
                this.Segundos = 0;

                if (this.Minutos < 59)
                {
                    this.Minutos++;
                }
                else
                {
                    this.Minutos = 0;

                    if (this.formato24h)
                    {
                        if (this.hora < 23)
                        {
                            this.hora++;
                        }
                        else
                        {
                            this.hora = 0;
                        }
                    }
                    else
                    {
                        // Si estamos en formato de 12 horas, manejamos la lógica correspondiente
                        if (this.hora < 11)
                        {
                            this.hora++;
                        }
                        else
                        {
                            this.hora = 0;
                        }
                    }
                }
            }
        }


        // Obtiene la hora formateada
        private string getHoraFormateada()
        {
            return String.Format("{0:00}:{1:00}:{2:00}" , this.hora, this.Minutos, this.Segundos);
        }

        // Cambia temporalmente el color de fondo
        private void colorTemporal(int milisegundos)
        {
            labelCronometro.Background = Brushes.Red;

            DispatcherTimer temporizador = new DispatcherTimer();
            temporizador.Interval = TimeSpan.FromMilliseconds(milisegundos);
            temporizador.Start();

            temporizador.Tick += (s, args) =>
            {
                labelCronometro.Background = null;
                temporizador.Stop();
            };
        }

    }
}
