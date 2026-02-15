using Microsoft.EntityFrameworkCore;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public sealed class SucursalRepository(AppDbContext context) : ISucursalRepository
{
    public async Task<Sucursal?> ObtenerSucursalPorIdAsync(Guid sucursalId, bool conRastreo = false)
    {
        IQueryable<Sucursal> query = context.Sucursales.Include( s => s.IdMunicipio);
        if (!conRastreo) query = query.AsNoTracking();
        return await query.FirstOrDefaultAsync(g => g.IdSucursal == sucursalId);
    }

    public async Task<ICollection<Sucursal>> ObtenerSucursalesAsync()
    {
        return await context.Sucursales.AsNoTracking().ToListAsync();
    }

    public async Task<Sucursal> InsertarSucursalAsync(Sucursal sucursal)
    {
        await context.Sucursales.AddAsync(sucursal);
        await context.SaveChangesAsync();
        return sucursal;
    }

    public async Task EditarSucursalAsync(Sucursal sucursal)
    {
        context.Entry(sucursal).State = EntityState.Modified;
        await context.SaveChangesAsync();
    }

    public async Task DesactivarSucursalAsync(Guid sucursalId)
    {
        var sucursal = await context.Sucursales.FindAsync(sucursalId);

        if (sucursal != null)
        {
            sucursal.Estatus = EstatusGenerico.Inactivo;
            await context.SaveChangesAsync();
        }
    }
}