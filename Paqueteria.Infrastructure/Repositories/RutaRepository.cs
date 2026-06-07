using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class RutaRepository(AppDbContext context) : IRutaRepository
{
    public async Task<Ruta?> GetByIdAsync(Guid rutaId, Guid empresaId)
    {
        return await context.Rutas
            .Include(r => r.SucursalOrigen)
            .Include(r => r.SucursalDestino)
            .FirstOrDefaultAsync(r => r.Id == rutaId && r.EmpresaId == empresaId);
    }

    public async Task<IEnumerable<Ruta>> GetAllAsync(Guid empresaId)
    {
        return await context.Rutas
            .Include(r => r.SucursalOrigen)
            .Include(r => r.SucursalDestino)
            .Where(r => r.EmpresaId == empresaId)
            .ToListAsync();
    }

    public async Task<Ruta> AddAsync(Ruta entity)
    {
        var ruta = await context.Rutas.AddAsync(entity);
        return ruta.Entity;
    }

    public void Delete(Ruta entity)
    {
        context.Rutas.Remove(entity);
    }
}