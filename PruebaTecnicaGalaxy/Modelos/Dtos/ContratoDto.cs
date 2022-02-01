using PruebaTecnicaGalaxy.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaGalaxy.Modelos.Dtos
{
    public class ContratoDto
    {
        public int Id { get; set; }
        public string NombreEntidad { get; set; }
        public int NumeroContrato { get; set; }
        public int TrabajadorId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
        public virtual Trabajador trabajador { get; set; }
    }
}
