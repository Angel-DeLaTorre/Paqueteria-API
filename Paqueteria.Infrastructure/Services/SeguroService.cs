using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;

namespace Paqueteria.Infrastructure.Services;

public class SeguroService(ISeguroRepository seguroRepository) : ISeguroService
{
    public Task<Result<IReadOnlyList<SeguroResponseDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<SeguroResponseDto>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<SeguroResponseDto>> CreateAsync(SeguroCreateDto dto, Guid usuarioId, Guid sucursalId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateAsync(SeguroUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}