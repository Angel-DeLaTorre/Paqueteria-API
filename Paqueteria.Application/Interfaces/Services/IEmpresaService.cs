using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IEmpresaService
{
    Task<Result<IReadOnlyList<EmpresaResponseDto>>> GetAllAsync();
    Task<Result<EmpresaResponseDto>> GetByIdAsync(Guid empresaId);
    Task<Result<EmpresaResponseDto>> CreateAsync(EmpresaCreateDto dto, UserContext currentUser);
    Task<Result> UpdateAsync(EmpresaUpdateDto dto, UserContext currentUser);
    Task<Result> DeleteAsync(Guid empresaId, UserContext currentUser);
}