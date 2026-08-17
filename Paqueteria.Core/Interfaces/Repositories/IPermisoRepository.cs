using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IPermisoRepository
{
    Task<Permiso?> ObtenerPorIdAsync(Guid id, Guid empresaId);
    Task<Permiso?> ObtenerPorNombreAsync(string nombre, Guid empresaId);
    Task<IEnumerable<Permiso>> ObtenerTodosAsync(Guid empresaId);
    Task AgregarAsync(Permiso permission);
    void Eliminar(Permiso permission);
}