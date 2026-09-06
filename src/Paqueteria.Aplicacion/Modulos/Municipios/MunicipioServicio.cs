using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Municipios.Dtos;
using Paqueteria.Dominio.Comun;
using Paqueteria.Dominio.Comun.Errors;

namespace Paqueteria.Application.Modulos.Municipios;

public sealed class MunicipioServicio(IUnitOfWork unit) : IMunicipioServicio
{
    public async Task<Respuesta<IReadOnlyList<MunicipioRespuestaDto>>> ObtenerTodosAsync()
    {
        var municipiosEntityList = await unit.Municipios.ObtenerTodosAsync();
        var municipios = municipiosEntityList.Select( m => m.MapeaRespuestaDto() ).ToList();

        return Respuesta<IReadOnlyList<MunicipioRespuestaDto>>.Exitoso(municipios);
    }
    public async Task<Respuesta<MunicipioRespuestaDto>> ObtenerPorIdAsync(Guid municipioId)
    {
        var municipio = await unit.Municipios.ObtenerPorIdAsync(municipioId);

         return municipio is not null ? 
             Respuesta<MunicipioRespuestaDto>.Exitoso( municipio.MapeaRespuestaDto() ) 
             : Respuesta<MunicipioRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);
    }

    public async Task<Respuesta<IReadOnlyList<MunicipioRespuestaDto>>> ObtenerPorEstadoAsync(string estadoId)
    {
        var municipios = (await unit.Municipios.ObtenerTodosPorEstadoAsync(estadoId))
            .Select( m => m.MapeaRespuestaDto() )
            .ToList();

        return Respuesta<IReadOnlyList<MunicipioRespuestaDto>>.Exitoso( municipios);
    }
}