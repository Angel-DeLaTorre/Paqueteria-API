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

    public Task<Result<SeguroResponseDto>> GetByIdAsync(Guid seguroId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<SeguroResponseDto>> CreateAsync(SeguroCreateDto dto, UserContext currentUser)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(SeguroUpdateDto dto, UserContext currentUser)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid seguroId, UserContext currentUser)
    {
        throw new NotImplementedException();
    }
}