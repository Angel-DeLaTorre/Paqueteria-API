using Paqueteria.Application.Modulos.Clientes.Dtos;
using Paqueteria.Dominio.Comun;

namespace Paqueteria.Application.Modulos.Clientes;

public interface IClienteServicio
{
    Task<Respuesta<ClienteRespuestaDto>> ObtenerPorIdAsync(Guid clienteId);
    Task<Respuesta<IReadOnlyList<ClienteRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<ClienteRespuestaDto>> AgregarAsync(ClienteCrearDto dto);
    Task<Respuesta> ActualizarAsync(ClienteActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid clienteId);

    Task<Respuesta> ActivarAsync(Guid clienteId);
    Task<Respuesta> DesactivarAsync(Guid clienteId);

    Task<Respuesta> ActivarDireccionAsync(Guid direccionId, Guid clienteId);
    Task<Respuesta> DesactivarDireccionAsync(Guid direccionId, Guid clienteId);
}