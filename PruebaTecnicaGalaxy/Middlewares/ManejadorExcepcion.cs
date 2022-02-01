using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PruebaTecnicaGalaxy.Middlewares
{
    public class ManejadorExcepcion:Exception
    {
        public HttpStatusCode _codigo { get; }
        public object _errores { get; set; }

        public ManejadorExcepcion(HttpStatusCode codigo, object errores = null)
        {
            _codigo = codigo;
            _errores = errores;

        }
    }
}
