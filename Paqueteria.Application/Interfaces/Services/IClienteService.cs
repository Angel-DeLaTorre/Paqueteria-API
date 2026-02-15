using Paqueteria.Application.DTOs;

namespace Paqueteria.Application.Interfaces.Services;

public interface IClienteService
{
    Task<ClienteResponseDto> CrearClienteAsync(ClienteCreateDto dto, Guid usuarioId, Guid sucursalId);
    Task<IEnumerable<ClienteResponseDto>> ObtenerTodosAsync();
}