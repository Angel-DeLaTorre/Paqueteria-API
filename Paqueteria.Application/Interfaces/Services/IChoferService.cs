using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IChoferService
{
    Task<Result<IReadOnlyList<ChoferResponseDto>>> GetAllAsync();
    Task<Result<ChoferResponseDto>> GetByIdAsync(Guid choferId);
    Task<Result<ChoferResponseDto>> CreateAsync(ChoferCreateDto dto, UserContext currentUser);
    Task<Result> UpdateAsync(ChoferUpdateDto dto, UserContext currentUser);
    Task<Result> DeleteAsync(Guid choferId, UserContext currentUser);
}