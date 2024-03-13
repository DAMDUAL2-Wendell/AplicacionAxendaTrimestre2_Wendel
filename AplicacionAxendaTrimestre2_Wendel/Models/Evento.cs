using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AplicacionAxendaTrimestre2_Wendel.POJO
{
    public class Evento
    {


        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Titulo { get; set; }

        [Required]
        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        // Propiedad de navegación para establecer la relación con Contacto
        public int ContactoId { get; set; }

        // Referencia de navegación a la entidad Contacto
        [ForeignKey("ContactoId")]
        public Contacto Contacto { get; set; }

        public Evento(string titulo, string descripcion, DateTime fechaInicio, DateTime fechaFin)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }

        public Evento()
        {
        }
    }
}
