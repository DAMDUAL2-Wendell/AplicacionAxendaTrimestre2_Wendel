using AplicacionAxendaTrimestre2_Wendel.POJO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace AplicacionAxendaTrimestre2_Wendel.Models
{
    public class Email
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }

        public int ContactoId { get; set; }

        [ForeignKey("ContactoId")]
        public Contacto Contacto { get; set; }

        public Email() { }

        public Email(string address)
        {
            Address = address;
        }

        public Email(string address, int contactoId)
        {
            Address = address;
            ContactoId = contactoId;
        }

        public Email(string address, Contacto contacto)
        {
            Address = address;
            Contacto = contacto;
        }
    }
}
