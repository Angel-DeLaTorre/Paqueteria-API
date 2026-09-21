using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Sucursales;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface ISucursalServicio
{
    Task<Respuesta<List<SucursalRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<SucursalRespuestaDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<SucursalRespuestaDto>> CrearAsync(SucursalCrearDto dto);
    Task<Respuesta> ActualizarAsync(Guid id, SucursalActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid id);
}