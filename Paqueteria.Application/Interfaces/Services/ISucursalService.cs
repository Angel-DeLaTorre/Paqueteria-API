using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface ISucursalService
{
    Task<Result<IReadOnlyList<SucursalResponseDto>>> GetAllAsync();
    Task<Result<SucursalResponseDto>> GetByIdAsync(Guid sucursalId);
    Task<Result<SucursalResponseDto>> CreateAsync(SucursalCreateDto dto, UserContext currentUser);
    Task<Result> UpdateAsync(SucursaUpdateDto dto, UserContext currentUser);
    Task<Result> DeleteAsync(Guid sucursalId, UserContext currentUser);
}