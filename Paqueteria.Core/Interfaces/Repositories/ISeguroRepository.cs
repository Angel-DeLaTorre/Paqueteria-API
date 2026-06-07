using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface ISeguroRepository
{
    Task<Seguro?> GetByIdAsync(Guid seguroId, Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Seguro>> GetAllAsync(Guid empresaId, bool asTracking = true);
    Task<Seguro> AddAsync(Seguro entity);
    void Delete(Seguro entity);
}