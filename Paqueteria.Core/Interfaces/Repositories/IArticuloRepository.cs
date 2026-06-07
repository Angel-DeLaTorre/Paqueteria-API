using Paqueteria.Core.Entities.Sat;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IArticuloRepository
{
    Task<Articulo?> GetByIdAsync(string articuloId, bool asTracking = true);
    Task<IReadOnlyList<Articulo>> GetAllAsync(bool asTracking = true);
    Task<Articulo> AddAsync(Articulo entity);
    void Delete(Articulo entity);
}