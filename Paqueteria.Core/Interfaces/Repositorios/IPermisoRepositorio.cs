using Paqueteria.Core.Entidades.Sistema;

namespace Paqueteria.Core.Interfaces.Repositorios;

public interface IPermisoRepositorio
{
    Task<Permiso?> ObtenerPorIdAsync(Guid id, Guid empresaId);
    Task<Permiso?> ObtenerPorNombreAsync(string nombre, Guid empresaId);
    Task<IEnumerable<Permiso>> ObtenerTodosAsync(Guid empresaId);
    Task AgregarAsync(Permiso permission);
    void Eliminar(Permiso permission);
}