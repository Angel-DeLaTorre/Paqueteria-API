using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IArticuloService
{
    Task<Result<IReadOnlyList<ArticuloResponseDto>>> GetAllAsync();
    Task<Result<ArticuloResponseDto>> GetByIdAsync(string articuloId);
    Task<Result<ArticuloResponseDto>> CreateAsync(ArticuloCreateDto dto, UserContext currentUser);
    Task<Result> UpdateAsync(ArticuloUpdateDto dto, UserContext currentUser);
    Task<Result> DeleteAsync(Guid articuloId, UserContext currentUser);
}