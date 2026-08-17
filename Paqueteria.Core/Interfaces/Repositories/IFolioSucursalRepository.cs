using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Core.Interfaces.Repositories;

public interface IFolioSucursalRepository
{
    public Task AgregarAsync(FolioSucursal entity);
    Task<FolioSucursal?> ObtenerConBloqueoAsync(Guid sucursalId);
}