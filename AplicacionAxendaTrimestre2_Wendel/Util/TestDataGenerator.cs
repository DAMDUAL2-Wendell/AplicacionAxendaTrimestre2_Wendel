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

                // Generar entre 1 y 2 direcciones de correo electrónico
                int numEmails = rand.Next(1, 3);
                List<Email> emails = GenerateEmails(numEmails);

                contactos.Add(new Contacto
                {
                    //Id = i + 1,
                    FirstName = nombre,
                    LastName = apellido,
                    Nickname = "Nickname" + (i + 1),
                    Address = "Dirección" + (i + 1),
                    Note = "Nota" + (i + 1),
                    Age = rand.Next(18, 60),
                    BirthDate = DateTime.Now.AddYears(-rand.Next(18, 60)),
                    ContactType = "Tipo" + (i + 1),
                    Numbers = new List<PhoneNumber>
            {
                new PhoneNumber { Number = "123456789" + i },
                new PhoneNumber { Number = "987654321" + i }
            },
                    Emails = emails // Asignar las direcciones de correo electrónico generadas
                });
            }
            return contactos;
        }


        public static List<PhoneNumber> GenerateTelefonos(int cantidad)
        {
            List<PhoneNumber> telefonos = new List<PhoneNumber>();
            Random rand = new Random();

            for (int i = 0; i < cantidad; i++)
            {
                // Generar un número aleatorio de 9 dígitos
                string numero = rand.Next(100000000, 999999999).ToString();
                telefonos.Add(new PhoneNumber { Number = numero });
            }

            return telefonos;
        }

        public static List<Email> GenerateEmails(int cantidad)
        {
            List<Email> emails = new List<Email>();
            Random rand = new Random();

            for (int i = 0; i < cantidad; i++)
            {
                // Generar una dirección de correo electrónico aleatoria
                string address = $"user{i + 1}@example.com";
                emails.Add(new Email { Address = address });
            }

            return emails;
        }


    }
}
    

