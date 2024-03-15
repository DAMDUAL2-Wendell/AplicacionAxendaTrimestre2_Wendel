using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.Models;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using AplicacionAxendaTrimestre2_Wendel.UI.Views.Shared;
using AplicacionAxendaTrimestre2_Wendel.Util;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace AplicacionAxendaTrimestre2_Wendel.DataAccess
{
    public class ContactoManager
    {
        private DataAcceso _dataAccess = AppData.DataAccess;

        public void InsertarTelefonosEnContactos(List<Contacto> contactos, List<PhoneNumber> telefonos)
        {
            Random random = new Random();

            // Verificar si _dataAccess y _dataAccess.DbContext están inicializados
            if (_dataAccess == null || _dataAccess.DbContext == null)
            {
                return; // Salir del método si _dataAccess no está inicializado correctamente
            }

            foreach (var contacto in contactos)
            {
                // Verificar si quedan números de teléfono disponibles
                if (telefonos.Any())
                {
                    // Obtener un número de teléfono aleatorio de la lista
                    int indexTelefonoAleatorio = random.Next(0, telefonos.Count);
                    PhoneNumber telefonoAleatorio = telefonos[indexTelefonoAleatorio];

                    // Asignar el número de teléfono al contacto
                    contacto.Numbers.Add(telefonoAleatorio);

                    // Asignar el Id del contacto al número de teléfono
                    telefonoAleatorio.ContactoId = contacto.Id;

                    // Eliminar el número de teléfono de la lista para evitar que se reutilice en otro contacto
                    telefonos.RemoveAt(indexTelefonoAleatorio);
                }
                else
                {
                    // Si no quedan números de teléfono disponibles, salir del bucle
                    break;
                }
            }

            // Guardar los cambios en la base de datos
            _dataAccess.DbContext.SaveChanges();
        }

        public async Task AgregarEventosAContactosAsync()
        {
            // Obtener un número aleatorio entre 1 y 5 para seleccionar el contacto
            Random rnd = new Random();
            int contactoId = rnd.Next(1, 6); // Selecciona un número aleatorio entre 1 y 5 inclusive

            // Crear los eventos
            Evento evento1 = new Evento("peluqueria", "ir al peluquero", DateTime.Now, DateTime.Now.AddDays(1));
            Evento evento2 = new Evento("compras", "ir a comprar", DateTime.Now.AddDays(5), DateTime.Now.AddDays(6));

            // Buscar el contacto por su Id
            var contacto = await _dataAccess.DbContext.Contactos.FindAsync(contactoId);

            // Verificar si se encontró el contacto
            if (contacto != null)
            {
                // Agregar los eventos al contacto
                contacto.Eventos.Add(evento1);
                contacto.Eventos.Add(evento2);

                _dataAccess.DbContext.AgregarEvento(evento1);
                _dataAccess.DbContext.AgregarEvento(evento2);

                // Guardar los cambios en la base de datos
                await _dataAccess.DbContext.SaveChangesAsync();

                MessageBox.Show("Eventos guardados correctamente en el contacto con id: " + contactoId);
            }
            else
            {
                // Manejar el caso donde no se encuentra el contacto
                Console.WriteLine("No se encontró el contacto con el Id especificado.");
            }

        }

        public void InsertarDatosAleatorios(object sender, RoutedEventArgs e)
        {
            try
            {
                List<Contacto> contactos = TestDataGenerator.GenerateContactos(5);
                InsertarContactosPrueba(contactos);
                //List<PhoneNumber> telefonos = TestDataGenerator.GenerateTelefonos(2);
                //InsertarTelefonosEnContactos(contactos, telefonos);
            }
            catch (Exception ex) { }
        }

        // Método para insertar contactos de prueba
        public void InsertarContactosPrueba(List<Contacto> contactosPrueba)
        {
            // Verificar si _dataAccess y _dataAccess.DbContext están inicializados
            if (_dataAccess != null && _dataAccess.DbContext != null)
            {
                // Agregar los contactos de prueba y guardar los cambios en la base de datos
                _dataAccess.DbContext.Contactos.AddRange(contactosPrueba);
                _dataAccess.DbContext.SaveChanges();
            }
        }

        public void InsertarTelefonosEnContactos2(List<Contacto> contactos, List<PhoneNumber> telefonos)
        {
            Random random = new Random();

            // Verificar si _dataAccess y _dataAccess.DbContext están inicializados
            if (_dataAccess == null || _dataAccess.DbContext == null)
            {
                return; // Salir del método si _dataAccess no está inicializado correctamente
            }

            foreach (var contacto in contactos)
            {
                // Verificar si quedan números de teléfono disponibles
                if (telefonos.Any())
                {
                    // Obtener un número de teléfono aleatorio de la lista
                    int indexTelefonoAleatorio = random.Next(0, telefonos.Count);
                    PhoneNumber telefonoAleatorio = telefonos[indexTelefonoAleatorio];

                    // Asignar el número de teléfono al contacto
                    contacto.Numbers.Add(telefonoAleatorio);

                    // Asignar el Id del contacto al número de teléfono
                    telefonoAleatorio.ContactoId = contacto.Id;

                    // Eliminar el número de teléfono de la lista para evitar que se reutilice en otro contacto
                    telefonos.RemoveAt(indexTelefonoAleatorio);
                }
                else
                {
                    // Si no quedan números de teléfono disponibles, salir del bucle
                    break;
                }
            }

            // Guardar los cambios en la base de datos
            _dataAccess.DbContext.SaveChanges();
        }


    }
}
