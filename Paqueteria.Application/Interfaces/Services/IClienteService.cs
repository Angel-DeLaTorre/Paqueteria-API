using Paqueteria.Application.DTOs;
using Paqueteria.Core.Entities;

namespace Paqueteria.Application.Interfaces.Services;

public interface IClienteService
{
    Task<IReadOnlyList<Cliente>> ObtenerTodosAsync();
    Task<ClienteResponseDto> CreateAsync(ClienteCreateDto dto, Guid usuarioId, Guid sucursalId);
}