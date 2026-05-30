using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.Interfaces.Repositories;

public interface IPermisoRepository
{
    Task<Permiso?> GetByIdAsync(Guid id, Guid empresaId);
    Task<Permiso?> GetByNameAsync(string name, Guid empresaId);
    Task<IEnumerable<Permiso>> GetAllAsync(Guid empresaId);
    Task AddAsync(Permiso permission);
    void Delete(Permiso permission);
}