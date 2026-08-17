using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IUsuarioRepository
{
    Task<Usuario?> ObtenerPorIdAsync(Guid id, Guid empresaId, bool includeRoles = false);
    Task<Usuario?> ObtenerPorUsernameAsync(string username, Guid empresaId, bool includeRolesAndPermissions = false);
    Task<Usuario?> ObtenerPorUsernameAsync(string username, bool includeRolesAndPermissions = false);
    Task<IEnumerable<Usuario>> ObtenerTodosAsync(Guid empresaId);
    Task AgregarAsync(Usuario user);
    void Eliminar(Usuario user);
    Task AgregarRolAlUsuarioAsync(Guid userId, Guid roleId, Guid empresaId);
    Task RemoverRolAlUsuarioAsync(Guid userId, Guid roleId, Guid empresaId);
}