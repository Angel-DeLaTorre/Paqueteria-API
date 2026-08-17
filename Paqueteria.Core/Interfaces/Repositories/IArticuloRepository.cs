using Paqueteria.Core.Entities.Sat;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IArticuloRepository
{
    Task<Articulo?> ObtenerPorIdAsync(Guid articuloId, bool asTracking = true);
    Task<IReadOnlyList<Articulo>> ObtenerTodosAsync(bool asTracking = true);
    Task<Articulo> AgregarAsync(Articulo entity);
    void Eliminar(Articulo entity);
}