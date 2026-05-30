using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IMunicipioService
{
    Task<Result<IReadOnlyList<MunicipioResponseDto>>> GetAll();
    Task<Result<MunicipioResponseDto>> ObtenerMunicipioAsync(Guid id);
    Task<Result<IReadOnlyList<MunicipioResponseDto>>> ObtenerMunicipiosPorEstadoAsync(string estado);
}