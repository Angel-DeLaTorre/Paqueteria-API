using Paqueteria.Application.Modulos.Estados.Dtos;
using Paqueteria.Dominio.Comun;

namespace Paqueteria.Application.Modulos.Estados;

public interface IEstadoServicio
{
    Task<Respuesta<EstadoRespuestaDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<IEnumerable<EstadoRespuestaDto>>> ObtenerTodosAsync();
}