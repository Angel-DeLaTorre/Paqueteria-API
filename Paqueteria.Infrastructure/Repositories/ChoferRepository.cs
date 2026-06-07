using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class ChoferRepository(AppDbContext context) : IChoferRepository
{
    public async Task<Chofer?> GetByIdAsync(Guid choferId, Guid empresaId, bool asTracking = true)
    {
        IQueryable<Chofer> query = context.Choferes;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(c => c.Id == choferId && c.EmpresaId == empresaId);
    }
    
    public async Task<IReadOnlyList<Chofer>> GetAllAsync(Guid empresaId, bool asTracking = true)
    {
        IQueryable<Chofer> query = context.Choferes;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query
            .Where(c => c.EmpresaId == empresaId)
            .ToListAsync();
    }
    
    public async Task<Chofer> AddAsync(Chofer entity)
    {
        var ruta = await context.Choferes.AddAsync(entity);
        return ruta.Entity;
    }

    public void Delete(Chofer entity)
    {
        context.Choferes.Remove(entity);
    }
}