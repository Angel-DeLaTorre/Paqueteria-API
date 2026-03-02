using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IRutaService
{
    Task<Result<IReadOnlyList<RutaResponseDto>>> GetAllAsync();
    Task<Result<RutaResponseDto>> GetByIdAsync(Guid rutaId);
    Task<Result<RutaResponseDto>> CreateAsync(RutaCreateDto dto, UserContext currentUser);
    Task<Result> UpdateAsync(RutaUpdateDto dto, UserContext currentUser);
    Task<Result> DeleteAsync(Guid rutaId, UserContext currentUser);
}