using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Estados.Dtos;
using Paqueteria.Dominio.Comun;
using Paqueteria.Dominio.Comun.Errors;

namespace Paqueteria.Application.Modulos.Estados;

public class EstadoServicio(IUnitOfWork unit) : IEstadoServicio
{
    public async Task<Respuesta<EstadoRespuestaDto>> ObtenerPorIdAsync(Guid id)
    {
        var estado = await unit.Estados.ObtenerPorIdAsync(id);
        return estado is not null ?
            Respuesta<EstadoRespuestaDto>.Exitoso( estado.MapeaRespuestaDto() )
            : Respuesta<EstadoRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);
    }

    public async Task<Respuesta<IEnumerable<EstadoRespuestaDto>>> ObtenerTodosAsync()
    {
        var estados = ( await unit.Estados.ObtenerTodosAsync() )
            .Select( e => e.MapeaRespuestaDto() )
            .ToList();
        return Respuesta<IEnumerable<EstadoRespuestaDto>>.Exitoso(estados);
    }
}