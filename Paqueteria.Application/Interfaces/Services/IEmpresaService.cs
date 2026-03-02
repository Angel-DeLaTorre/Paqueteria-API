using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IEmpresaService
{
    Task<Result<IReadOnlyList<EmpresaResponseDto>>> GetAllAsync();
    Task<Result<EmpresaResponseDto>> GetByIdAsync(Guid id);
    Task<Result<EmpresaResponseDto>> CreateAsync(EmpresaCreateDto dto);
    Task<Result> UpdateAsync(EmpresaUpdateDto dto);
    Task<Result> DeleteAsync(Guid empresaId);
}