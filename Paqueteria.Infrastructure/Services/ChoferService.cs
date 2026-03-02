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

    public Task<Result<ChoferResponseDto>> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task<Result<ChoferResponseDto>> CreateAsync(ChoferCreateDto dto)
    {
        throw new NotImplementedException();
    }

    public Task<Result<bool>> UpdateAsync(ChoferUpdateDto dto)
    {
        throw new NotImplementedException();
    }
}