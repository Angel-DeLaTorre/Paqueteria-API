using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IClienteRepository
{
    Task<Cliente?> GetByIdAsync(Guid clienteId, Guid empresaId, bool asTraking = true);
    Task<DireccionCliente?> GetDireccionByIdAsync(Guid clienteId, Guid direccionId, bool asTracking = true);
    Task<IReadOnlyList<Cliente>> GetAllAsync(Guid empresaId);
    Task<Cliente> AddAsync(Cliente entity);
    void Delete(Cliente entity);
    
    Task AddDireccion(DireccionCliente dir);
}