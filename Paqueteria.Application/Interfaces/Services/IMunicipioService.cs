using Paqueteria.Application.DTOs;

namespace Paqueteria.Application.Interfaces.Services;

public interface IMunicipioService
{
    Task<MunicipioDto?> ObtenerMunicipioAsync(Guid id);
    Task<IEnumerable<MunicipioDto>> ObtenerMunicipiosPorEstadoAsync(string estado);
}