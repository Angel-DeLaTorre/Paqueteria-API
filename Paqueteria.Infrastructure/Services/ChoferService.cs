using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;

namespace Paqueteria.Infrastructure.Services;

public class ChoferService(IChoferRepository choferRepository) : IChoferService
{
    public Task<Result<IReadOnlyList<ChoferResponseDto>>> GetAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<Result<ChoferResponseDto>> GetByIdAsync(Guid choferId)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ChoferResponseDto>> CreateAsync(ChoferCreateDto dto, UserContext currentUser)
    {
        throw new NotImplementedException();
    }

    public Task<Result> UpdateAsync(ChoferUpdateDto dto, UserContext currentUser)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteAsync(Guid choferId, UserContext currentUser)
    {
        throw new NotImplementedException();
    }
}