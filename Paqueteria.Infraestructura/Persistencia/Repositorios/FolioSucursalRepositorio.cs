using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entidades.Remisiones;
using Paqueteria.Core.Enums;
using Paqueteria.Core.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class FolioSucursalRepositorio(AppDbContext context) : IFolioSucursalRepositorio
{
    public async Task AgregarAsync(FolioSucursal entity)
    {
        await context.FoliosSucursal.AddAsync(entity);
    }

    public async Task<FolioSucursal?> ObtenerConBloqueoAsync(Guid sucursalId, TipoFolio tipo)
    {
        var tipoInt = (int)tipo;
        return await context.FoliosSucursal
            .FromSqlInterpolated($"SELECT * FROM folios_sucursales WHERE sucursal_id = {sucursalId} AND tipo = {tipoInt} FOR UPDATE")
            .FirstOrDefaultAsync();
    }
}