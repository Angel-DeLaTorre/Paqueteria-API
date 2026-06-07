using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IChoferRepository
{
    Task<Chofer?> GetByIdAsync(Guid choferId, Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Chofer>> GetAllAsync(Guid empresaId, bool asTracking = true);
    Task<Chofer> AddAsync(Chofer entity);
    void Delete(Chofer entity);
}