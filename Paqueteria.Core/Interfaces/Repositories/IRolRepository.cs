using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IRolRepository 
{
    Task<Rol?> GetByIdAsync(Guid id, Guid empresaId, bool includePermissions = false);
    Task<Rol?> GetByNameAsync(string name, Guid empresaId);
    Task<IEnumerable<Rol>> GetAllAsync(Guid empresaId);
    Task AddAsync(Rol role);
    void Delete(Rol role);
    
    // Métodos para gestionar la relación con Permisos
    Task AddPermissionToRoleAsync(Guid roleId, Guid permissionId, Guid empresaId);
    Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId, Guid empresaId);
}