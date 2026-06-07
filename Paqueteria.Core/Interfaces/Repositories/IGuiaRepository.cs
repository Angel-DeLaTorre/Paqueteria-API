using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IGuiaRepository
{
    Task<Guia?> GetByIdAsync(Guid guiaId, Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Guia>> GetAllAsync(Guid empresaId, bool asTracking = true);
    Task<Guia> AddAsync(Guia entity);
    void Delete(Guia entity);
}