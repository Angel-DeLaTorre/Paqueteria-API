using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IRolService
{
    Task<Result<RolResponseDto>> GetByIdAsync(Guid id, UserContext currentUser);
    Task<Result<IEnumerable<RolResponseDto>>> GetAllAsync(UserContext currentUser);
    Task<Result<RolResponseDto>> CreateAsync(RolCreateDto dto, UserContext currentUser);
    Task<Result> UpdateAsync(RolUpdateDto dto, UserContext currentUser);
    Task<Result> DeleteAsync(Guid id, UserContext currentUser);
}