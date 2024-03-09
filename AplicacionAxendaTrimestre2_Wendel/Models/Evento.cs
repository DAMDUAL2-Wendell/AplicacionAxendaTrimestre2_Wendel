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

        public string Titulo { get; set; }

        public string Descripcion { get; set; }

        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        public Evento(string titulo, string descripcion, DateTime fechaCreacion, DateTime fechaActivacion)
        {
            Titulo = titulo;
            Descripcion = descripcion;
            FechaInicio = fechaCreacion;
            FechaFin = fechaActivacion;
        }

        public Evento()
        {
        }


    }
}
