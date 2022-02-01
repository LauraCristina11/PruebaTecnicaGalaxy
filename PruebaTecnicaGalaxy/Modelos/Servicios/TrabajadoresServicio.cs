#region Using
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
#endregion

namespace PruebaTecnicaGalaxy.Modelos.Servicios
{
    public class TrabajadoresServicio : ITrabajadoresServicio
    {
        #region Propiedades
        private readonly AplicationDbContext _context;
        private readonly IMapper _mapper;
        #endregion

        #region Constructor
        public TrabajadoresServicio(AplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        #endregion

        #region Servicios

        #region Editar
        public async Task<IReadOnlyList<RespuestasDto>> EditarTrabajador(EditarTrabajador trabajador)
        {   
            try
            {
                #region Validar trabajador existe
                if (!_context.Trabajadores.Any(t => t.Id == trabajador.Id))
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Este trabajador no existe " });
                }
                if (!_context.TipoIdentidad.Any(t => t.Id == trabajador.TipoIdentidadId))
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Este tipo de identificación no existe " });
                }
                #endregion

                Trabajador trabajador1 = new Trabajador()
                {
                    Id= trabajador.Id,
                    TipoIdentidadId = trabajador.TipoIdentidadId,
                    NumeroDocumento = trabajador.NumeroDocumento,
                    PrimerNombre = trabajador.PrimerNombre,
                    SegundoNombre = trabajador.SegundoApellido,
                    PrimerApellido = trabajador.PrimerApellido,
                    SegundoApellido = trabajador.SegundoApellido,
                    FechaNacimiento = trabajador.FechaNacimiento,
                    Edad = DateTime.Now.Year - trabajador.FechaNacimiento.Year
                };
                
                #region Calcular edad
                if (DateTime.Now.Month < trabajador1.FechaNacimiento.Month)
                {
                    --trabajador1.Edad;
                }
                if (DateTime.Now.Month == trabajador1.FechaNacimiento.Month && DateTime.Now.Day < trabajador1.FechaNacimiento.Day)
                {
                    --trabajador1.Edad;
                }
                if (DateTime.Now.Month == trabajador1.FechaNacimiento.Month && DateTime.Now.Day == trabajador1.FechaNacimiento.Day)
                {
                    trabajador1.Edad = trabajador1.Edad;
                }
                #endregion
                
                _context.Update(trabajador1);
                await _context.SaveChangesAsync();

                RespuestasDto respuesta = new RespuestasDto()
                {
                    Codigo = 200,
                    Mensaje = "El trabajador se editó correctamente"
                };
                List<RespuestasDto> respuestaLista = new List<RespuestasDto>();
                respuestaLista.Add(respuesta);
                return respuestaLista;

            }
            catch (Exception ex)
            {
                throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Ha ocurrido un error y el trabajador no pudo ser editado. " + ex.Message });
            }
        }
        #endregion

        #region Eliminar
        public async Task<IReadOnlyList<RespuestasDto>> EliminarTrabajador(int id)
        {
            #region Validar tabajador a eliminar existe
            var trabajador = await _context.Trabajadores.Where(t=>t.Id==id).AsNoTracking().ToListAsync();
            if (trabajador.Count == 0) throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "No se encontro el trabajador seleccionado." });
            #endregion

            try
            {
                _context.Trabajadores.Remove(trabajador.FirstOrDefault());
                await _context.SaveChangesAsync();

                RespuestasDto respuesta = new RespuestasDto()
                {
                    Codigo = 200,
                    Mensaje = "El cliente se elimino correctamente"
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
        #endregion

        #region Listar 
        public async Task<IReadOnlyList<TrabajadorDto>> ObtenerTrabajadores()
        {
            var trabajadores = await _context.Trabajadores.Include(I=>I.tipoIdentidad).AsNoTracking().ToListAsync();
            var trabajadoresDTO = _mapper.Map<List<Trabajador>, List<TrabajadorDto>>(trabajadores);
            return trabajadoresDTO;
        }
        #endregion

        #region Registrar
        public async Task<IReadOnlyList<RespuestasDto>> RegistrarTrabajador(RegistrarTrabajador trabajador)
        {
            try
            {
                if(trabajador==null)
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Ha ocurrido un error, No hay información del trabajador ." });
                }
               
                if (!_context.TipoIdentidad.Any(t => t.Id == trabajador.TipoIdentidadId))
                {
                    throw new ManejadorExcepcion(HttpStatusCode.NotFound, new { mensaje = "Este tipo de identificación no existe " });
                }
               

                Trabajador trabajador1 = new Trabajador()
                {
                    TipoIdentidadId= trabajador.TipoIdentidadId,
                    NumeroDocumento=trabajador.NumeroDocumento,
                    PrimerNombre=trabajador.PrimerNombre,
                    SegundoNombre=trabajador.SegundoApellido,
                    PrimerApellido=trabajador.PrimerApellido,
                    SegundoApellido=trabajador.SegundoApellido,
                    FechaNacimiento=trabajador.FechaNacimiento,
                    Edad= DateTime.Now.Year - trabajador.FechaNacimiento.Year
                 
                };

               

                if (DateTime.Now.Month < trabajador1.FechaNacimiento.Month )
                {

                    --trabajador1.Edad;
                }
                if (DateTime.Now.Month == trabajador1.FechaNacimiento.Month && DateTime.Now.Day<trabajador1.FechaNacimiento.Day)
                {
                    --trabajador1.Edad;
                }

                if (DateTime.Now.Month == trabajador1.FechaNacimiento.Month && DateTime.Now.Day == trabajador1.FechaNacimiento.Day)
                {
                    trabajador1.Edad = trabajador1.Edad;
                }

                _context.Add(trabajador1);
                await _context.SaveChangesAsync();

                RespuestasDto respuesta = new RespuestasDto()
                {
                    Codigo = 200,
                    Mensaje = "El trabajador se guardo correctamente"
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

        #endregion
    }

}
