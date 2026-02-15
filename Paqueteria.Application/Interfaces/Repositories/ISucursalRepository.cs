using Paqueteria.Core.Entities;

namespace Paqueteria.Application.Interfaces.Repositories;

public interface ISucursalRepository
{
    Task<Sucursal?> ObtenerSucursalPorIdAsync(Guid sucursalId, bool conRastreo = false);
    Task<ICollection<Sucursal>> ObtenerSucursalesAsync();
    Task<Sucursal> InsertarSucursalAsync(Sucursal sucursal);
    Task EditarSucursalAsync(Sucursal sucursal);
    Task DesactivarSucursalAsync(Guid sucursalId);
}