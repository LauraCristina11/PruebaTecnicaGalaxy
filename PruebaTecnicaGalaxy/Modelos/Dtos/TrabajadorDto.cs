using PruebaTecnicaGalaxy.Modelos.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaGalaxy.Modelos.Dtos
{
    public class TrabajadorDto
    {
        public int Id { get; set; }
        public int TipoIdentidadId { get; set; }
        public virtual TipoIdentidad tipoIdentidad { get; set; }
        public string NumeroDocumento { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public DateTime FechaNacimiento { get; set; }
        public int Edad { get; set; }

        

    }
}
