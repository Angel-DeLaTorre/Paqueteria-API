using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities;

namespace Paqueteria.Application.Interfaces.Services;

public interface IClienteService
{
    Task<Result<IReadOnlyList<ClienteResponseDto>>> GetAllAsync();
    Task<Result<ClienteResponseDto>> GetByIdAsync(Guid id);
    Task<Result<ClienteResponseDto>> CreateAsync(ClienteCreateDto dto, Guid usuarioId, Guid sucursalId);
    Task<Result<bool>> UpdateAsync(ClienteUpdateDto dto);
}