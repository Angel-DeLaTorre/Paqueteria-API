using Paqueteria.Core.Entidades.Remisiones;
using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Interfaces.Repositorios;

public interface IFolioSucursalRepositorio
{
    public Task AgregarAsync(FolioSucursal entity);
    Task<FolioSucursal?> ObtenerConBloqueoAsync(Guid sucursalId, TipoFolio tipo);
}