using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface ISucursalRepository
{
    public Task<Sucursal?> GetByIdAsync(Guid sucursalId, Guid empresaId);

    public Task<IReadOnlyList<Sucursal>> GetAllAsync(Guid empresaId);

    public Task<Sucursal> AddAsync(Sucursal entity);

    public void Update(Sucursal entity);

    public void Delete(Sucursal entity);
}