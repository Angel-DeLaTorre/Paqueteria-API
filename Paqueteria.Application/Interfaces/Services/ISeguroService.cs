using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface ISeguroService
{
    Task<Result<IReadOnlyList<SeguroResponseDto>>> GetAllAsync();
    Task<Result<SeguroResponseDto>> GetByIdAsync(Guid seguroId);
    Task<Result<SeguroResponseDto>> CreateAsync(SeguroCreateDto dto);
    Task<Result> UpdateAsync(SeguroUpdateDto dto);
    Task<Result> DeleteAsync(Guid seguroId);
}