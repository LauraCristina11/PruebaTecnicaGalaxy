using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PruebaTecnicaGalaxy.Middlewares;
using PruebaTecnicaGalaxy.Modelos.Dtos;
using PruebaTecnicaGalaxy.Modelos.Entidades;
using PruebaTecnicaGalaxy.Modelos.EntityFrameworkCore;
using PruebaTecnicaGalaxy.Modelos.Interfaces;
using PruebaTecnicaGalaxy.Modelos.ParametrosEntrada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace PruebaTecnicaGalaxy.Modelos.Servicios
{
    public class ContratosServicio : IContratosServicio
    {
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;

        public ContratosServicio(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #region Editar
        public async Task<IReadOnlyList<RespuestasDto>> EditarContrato(EditarContrato contrato)
        {
            try
            {

                #region Validar contrato existente
                if (!_context.Contratos.Any(c => c.Id == contrato.Id))
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Este trabajador no existe " });
                }

                var trabajadorExistente= _context.Trabajadores.Any(c => c.Id == int.Parse(contrato.TrabajadorId));
                if(!trabajadorExistente)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Este trabajdor que has seleccionado, no existe " });
                }

                
                #endregion
                Contrato contrato1 = new Contrato()
                {
                    Id = contrato.Id,
                    NombreEntidad = contrato.NombreEntidad,
                    NumeroContrato = int.Parse(contrato.NumeroContrato),
                    FechaInicio = contrato.FechaInicio,
                    FechaFinalizacion = contrato.FechaFinalizacion,
                    TrabajadorId = int.Parse(contrato.TrabajadorId)
                };

     

                _context.Update(contrato1);
                await _context.SaveChangesAsync();

                RespuestasDto respuesta = new RespuestasDto()
                {
                    Codigo = 200,
                    Mensaje = "El contrato se editó correctamente"
                };
                List<RespuestasDto> respuestaLista = new List<RespuestasDto>();
                respuestaLista.Add(respuesta);
                return respuestaLista;
            }
            catch (Exception ex)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Ha ocurrido un error, el contrato no pudo ser editado. " + ex.Message });
            }
        }
        #endregion

        public async Task<IReadOnlyList<RespuestasDto>> EliminarContrato(int id)
        {
            var contrato = await _context.Contratos.Where(i => i.Id == id).ToListAsync();
            if (contrato.Count<=0)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Este contrato no existe " });
            }
            try
            {
                _context.Contratos.Remove(contrato.FirstOrDefault());
                await _context.SaveChangesAsync();

                RespuestasDto respuesta = new RespuestasDto()
                {
                    Codigo = 200,
                    Mensaje = "El contrato se elimino correctamente"
                };

                List<RespuestasDto> respuestaLista = new List<RespuestasDto>();
                respuestaLista.Add(respuesta);

                return respuestaLista;
            }
            catch (Exception ex)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Ha ocurrido un error y el trabajador no pudo ser eliminado. " + ex.Message });
            }
        }

        public async Task<IReadOnlyList<ContratoDto>> ObtenerContratos()
        {
            var contrato = await _context.Contratos.Include(n =>n.trabajador).AsNoTracking().ToListAsync();
            var contratosDTO = _mapper.Map<List<Contrato>, List<ContratoDto>>(contrato);
            return contratosDTO;
        }

        #region Registrar 
        public async Task<IReadOnlyList<RespuestasDto>> RegistrarContrato(RegistrarContrato contrato)
        {

            if (contrato == null)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Ha ocurrido un error, No hay información ." });
            }

            if (!_context.Trabajadores.Any(c => c.Id == int.Parse(contrato.TrabajadorId)))
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Este trabajador que has seleccionado, no existe " });
            }
            try
            {

                Contrato contrato1 = new Contrato()
                {
                    NombreEntidad = contrato.NombreEntidad,
                    NumeroContrato = int.Parse(contrato.NumeroContrato),
                    FechaInicio = contrato.FechaInicio,
                    FechaFinalizacion = contrato.FechaFinalizacion,
                    TrabajadorId = int.Parse(contrato.TrabajadorId)
                };
                _context.Add(contrato1);
                await _context.SaveChangesAsync();

                RespuestasDto respuesta = new RespuestasDto()
                {
                    Codigo = 200,
                    Mensaje = "La información del contrato se guardo correctamente"
                };
                List<RespuestasDto> respuestaLista = new List<RespuestasDto>();
                respuestaLista.Add(respuesta);
                return respuestaLista;

            }
            catch (Exception ex)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Ha ocurrido un error, la informacion no se pudo registrar " + ex.Message });
            }
            



        }
        #endregion
    }
}
