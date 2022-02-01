using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PruebaTecnicaGalaxy.Modelos.Dtos;
using PruebaTecnicaGalaxy.Modelos.Interfaces;
using PruebaTecnicaGalaxy.Modelos.ParametrosEntrada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaGalaxy.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContratoController : ControllerBase
    {
        private readonly IContratosServicio _contratosServicio;

        public ContratoController(IContratosServicio contratosServicio)
        {
            _contratosServicio = contratosServicio;
        }

        [HttpPut]
        public async Task<IReadOnlyList<RespuestasDto>>EditarContrato(EditarContrato editarContrato)
        {
            return await _contratosServicio.EditarContrato(editarContrato);
        }

        [HttpPost]
        
        public async Task<IReadOnlyList<RespuestasDto>> RegistrarContrato(RegistrarContrato registrarContrato)
        {
            return await _contratosServicio.RegistrarContrato(registrarContrato);
        }
        [HttpGet]
        public async Task<IReadOnlyList<ContratoDto>> ObtenerContrato()
        {
            return await _contratosServicio.ObtenerContratos();
        }

        [HttpDelete]
        public async Task<IReadOnlyList<RespuestasDto>> EliminarContrato(int id)
        {
            return await _contratosServicio.EliminarContrato(id);
        }
    }
}
