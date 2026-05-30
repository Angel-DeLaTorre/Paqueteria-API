using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IPermisoService
{
    Task<Result<PermisoResponseDto>> GetByIdAsync(Guid id, UserContext currentUser);
    Task<Result<IEnumerable<PermisoResponseDto>>> GetAllAsync(UserContext currentUser);
    Task<Result<PermisoResponseDto>> CreateAsync(PermisoCreateDto dto, UserContext currentUser);
    Task<Result> UpdateAsync(PermisoUpdateDto dto, UserContext currentUser);
    Task<Result> DeleteAsync(Guid id, UserContext currentUser);
}