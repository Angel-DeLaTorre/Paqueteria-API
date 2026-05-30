using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IAsignacionSerivce
{
    Task<Result<IReadOnlyList<AsignacionResponseDto>>> GetAllAsync();
    Task<Result<AsignacionResponseDto>> GetByIdAsync(Guid asignacionId);
    Task<Result<AsignacionResponseDto>> CreateAsync(AsignacionCreateDto dto, UserContext currentUser);
    Task<Result> UpdateAsync(AsignacionUpdateDto dto, UserContext currentUser);
    Task<Result> DeleteAsync(Guid asignacionId, UserContext currentUser);
}