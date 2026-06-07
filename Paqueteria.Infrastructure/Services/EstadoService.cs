using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Infrastructure.Services;

public class EstadoService(IUnitOfWork unit) : IEstadoService
{
    public async Task<Result<EstadoResponseDto>> GetEstadoByIdAsync(Guid id)
    {
        var estado = await unit.Estados.GetByIdAsync(id);
        if (estado is null)
            return Result<EstadoResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<EstadoResponseDto>.Success(EstadoResponseDto.FromEntity(estado));
    }

    public async Task<Result<IEnumerable<EstadoResponseDto>>> GetEstadosAsync()
    {
        var estados = ( await unit.Estados.GetAllAsync() ).Select(EstadoResponseDto.FromEntity).ToList();
        return Result<IEnumerable<EstadoResponseDto>>.Success(estados);
    }
}