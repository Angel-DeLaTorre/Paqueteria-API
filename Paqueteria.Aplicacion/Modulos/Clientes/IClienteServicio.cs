using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Clientes.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Clientes;

public interface IClienteServicio
{
    Task<Respuesta<ClienteResponseDto>> ObtenerPorIdAsync(Guid clienteId);
    Task<Respuesta<IReadOnlyList<ClienteResponseDto>>> ObtenerTodosAsync();
    Task<Respuesta<ClienteResponseDto>> AgregarAsync(ClienteCreateDto dto);
    Task<Respuesta> ActualizarAsync(ClienteUpdateDto dto);
    Task<Respuesta> EliminarAsync(Guid clienteId);

    Task<Respuesta> ActivarAsync(Guid clienteId);
    Task<Respuesta> DesactivarAsync(Guid clienteId);

    Task<Respuesta> ActivarDireccionAsync(Guid direccionId, Guid clienteId);
    Task<Respuesta> DesactivarDireccionAsync(Guid direccionId, Guid clienteId);
}