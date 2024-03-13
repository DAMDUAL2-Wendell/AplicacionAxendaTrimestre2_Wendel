using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AplicacionAxendaTrimestre2_Wendel.Models;
using System.Security.RightsManagement;

namespace AplicacionAxendaTrimestre2_Wendel.POJO
{
    public class Contacto
    {
        // Marcar el Id como clave primaria y que sea autoincremental
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string FirstName { get; set; } = "";
        public string LastName { get; set; } = "";

        [NotMapped] // No mapear esta propiedad a la base de datos
        public string Nickname { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string Note { get; set; } = "";
        public int Age { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ContactType { get; set; } = "";

        // Lista de números de teléfono
        public List<PhoneNumber> Numbers { get; set; } = new List<PhoneNumber>();

        // Lista de notas
        public List<Nota> Notas { get; set; } = new List<Nota>();

        // Lista de eventos
        public List<Evento> Eventos { get; set; } = new List<Evento>();

        public Contacto() { }

        public Contacto(string nombre, string apellidos)
        {
            this.FirstName = nombre;
            this.LastName = apellidos;
            this.Age = 18;
            this.Numbers = new List<PhoneNumber>();
            this.ContactType = "";
        }


        public Contacto(string nombre, string apellidos, int edad)
        {
            this.FirstName = nombre;
            this.LastName = apellidos;
            this.Age = edad;
            this.Numbers = new List<PhoneNumber>();
            this.ContactType = "";
        }

        public Contacto(string nombre, string apellidos, int edad, List<PhoneNumber> numbers, string contactType)
        {
            this.FirstName = nombre;
            this.LastName = apellidos;
            this.Age = edad;
            this.Numbers = numbers ?? new List<PhoneNumber>();
            this.ContactType = contactType;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Age} años";
        }

    }
}
