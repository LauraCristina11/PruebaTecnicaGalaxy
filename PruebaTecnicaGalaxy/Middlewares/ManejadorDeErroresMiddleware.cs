using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PruebaTecnicaGalaxy.Middlewares
{
    public class ManejadorDeErroresMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ManejadorDeErroresMiddleware> _logger;

        public ManejadorDeErroresMiddleware(RequestDelegate next, ILogger<ManejadorDeErroresMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception e)
            {
                await ManejadorExcepcionAsync(context, e);
            }
        }
        private async Task ManejadorExcepcionAsync(HttpContext context, Exception e)
        {
            object errores = null;
            switch (e)
            {
                case ManejadorExcepcion me:
                    errores = me._errores;
                    context.Response.StatusCode = (int)me._codigo;
                    break;

                case Exception ex:
                    
                    errores = string.IsNullOrWhiteSpace(e.Message) ? "Error" : e.Message;
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.ContentType = "application/json";

            if (errores != null)
            {
                var resultados = JsonConvert.SerializeObject(new { errores });
                await context.Response.WriteAsync(resultados);
            }
        }
    }
}
