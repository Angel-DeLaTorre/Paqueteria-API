using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> GetByIdAsync(Guid id, Guid empresaId, bool includeRoles = false);
    Task<Usuario?> GetByUsernameAsync(string username, Guid empresaId, bool includeRolesAndPermissions = false);
    Task<Usuario?> GetByUsernameAsync(string username, bool includeRolesAndPermissions = false);
    Task<IEnumerable<Usuario>> GetAllAsync(Guid empresaId);
    Task AddAsync(Usuario user);
    void Delete(Usuario user);

    Task RegistrarIngreso(Usuario usuario);
    
    Task AddRoleToUserAsync(Guid userId, Guid roleId, Guid empresaId);
    Task RemoveRoleFromUserAsync(Guid userId, Guid roleId, Guid empresaId);
}