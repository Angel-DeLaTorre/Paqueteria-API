using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Sat;

namespace Paqueteria.Application.Interfaces.Repositories;

public interface IArticuloRepository : IEntityRepository<Articulo>
{
    Task<Articulo?> GetByIdAsync(string id);
}