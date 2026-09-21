using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Asignaciones;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface IAsignacionServicio
{
    Task<Respuesta<List<AsignacionRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<AsignacionRespuestaDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<AsignacionRespuestaDto>> CrearAsync(AsignacionCrearDto dto);
    Task<Respuesta> CambiarEstadoAsync(int id, int estadoId);
    
    Task<Respuesta> GenerarReporteSalidaAsync(Guid asignacionId);
}