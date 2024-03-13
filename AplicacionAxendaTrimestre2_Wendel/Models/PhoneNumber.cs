﻿using AplicacionAxendaTrimestre2_Wendel.POJO;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionAxendaTrimestre2_Wendel.Models
{
    public class PhoneNumber
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Number { get; set; }

        public int ContactoId { get; set; }

        [ForeignKey("ContactoId")]
        public Contacto Contacto { get; set; }

        public PhoneNumber() { }

        public PhoneNumber(string number)
        {
            Number = number;
        }

        public PhoneNumber(string number, int contactoId)
        {
            Number = number;
            ContactoId = contactoId;
        }

        public PhoneNumber(string number, Contacto contacto)
        {
            Number = number;
            Contacto = contacto;
        }
    }
}