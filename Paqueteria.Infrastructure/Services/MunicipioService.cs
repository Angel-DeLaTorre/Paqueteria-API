using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Infrastructure.Services;

public sealed class MunicipioService(IUnitOfWork unit) : IMunicipioService
{
    public async Task<Result<IReadOnlyList<MunicipioResponseDto>>> GetAll()
    {
        var municipiosEntityList = await unit.Municipios.ObtenerMunicipiosAsync();
        var municipios = municipiosEntityList.Select(MunicipioResponseDto.FromEntity).ToList();
        
        if (municipios.Count == 0)
            return Result<IReadOnlyList<MunicipioResponseDto>>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<IReadOnlyList<MunicipioResponseDto>>.Success(municipios);
    }
    public async Task<Result<MunicipioResponseDto>> ObtenerMunicipioAsync(Guid id)
    {
        var municipio = await unit.Municipios.GetByIdAsync(id);

        if (municipio == null)
            return Result<MunicipioResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);



        return Result<MunicipioResponseDto>.Success( MunicipioResponseDto.FromEntity(municipio) );
    }

    public async Task<Result<IReadOnlyList<MunicipioResponseDto>>> ObtenerMunicipiosPorEstadoAsync(string estadoId)
    {
        var municipios = (await unit.Municipios.ObtenerMunicipiosPorEstadoAsync(estadoId)).Select(MunicipioResponseDto.FromEntity).ToList();

        return Result<IReadOnlyList<MunicipioResponseDto>>.Success( municipios);
    }
}