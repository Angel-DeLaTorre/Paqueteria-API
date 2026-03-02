using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IArticuloService
{
    Task<Result<IReadOnlyList<ArticuloResponseDto>>> GetAllAsync();
    Task<Result<ArticuloResponseDto>> GetByIdAsync(Guid id);
    Task<Result<ArticuloResponseDto>> CreateAsync(ArticuloCreateDto dto);
    Task<Result> UpdateAsync(ArticuloUpdateDto dto);
}