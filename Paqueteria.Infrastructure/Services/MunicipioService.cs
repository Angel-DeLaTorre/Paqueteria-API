using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public sealed class MunicipioService(IMunicipioRepository repository) : IMunicipioService
{
    public async Task<Result<MunicipioResponseDto>> ObtenerMunicipioAsync(Guid id)
    {
        var municipio = await repository.GetByIdAsync(id);

        if (municipio == null)
            return Result<MunicipioResponseDto>.Failure(CodigoRespuesta.NotFound, "Municipio no encontrado");



        return Result<MunicipioResponseDto>.Success( MunicipioResponseDto.FromEntity(municipio) );
    }

    public async Task<Result<IReadOnlyList<MunicipioResponseDto>>> ObtenerMunicipiosPorEstadoAsync(string estadoId)
    {
        var municipios = (await repository.ObtenerMunicipiosPorEstadoAsync(estadoId)).Select(MunicipioResponseDto.FromEntity).ToList();

        if (municipios.Count == 0)
            return Result<IReadOnlyList<MunicipioResponseDto>>.Failure(CodigoRespuesta.NotFound, "No existe ningun municipio");

        return Result<IReadOnlyList<MunicipioResponseDto>>.Success( municipios);
    }
}