using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IEstadoService
{
    Task<Result<EstadoResponseDto>> GetEstadoByIdAsync(Guid id);
    Task<Result<IEnumerable<EstadoResponseDto>>> GetEstadosAsync();
}