using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IPermisoService
{
    Task<Result<PermisoResponseDto>> GetByIdAsync(Guid id);
    Task<Result<IEnumerable<PermisoResponseDto>>> GetAllAsync();
    Task<Result<PermisoResponseDto>> CreateAsync(PermisoCreateDto dto);
    Task<Result> UpdateAsync(PermisoUpdateDto dto);
    Task<Result> DeleteAsync(Guid id);
    Task<Result> ActivarAsync(Guid permisoId);
    Task<Result> DesactivarAsync(Guid permisoId);
}