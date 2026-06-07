using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IChoferService
{
    Task<Result<IReadOnlyList<ChoferResponseDto>>> GetAllAsync();
    Task<Result<ChoferResponseDto>> GetByIdAsync(Guid choferId);
    Task<Result<ChoferResponseDto>> CreateAsync(ChoferCreateDto dto);
    Task<Result> UpdateAsync(ChoferUpdateDto dto);
    Task<Result> DeleteAsync(Guid choferId);
    Task<Result> ActivarAsync(Guid choferId);
    Task<Result> DesactivarAsync(Guid choferId);
}