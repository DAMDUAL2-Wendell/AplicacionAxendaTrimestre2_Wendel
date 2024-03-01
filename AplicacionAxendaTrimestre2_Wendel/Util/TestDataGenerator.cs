using AplicacionAxendaTrimestre2_Wendel.Models;
using AplicacionAxendaTrimestre2_Wendel.POJO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionAxendaTrimestre2_Wendel.Util
{
    public static class TestDataGenerator
    {
        public static List<Contacto> GenerateContactos(int cantidad)
        {
            List<Contacto> contactos = new List<Contacto>();
            Random rand = new Random();

            // Nombres y apellidos aleatorios para los contactos
            string[] nombres = { "Juan", "María", "Pedro", "Ana", "Luis", "Sofía", "Carlos", "Laura", "José", "Isabel" };
            string[] apellidos = { "García", "Martínez", "López", "González", "Rodríguez", "Pérez", "Fernández", "Sánchez", "Romero", "Álvarez" };

            for (int i = 0; i < cantidad; i++)
            {
                string nombre = nombres[rand.Next(nombres.Length)];
                string apellido = apellidos[rand.Next(apellidos.Length)];

                contactos.Add(new Contacto
                {
                    Id = i + 1,
                    FirstName = nombre,
                    LastName = apellido,
                    Nickname = "Nickname" + (i + 1),
                    Email = $"contacto{i + 1}@example.com",
                    Address = "Dirección" + (i + 1),
                    Note = "Nota" + (i + 1),
                    Age = rand.Next(18, 60),
                    BirthDate = DateTime.Now.AddYears(-rand.Next(18, 60)),
                    ContactType = "Tipo" + (i + 1),
                    Numbers = new List<PhoneNumber>
                    {
                        new PhoneNumber { Number = "123456789" + i },
                        new PhoneNumber { Number = "987654321" + i }
                    }
                });
            }

            return contactos;
        }
    }
}
    

