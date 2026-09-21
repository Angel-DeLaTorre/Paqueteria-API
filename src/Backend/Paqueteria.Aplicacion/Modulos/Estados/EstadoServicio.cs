using Paqueteria.Aplicacion.Interfaces.Persistence;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Comun.Errores;
using Paqueteria.Comun.Dtos.Estados;

namespace Paqueteria.Aplicacion.Modulos.Estados;

public class EstadoServicio(IUnitOfWork unit) : IEstadoServicio
{
    public async Task<Respuesta<EstadoRespuestaDto>> ObtenerPorIdAsync(Guid id)
    {
        var estado = await unit.Estados.ObtenerPorIdAsync(id);
        return estado is not null
            ? Respuesta<EstadoRespuestaDto>.Exitoso(estado.MapeaRespuestaDto())
            : Respuesta<EstadoRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);
    }

    public async Task<Respuesta<IEnumerable<EstadoRespuestaDto>>> ObtenerTodosAsync()
    {
        var estados = (await unit.Estados.ObtenerTodosAsync())
            .Select(e => e.MapeaRespuestaDto())
            .ToList();
        return Respuesta<IEnumerable<EstadoRespuestaDto>>.Exitoso(estados);
    }

    public async Task<Respuesta<IEnumerable<EstadoRespuestaDto>>> ObtenerPorPais(string pais)
    {
        var estados = (await unit.Estados.ObtenerPorPaisAsync(pais))
            .Select(e => e.MapeaRespuestaDto())
            .ToList();
        return Respuesta<IEnumerable<EstadoRespuestaDto>>.Exitoso(estados);
    }
}