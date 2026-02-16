using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Infrastructure.Repositories;

namespace Paqueteria.Infrastructure.Services;

public sealed class MunicipioService(MunicipioRepository repository) : IMunicipioService
{
    public async Task<MunicipioDto?> ObtenerMunicipioAsync(Guid id)
    {
        var obj = await repository.ObtenerMunicipioAsync(id);
        if (obj != null)
            return new MunicipioDto(
                obj.Id,
                obj.Nombre,
                obj.EstadoId
            );
        return null;
    }

    public async Task<IEnumerable<MunicipioDto>> ObtenerMunicipiosPorEstadoAsync(string estadoId)
    {
        var municipios = await repository.ObtenerMunicipiosPorEstadoAsync(estadoId);

        return municipios.Select(m => new MunicipioDto
        (
            m.Id,
            m.Nombre,
            m.EstadoId
        ));
    }
}