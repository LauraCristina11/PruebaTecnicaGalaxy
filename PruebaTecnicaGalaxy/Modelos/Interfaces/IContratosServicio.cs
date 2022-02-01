using PruebaTecnicaGalaxy.Modelos.Dtos;
using PruebaTecnicaGalaxy.Modelos.ParametrosEntrada;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaGalaxy.Modelos.Interfaces
{
    public interface IContratosServicio
    {
        Task<IReadOnlyList<ContratoDto>> ObtenerContratos();
        Task<IReadOnlyList<RespuestasDto>> EditarContrato(EditarContrato contrato);
        Task<IReadOnlyList<RespuestasDto>> RegistrarContrato(RegistrarContrato contrato);
        Task<IReadOnlyList<RespuestasDto>> EliminarContrato(int id);

    }
}
