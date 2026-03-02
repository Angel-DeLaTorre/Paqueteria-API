using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;
using Paqueteria.Core.Entities;

namespace Paqueteria.Application.Interfaces.Services;

public interface IClienteService
{
    Task<Result<IReadOnlyList<ClienteResponseDto>>> GetAllAsync();
    Task<Result<ClienteResponseDto>> GetByIdAsync(Guid clienteId);
    Task<Result<ClienteResponseDto>> CreateAsync(ClienteCreateDto dto, UserContext currentUser);
    Task<Result> UpdateAsync(ClienteUpdateDto dto, UserContext currentUser);
    Task<Result> DeleteAsync(Guid clienteId, UserContext currentUser);
}