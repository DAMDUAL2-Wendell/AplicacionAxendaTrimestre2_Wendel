using AplicacionAxendaTrimestre2_Wendel.bbdd;
using AplicacionAxendaTrimestre2_Wendel.Models;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionAxendaTrimestre2_Wendel.DataAccess
{
    public class ContactoManager
    {

        public void InsertarTelefonosEnContactos(List<Contacto> contactos, DataAcceso _dataAccess, List<PhoneNumber> telefonos)
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
