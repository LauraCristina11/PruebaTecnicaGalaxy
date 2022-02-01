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
    public class TrabajadorController : ControllerBase
    {
        private readonly ITrabajadoresServicio _trabajadoresServicio;

        public TrabajadorController(ITrabajadoresServicio  trabajadoresServicio)
        {
            _trabajadoresServicio = trabajadoresServicio;
        }

        [HttpGet]
        public async Task<IReadOnlyList<TrabajadorDto>> ObtenerTrabajador()
        {
           return await _trabajadoresServicio.ObtenerTrabajadores();
        }

        [HttpDelete]
        public async Task<IReadOnlyList<RespuestasDto>>  EliminarTrabajador(int id)
        {
            return await _trabajadoresServicio.EliminarTrabajador(id);
        }

        [HttpPost]
        public async Task<IReadOnlyList<RespuestasDto>> RegistrarTrabajador(RegistrarTrabajador registrarTrabajador)
        {
            return await _trabajadoresServicio.RegistrarTrabajador(registrarTrabajador);
        }

        [HttpPut]
        public async Task<IReadOnlyList<RespuestasDto>> EditarTrabajador(EditarTrabajador editarTrabajador)
        {
            return await _trabajadoresServicio.EditarTrabajador(editarTrabajador);
        }

      
    }
}
