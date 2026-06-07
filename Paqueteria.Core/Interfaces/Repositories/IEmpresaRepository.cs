using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IEmpresaRepository
{
    Task<Empresa?> GetByIdAsync(Guid empresaId, bool asTracking = true);
    Task<IReadOnlyList<Empresa>> GetAllAsync(bool asTracking = true);
    Task<Empresa> AddAsync(Empresa entity);
    void Delete(Empresa entity);
}