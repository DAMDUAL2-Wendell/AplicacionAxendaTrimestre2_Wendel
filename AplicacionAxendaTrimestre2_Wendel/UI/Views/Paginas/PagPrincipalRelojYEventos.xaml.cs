﻿using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using AplicacionAxendaTrimestre2_Wendel.Util;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AplicacionAxendaTrimestre2_Wendel.UI.Views.Paginas
{
    /// <summary>
    /// Lógica de interacción para PagPrincipalRelojYEventos.xaml
    /// </summary>
    public partial class PagPrincipalRelojYEventos : Page
    {
        private DataAccess _dataAccess = AppData.DataAccess;
        public List<Evento> eventos = new List<Evento>();
        public PagPrincipalRelojYEventos()
        {
            InitializeComponent();

            _dataAccess.DbContext.Eventos.Add(new Evento("peluqueria", "ir al peluquero", DateTime.Now, DateTime.Now.AddDays(1)));
            _dataAccess.DbContext.Eventos.Add(new Evento("compras", "ir a comprar", DateTime.Now.AddDays(5), DateTime.Now.AddDays(6)));
            _dataAccess.DbContext.SaveChanges();
           // _dataAccess.DbContext.SaveChangesAsync();

            eventos = _dataAccess.DbContext.Eventos.ToList();

            dataGrid.ItemsSource = eventos;

            // Suscribirse al evento de selección de fecha del calendario
            calendar.SelectedDatesChanged += Calendar_SelectedDatesChanged;
            // Suscribirse al evento de cambio de rango de fechas del calendario
            calendar.DisplayDateChanged += Calendar_DisplayDateChanged;

            // Suscribirse al evento Loaded de los botones de día del calendario
            calendar.Loaded += Calendar_Loaded;
        }

        private void Calendar_Loaded(object sender, RoutedEventArgs e)
        {
            // Buscar los botones de día en la jerarquía visual del calendario
            var dayButtons = Utilidades.FindVisualChildren<CalendarDayButton>(calendar);
            foreach (var dayButton in dayButtons)
            {
                // Obtener la fecha representada por el botón
                DateTime fecha = (DateTime)dayButton.DataContext;
                // Verificar si hay eventos para esta fecha
                bool hayEventos = eventos.Any(ev => ev.FechaInicio.Date == fecha.Date);
                // Cambiar el color de fondo del botón si hay eventos
                if (hayEventos)
                {
                    dayButton.Background = Brushes.LightBlue;
                }
            }
        }


        private void Calendar_SelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            // Obtener el rango de fechas seleccionado
            IEnumerable<DateTime> selectedDates = calendar.SelectedDates;

            // Filtrar los eventos que caen dentro del rango de fechas seleccionado
            List<Evento> eventosFiltrados = eventos.Where(ev => selectedDates.Any(d => d.Date == ev.FechaInicio.Date || d.Date == ev.FechaFin.Date)).ToList();

            // Actualizar el DataGrid con los eventos filtrados
            dataGrid.ItemsSource = eventosFiltrados;
        }

        private void Calendar_DisplayDateChanged(object sender, CalendarDateChangedEventArgs e)
        {
            // Obtener el rango de fechas visible en el calendario
            DateTime startDate = e.RemovedDate ?? DateTime.MinValue;
            DateTime endDate = e.AddedDate ?? DateTime.MinValue;
            // Realizar la lógica para el rango de fechas seleccionado
            // Esto podría incluir actualizaciones en la interfaz de usuario u otras acciones necesarias
            // Aquí puedes realizar acciones basadas en el rango de fechas seleccionado
        }
    }
}
