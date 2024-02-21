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

        public Contacto() { }
 

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }

        public Contacto(string nombre, string apellidos, int edad)
        {
            this.FirstName = nombre;
            this.LastName = apellidos;
            this.Age = edad;
        }

        public override string ToString()
        {
            return $"{FirstName} {LastName}, {Age} años";
        }

    }
}
