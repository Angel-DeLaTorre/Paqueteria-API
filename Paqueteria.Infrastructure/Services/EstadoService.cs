using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class EstadoService(IEstadoRepository estadoRepository) : IEstadoService
{
    public async Task<Result<EstadoResponseDto>> GetEstadoByIdAsync(Guid id)
    {
        var estado = await estadoRepository.GetByIdAsync(id);
        if (estado is null)
            return Result<EstadoResponseDto>.Failure(Errors.Generic.NoEncontrado);

        return Result<EstadoResponseDto>.Success(EstadoResponseDto.FromEntity(estado));
    }

    public async Task<Result<IEnumerable<EstadoResponseDto>>> GetEstadosAsync()
    {
        var estados = ( await estadoRepository.GetAllAsync() ).Select(EstadoResponseDto.FromEntity).ToList();
        return Result<IEnumerable<EstadoResponseDto>>.Success(estados);
    }
}