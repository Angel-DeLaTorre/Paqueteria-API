using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IRutaRepository
{
    public Task<Ruta?> GetByIdAsync(Guid rutaId, Guid empresaId);
    public Task<IEnumerable<Ruta>> GetAllAsync(Guid empresaId);
    public Task<Ruta> AddAsync(Ruta entity);
    public void Delete(Ruta entity);
}