using Paqueteria.Application.Dtos;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Municipios.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;

namespace Paqueteria.Application.Modulos.Municipios;

public sealed class MunicipioServicio(IUnitOfWork unit) : IMunicipioServicio
{
    public async Task<Respuesta<IReadOnlyList<MunicipioResponseDto>>> ObtenerTodosAsync()
    {
        var municipiosEntityList = await unit.Municipios.ObtenerTodosAsync();
        var municipios = municipiosEntityList.Select(MunicipioResponseDto.FromEntity).ToList();
        
        if (municipios.Count == 0)
            return Respuesta<IReadOnlyList<MunicipioResponseDto>>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<IReadOnlyList<MunicipioResponseDto>>.Exitoso(municipios);
    }
    public async Task<Respuesta<MunicipioResponseDto>> ObtenerPorIdAsync(Guid id)
    {
        var municipio = await unit.Municipios.GetByIdAsync(id);

        if (municipio == null)
            return Respuesta<MunicipioResponseDto>.Error(CodigosError.Generic.NoEncontrado);



        return Respuesta<MunicipioResponseDto>.Exitoso( MunicipioResponseDto.FromEntity(municipio) );
    }

    public async Task<Respuesta<IReadOnlyList<MunicipioResponseDto>>> ObtenerPorEstadoAsync(string estadoId)
    {
        var municipios = (await unit.Municipios.ObtenerTodosPorEstadoAsync(estadoId)).Select(MunicipioResponseDto.FromEntity).ToList();

        return Respuesta<IReadOnlyList<MunicipioResponseDto>>.Exitoso( municipios);
    }
}