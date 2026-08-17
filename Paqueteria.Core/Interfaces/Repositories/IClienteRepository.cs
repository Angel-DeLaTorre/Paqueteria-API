using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IClienteRepository
{
    Task<Cliente?> ObtenerPorIdAsync(Guid clienteId, Guid empresaId, bool asTraking = true);
    Task<DireccionCliente?> ObtenerDireccionPorIdAsync(Guid clienteId, Guid direccionId, bool asTracking = true);
    
    Task<IReadOnlyList<Cliente>> ObtenerTodosAsync(Guid empresaId);
    Task<Cliente> AgregarAsync(Cliente entity);
    void Eliminar(Cliente entity);
    
    Task AgregarDireccionAsync(DireccionCliente dir);
}