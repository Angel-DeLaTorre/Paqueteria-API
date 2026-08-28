using Paqueteria.Application.Dtos;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Estados.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;

namespace Paqueteria.Application.Modulos.Estados;

public class EstadoServicio(IUnitOfWork unit) : IEstadoServicio
{
    public async Task<Respuesta<EstadoResponseDto>> ObtenerPorIdAsync(Guid id)
    {
        var estado = await unit.Estados.GetByIdAsync(id);
        if (estado is null)
            return Respuesta<EstadoResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<EstadoResponseDto>.Exitoso(EstadoResponseDto.FromEntity(estado));
    }

    public async Task<Respuesta<IEnumerable<EstadoResponseDto>>> ObtenerTodosAsync()
    {
        var estados = ( await unit.Estados.GetAllAsync() ).Select(EstadoResponseDto.FromEntity).ToList();
        return Respuesta<IEnumerable<EstadoResponseDto>>.Exitoso(estados);
    }
}