using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IMunicipioService
{
    Task<Result<MunicipioResponseDto>> ObtenerMunicipioAsync(Guid id);
    Task<Result<IEnumerable<MunicipioResponseDto>>> ObtenerMunicipiosPorEstadoAsync(string estado);
}