using Paqueteria.Core.Entidades.Sistema;

namespace Paqueteria.Core.Interfaces.Repositorios;

public interface IUsuarioRepositorio
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