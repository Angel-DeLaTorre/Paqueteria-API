using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Choferes;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface IChoferServicio
{
    Task<Respuesta<List<ChoferRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<ChoferRespuestaDto>> ObtenerPorIdAsync(int id);
    Task<Respuesta<ChoferRespuestaDto>> CrearAsync(ChoferCrearDto dto);
    Task<Respuesta> ActualizarAsync(Guid id, ChoferActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid id);
}