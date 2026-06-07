using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class SucursalRepository(AppDbContext context) :  ISucursalRepository
{
    public async Task<Sucursal?> GetByIdAsync(Guid sucursalId, Guid empresaId)
    {
        return await context.Sucursales
            .FirstOrDefaultAsync(s => s.Id == sucursalId && s.EmpresaId == empresaId);
    }

    public async Task<IReadOnlyList<Sucursal>> GetAllAsync(Guid empresaId)
    {
        return await context.Sucursales
            .Where(r => r.EmpresaId == empresaId)
            .ToListAsync();
    }

    public async Task<Sucursal> AddAsync(Sucursal entity)
    {
        var ruta = await context.Sucursales.AddAsync(entity);
        return ruta.Entity;
    }

    public async void Update(Sucursal entity)
    {
        throw new NotImplementedException();
    }

    public void Delete(Sucursal entity)
    {
        context.Sucursales.Remove(entity);
    }
}