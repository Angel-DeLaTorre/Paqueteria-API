using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Seguros;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface ISeguroServicio
{
    Task<Respuesta<List<SeguroRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<SeguroRespuestaDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<SeguroRespuestaDto>> CrearAsync(SeguroCrearDto dto);
    Task<Respuesta> ActualizarAsync(Guid seguroId, SeguroActualizarDto dto);
    Task<Respuesta> ActivarAsync(Guid id);
    Task<Respuesta> DesactivarAsync(Guid id);
    Task<Respuesta> EliminarAsync(Guid id);
}