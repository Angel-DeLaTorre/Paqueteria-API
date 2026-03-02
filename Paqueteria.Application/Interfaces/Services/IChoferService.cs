using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IChoferService
{
    Task<Result<IReadOnlyList<ChoferResponseDto>>> GetAllAsync();
    Task<Result<ChoferResponseDto>> GetByIdAsync(Guid id);
    Task<Result<ChoferResponseDto>> CreateAsync(ChoferCreateDto dto);
    Task<Result<bool>> UpdateAsync(ChoferUpdateDto dto);
}