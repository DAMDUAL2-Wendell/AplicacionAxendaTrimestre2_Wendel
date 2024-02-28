using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public string FullName { get; set; } = "";
        public string Nickname { get; set; } = "";
        public string Email { get; set; } = "";
        public string Address { get; set; } = "";
        public string Note { get; set; } = "";
        public int Age { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ContactType { get; set; } = "";
        public List<string> Numbers { get; set; } = new List<string>();

        public Contacto() { }


        public Contacto(string nombre, string apellidos, int edad)
        {
            this.FirstName = nombre;
            this.LastName = apellidos;
            this.Age = edad;
            this.Numbers = new List<string>();
            this.ContactType = "";
            this.FullName = nombre + " " + apellidos;
        }

        public Contacto(string nombre, string apellidos, int edad, List<string> numbers, string contactType)
        {
            this.FirstName = nombre;
            this.LastName = apellidos;
            this.Age = edad;
            this.Numbers = numbers ?? new List<string>();
            this.ContactType = contactType;
            this.FullName = nombre + " " + apellidos;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Age} años";
        }

    }
}
