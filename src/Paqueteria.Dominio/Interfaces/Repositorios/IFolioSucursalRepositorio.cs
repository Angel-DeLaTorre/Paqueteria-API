using Paqueteria.Dominio.Entidades.Remisiones;
using Paqueteria.Dominio.Enums;

namespace Paqueteria.Dominio.Interfaces.Repositorios;

public interface IFolioSucursalRepositorio
{
    public Task AgregarAsync(FolioSucursal entity);
    Task<FolioSucursal?> ObtenerConBloqueoAsync(Guid sucursalId, TipoFolio tipo);
}