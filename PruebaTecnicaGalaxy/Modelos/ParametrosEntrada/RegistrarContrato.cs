using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaGalaxy.Modelos.ParametrosEntrada
{
    public class RegistrarContrato
    {
        public string NombreEntidad { get; set; }
        public string NumeroContrato { get; set; }
        public string TrabajadorId { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFinalizacion { get; set; }
    }
}
