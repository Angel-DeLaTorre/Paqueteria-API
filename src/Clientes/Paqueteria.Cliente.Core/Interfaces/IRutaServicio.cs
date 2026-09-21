using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Rutas;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface IRutaServicio
{
    Task<Respuesta<List<RutaRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<RutaRespuestaDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<RutaRespuestaDto>> CrearAsync(RutaCrearDto dto);
    Task<Respuesta> ActualizarAsync(Guid id, RutaActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid id);
}