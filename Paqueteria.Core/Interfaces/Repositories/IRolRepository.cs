using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IRolRepository 
{
    Task<Rol?> ObtenerPorIdAsync(Guid id, Guid empresaId, bool includePermissions = false);
    Task<Rol?> ObtenerPorNombreAsync(string nombre, Guid empresaId);
    Task<IEnumerable<Rol>> ObtenerTodosAsync(Guid empresaId);
    Task AgregarAsync(Rol role);
    void Eliminar(Rol role);
    
    // Métodos para gestionar la relación con Permisos
    Task AgregarPermisoAlRolAsync(Guid roleId, Guid permissionId, Guid empresaId);
    Task RemoverPermisoAlRolAsync(Guid roleId, Guid permissionId, Guid empresaId);
}