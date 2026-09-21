using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Clientes;
using Paqueteria.Comun.Dtos.Comun;

namespace Paqueteria.Aplicacion.Modulos.Clientes;

public interface IClienteServicio
{
    Task<Respuesta<ClienteRespuestaDto>> ObtenerPorIdAsync(Guid clienteId);
    Task<Respuesta<IReadOnlyList<ClienteRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<ClienteRespuestaDto>> AgregarAsync(ClienteCrearDto dto);
    Task<Respuesta> ActualizarAsync(ClienteActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid clienteId);

    Task<Respuesta> ActivarAsync(Guid clienteId);
    Task<Respuesta> DesactivarAsync(Guid clienteId);

    Task<Respuesta> AgregarDireccionAsync(Guid clienteId, DireccionDto dto);
    Task<Respuesta> ActivarDireccionAsync(Guid direccionId, Guid clienteId);
    Task<Respuesta> DesactivarDireccionAsync(Guid direccionId, Guid clienteId);
}