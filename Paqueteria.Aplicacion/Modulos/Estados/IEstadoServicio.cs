using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Estados.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Estados;

public interface IEstadoServicio
{
    Task<Respuesta<EstadoResponseDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<IEnumerable<EstadoResponseDto>>> ObtenerTodosAsync();
}