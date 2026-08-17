using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class FolioSucursalRepository(AppDbContext context) : IFolioSucursalRepository
{
    public async Task AgregarAsync(FolioSucursal entity)
    {
        await context.FoliosSucursal.AddAsync(entity);
    }

    public async Task<FolioSucursal?> ObtenerConBloqueoAsync(Guid sucursalId)
    {
        return await context.FoliosSucursal
            .FromSqlInterpolated($"SELECT * FROM folios_sucursales WHERE sucursal_id = {sucursalId} FOR UPDATE")
            .FirstOrDefaultAsync();
    }
}