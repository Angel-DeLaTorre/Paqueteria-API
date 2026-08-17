using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface ISucursalRepository
{
    public Task<Sucursal?> ObtenerPorIdAsync(Guid sucursalId, Guid empresaId);
    public Task<IReadOnlyList<Sucursal>> ObtenerTodosAsync(Guid empresaId);
    public Task<Sucursal> AgregarAsync(Sucursal entity);
    public void Eliminar(Sucursal entity);
}