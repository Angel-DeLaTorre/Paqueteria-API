using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Clientes;

public interface IClienteServicio
{
    Task<Resultado<ClienteResponseDto>> ObtenerPorIdAsync(Guid clienteId);
    Task<Resultado<IReadOnlyList<ClienteResponseDto>>> ObtenerTodosAsync();
    Task<Resultado<ClienteResponseDto>> AgregarAsync(ClienteCreateDto dto);
    Task<Resultado> ActualizarAsync(ClienteUpdateDto dto);
    Task<Resultado> EliminarAsync(Guid clienteId);

    Task<Resultado> ActivarAsync(Guid clienteId);
    Task<Resultado> DesactivarAsync(Guid clienteId);

    Task<Resultado> ActivarDireccionAsync(Guid direccionId, Guid clienteId);
    Task<Resultado> DesactivarDireccionAsync(Guid direccionId, Guid clienteId);
}