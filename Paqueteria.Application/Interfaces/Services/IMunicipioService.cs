using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IMunicipioService
{
    Task<Result<MunicipioDto>> ObtenerMunicipioAsync(Guid id);
    Task<Result<IEnumerable<MunicipioDto>>> ObtenerMunicipiosPorEstadoAsync(string estado);
}