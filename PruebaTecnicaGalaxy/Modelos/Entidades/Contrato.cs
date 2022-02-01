using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaGalaxy.Modelos.Entidades
{
    public class Contrato
    {
        [Key]
        public int Id { get; set; }
        public string NombreEntidad { get; set; }
        public int NumeroContrato { get; set; }
        public int TrabajadorId { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime FechaInicio { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime FechaFinalizacion { get; set; }
        public virtual Trabajador trabajador { get; set; }

    }
}
