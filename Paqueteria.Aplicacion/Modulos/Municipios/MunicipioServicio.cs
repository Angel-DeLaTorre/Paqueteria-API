using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Application.Modulos.Municipios;

public sealed class MunicipioServicio(IUnitOfWork unit) : IMunicipioServicio
{
    public async Task<Resultado<IReadOnlyList<MunicipioResponseDto>>> ObtenerTodosAsync()
    {
        var municipiosEntityList = await unit.Municipios.ObtenerTodosAsync();
        var municipios = municipiosEntityList.Select(MunicipioResponseDto.FromEntity).ToList();
        
        if (municipios.Count == 0)
            return Resultado<IReadOnlyList<MunicipioResponseDto>>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<IReadOnlyList<MunicipioResponseDto>>.Exitoso(municipios);
    }
    public async Task<Resultado<MunicipioResponseDto>> ObtenerPorIdAsync(Guid id)
    {
        var municipio = await unit.Municipios.GetByIdAsync(id);

        if (municipio == null)
            return Resultado<MunicipioResponseDto>.Error(CodigosError.Generic.NoEncontrado);



        return Resultado<MunicipioResponseDto>.Exitoso( MunicipioResponseDto.FromEntity(municipio) );
    }

    public async Task<Resultado<IReadOnlyList<MunicipioResponseDto>>> ObtenerPorEstadoAsync(string estadoId)
    {
        var municipios = (await unit.Municipios.ObtenerTodosPorEstadoAsync(estadoId)).Select(MunicipioResponseDto.FromEntity).ToList();

        return Resultado<IReadOnlyList<MunicipioResponseDto>>.Exitoso( municipios);
    }
}