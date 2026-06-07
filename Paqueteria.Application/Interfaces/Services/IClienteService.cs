using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Interfaces.Services;

public interface IClienteService
{
    Task<Result<ClienteResponseDto>> GetByIdAsync(Guid clienteId);
    Task<Result<IReadOnlyList<ClienteResponseDto>>> GetAllAsync();
    Task<Result<ClienteResponseDto>> CreateAsync(ClienteCreateDto dto);
    Task<Result> UpdateAsync(ClienteUpdateDto dto);
    Task<Result> DeleteAsync(Guid clienteId);

    Task<Result> ActivarAsync(Guid clienteId);
    Task<Result> DesactivarAsync(Guid clienteId);

    Task<Result> ActivarDireccionAsync(Guid direccionId, Guid clienteId);
    Task<Result> DesactivarDireccionAsync(Guid direccionId, Guid clienteId);
}