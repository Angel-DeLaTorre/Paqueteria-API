using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface ISucursalService
{
    Task<Result<IReadOnlyList<SucursalResponseDto>>> GetAllAsync();
    Task<Result<SucursalResponseDto>> GetByIdAsync(Guid sucursalId);
    Task<Result<SucursalResponseDto>> CreateAsync(SucursalCreateDto dto);
    Task<Result> UpdateAsync(SucursaUpdateDto dto);
    Task<Result> DeleteAsync(Guid sucursalId);
}