using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Application.Modulos.Estados;

public class EstadoServicio(IUnitOfWork unit) : IEstadoServicio
{
    public async Task<Resultado<EstadoResponseDto>> ObtenerPorIdAsync(Guid id)
    {
        var estado = await unit.Estados.GetByIdAsync(id);
        if (estado is null)
            return Resultado<EstadoResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<EstadoResponseDto>.Exitoso(EstadoResponseDto.FromEntity(estado));
    }

    public async Task<Resultado<IEnumerable<EstadoResponseDto>>> ObtenerTodosAsync()
    {
        var estados = ( await unit.Estados.GetAllAsync() ).Select(EstadoResponseDto.FromEntity).ToList();
        return Resultado<IEnumerable<EstadoResponseDto>>.Exitoso(estados);
    }
}