using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Roles;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface IRolServicio
{
    Task<Respuesta<List<RolRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<RolRespuestaDto>> ObtenerPorIdAsync(Guid rolId);
    Task<Respuesta<RolRespuestaDto>> CrearAsync(RolCrearDto dto);
    Task<Respuesta> ActualizarAsync(Guid rolId, RolActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid rolId);
    Task<Respuesta> ActivarAsync(Guid rolId);
    Task<Respuesta> DesactivarAsync(Guid rolId);
    
}