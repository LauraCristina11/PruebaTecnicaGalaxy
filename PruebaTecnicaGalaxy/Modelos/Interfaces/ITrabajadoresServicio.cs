using PruebaTecnicaGalaxy.Modelos.Dtos;
using PruebaTecnicaGalaxy.Modelos.ParametrosEntrada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaGalaxy.Modelos.Interfaces
{
    public interface ITrabajadoresServicio
    {
        Task<IReadOnlyList<TrabajadorDto>> ObtenerTrabajadores();
        Task<IReadOnlyList<RespuestasDto>> RegistrarTrabajador(RegistrarTrabajador trabajador);
        Task<IReadOnlyList<RespuestasDto>> EditarTrabajador(EditarTrabajador trabajador);
        Task<IReadOnlyList<RespuestasDto>> EliminarTrabajador(int id);
    }
}
