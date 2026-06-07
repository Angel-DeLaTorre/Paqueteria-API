using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IRutaService
{
    Task<Result<IReadOnlyList<RutaResponseDto>>> GetAllAsync();
    Task<Result<RutaResponseDto>> GetByIdAsync(Guid rutaId);
    Task<Result<RutaResponseDto>> CreateAsync(RutaCreateDto dto);
    Task<Result> UpdateAsync(RutaUpdateDto dto);
    Task<Result> DeleteAsync(Guid rutaId);
}