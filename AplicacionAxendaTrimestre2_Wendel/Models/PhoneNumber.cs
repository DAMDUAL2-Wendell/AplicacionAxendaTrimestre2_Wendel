using AplicacionAxendaTrimestre2_Wendel.POJO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionAxendaTrimestre2_Wendel.Models
{
    public class PhoneNumber
    {
        public int Id { get; set; }
        public string Number { get; set; }

        [ForeignKey("Contacto")]
        public int ContactId { get; set; }

        public PhoneNumber() { }

        public PhoneNumber(string number, int contactId)
        {
            Number = number;
            ContactId = contactId;
        }
    }

}
