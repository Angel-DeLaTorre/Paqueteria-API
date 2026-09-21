using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Clientes;
using Paqueteria.Comun.Dtos.Comun;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface IClienteServicio
{
    Task<Respuesta<List<ClienteRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<ClienteRespuestaDto>> ObtenerPorIdAsync(int id);
    Task<Respuesta<ClienteRespuestaDto>> CrearAsync(ClienteCrearDto dto);
    Task<Respuesta> ActualizarAsync(Guid id, ClienteActualizarDto dto);
    Task<Respuesta> AgregarDireccion(Guid clienteId, DireccionDto dto);
    Task<Respuesta> EliminarAsync(Guid id);
    Task<Respuesta> ActivarAsync(Guid id);
    Task<Respuesta> DesactivarAsync(Guid id);
    Task<Respuesta> ActivarDireccionAsync(Guid id, Guid direccionId);
    Task<Respuesta> DesactivarDireccionAsync(Guid id, Guid direccionId);
    
}