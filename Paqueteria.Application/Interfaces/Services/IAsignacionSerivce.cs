using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IAsignacionSerivce
{
    Task<Result<IReadOnlyList<AsignacionResponseDto>>> GetAllAsync();
    Task<Result<AsignacionResponseDto>> GetByIdAsync(Guid asignacionId);
    Task<Result<AsignacionResponseDto>> CreateAsync(AsignacionCreateDto dto);
    Task<Result> UpdateAsync(AsignacionUpdateDto dto);
    Task<Result> DeleteAsync(Guid asignacionId);
}