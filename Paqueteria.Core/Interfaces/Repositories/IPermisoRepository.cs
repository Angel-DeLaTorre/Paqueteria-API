using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IPermisoRepository
{
    Task<Permiso?> GetByIdAsync(Guid id, Guid empresaId);
    Task<Permiso?> GetByNameAsync(string name, Guid empresaId);
    Task<IEnumerable<Permiso>> GetAllAsync(Guid empresaId);
    Task AddAsync(Permiso permission);
    void Delete(Permiso permission);
}