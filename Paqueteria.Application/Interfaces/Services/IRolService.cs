using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IRolService
{
    Task<Result<RolResponseDto>> GetByIdAsync(Guid id);
    Task<Result<IEnumerable<RolResponseDto>>> GetAllAsync();
    Task<Result<RolResponseDto>> CreateAsync(RolCreateDto dto);
    Task<Result> UpdateAsync(RolUpdateDto dto);
    Task<Result> DeleteAsync(Guid id);
    Task<Result> ActivarAsync(Guid rolId);
    Task<Result> DesactivarAsync(Guid rolId);
}