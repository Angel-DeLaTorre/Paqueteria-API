using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Estados;

public interface IEstadoServicio
{
    Task<Resultado<EstadoResponseDto>> ObtenerPorIdAsync(Guid id);
    Task<Resultado<IEnumerable<EstadoResponseDto>>> ObtenerTodosAsync();
}