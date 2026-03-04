using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IGuiaService
{
    Task<Result<IReadOnlyList<GuiaResponseDto>>> GetAllAsync();
    Task<Result<GuiaResponseDto>> GetByIdAsync(Guid guiaId);
    Task<Result<GuiaResponseDto>> CreateAsync(GuiaCreateDto dto, UserContext currentUser);
    Task<Result> UpdateAsync(GuiaUpdateDto dto, UserContext currentUser);
    Task<Result> DeleteAsync(Guid guiaId, UserContext currentUser);
}