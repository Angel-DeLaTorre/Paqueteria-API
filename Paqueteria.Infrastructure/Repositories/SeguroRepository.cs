using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class SeguroRepository(AppDbContext context) : ISeguroRepository
{
    public async Task<Seguro?> GetByIdAsync(Guid seguroId, Guid empresaId, bool asTracking = true)
    {
        IQueryable<Seguro> query = context.Seguros;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(s => s.Id == seguroId && s.EmpresaId == empresaId);
    }
    
    public async Task<IReadOnlyList<Seguro>> GetAllAsync(Guid empresaId, bool asTracking = true)
    {
        IQueryable<Seguro> query = context.Seguros;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query
            .Where(c => c.EmpresaId == empresaId)
            .ToListAsync();
    }
    
    public async Task<Seguro> AddAsync(Seguro entity)
    {
        var ruta = await context.Seguros.AddAsync(entity);
        return ruta.Entity;
    }

    public void Delete(Seguro entity)
    {
        context.Seguros.Remove(entity);
    }
}