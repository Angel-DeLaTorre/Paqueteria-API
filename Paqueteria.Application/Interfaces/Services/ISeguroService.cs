using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface ISeguroService
{
    Task<Result<IReadOnlyList<SeguroResponseDto>>> GetAllAsync();
    Task<Result<SeguroResponseDto>> GetByIdAsync(Guid id);
    Task<Result<SeguroResponseDto>> CreateAsync(SeguroCreateDto dto, Guid usuarioId, Guid sucursalId);
    Task<Result<bool>> UpdateAsync(SeguroUpdateDto dto);
}