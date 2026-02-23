using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public sealed class MunicipioService(IMunicipioRepository repository) : IMunicipioService
{
    public async Task<Result<MunicipioDto>> ObtenerMunicipioAsync(Guid id)
    {
        var municipio = await repository.ObtenerMunicipioAsync(id);

        if (municipio == null)
            return Result<MunicipioDto>.Failure(CodigoRespuesta.NotFound, "Municipio no encontrado");



        return Result<MunicipioDto>.Success( MunicipioDto.FromEntity(municipio) );
    }

    public async Task<Result<IEnumerable<MunicipioDto>>> ObtenerMunicipiosPorEstadoAsync(string estadoId)
    {
        var municipios = (await repository.ObtenerMunicipiosPorEstadoAsync(estadoId)).Select(MunicipioDto.FromEntity).ToList();

        if (municipios.Count == 0)
            return Result<IEnumerable<MunicipioDto>>.Failure(CodigoRespuesta.NotFound, "No existe ningun municipio");

        return Result<IEnumerable<MunicipioDto>>.Success( municipios);
    }
}