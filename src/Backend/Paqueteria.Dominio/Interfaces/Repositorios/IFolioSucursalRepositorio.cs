using Paqueteria.Comun.Enums;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Dominio.Interfaces.Repositorios;

public interface IFolioSucursalRepositorio
{
    public Task AgregarAsync(FolioSucursal entity);
    Task<FolioSucursal?> ObtenerConBloqueoAsync(Guid sucursalId, TipoFolio tipo);
}