using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IAsignacionRepository
{
    public Task<Asignacion?> GetByIdAsync(Guid asignacionId, Guid empresaId);
    public Task<IEnumerable<Asignacion>> GetAllAsync(Guid empresaId);
    public Task<Asignacion> AddAsync(Asignacion entity);
    public void Delete(Asignacion entity);
}