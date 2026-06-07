using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class GuiaRepository(AppDbContext context) : IGuiaRepository
{
    public async Task<Guia?> GetByIdAsync(Guid guiaId, Guid empresaId, bool asTracking = true)
    {
        IQueryable<Guia> query = context.Guias;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(g => g.Id == guiaId && g.EmpresaId == empresaId);
    }
    
    public async Task<IReadOnlyList<Guia>> GetAllAsync(Guid empresaId, bool asTracking = true)
    {
        IQueryable<Guia> query = context.Guias;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query
            .Where(c => c.EmpresaId == empresaId)
            .ToListAsync();
    }
    
    public async Task<Guia> AddAsync(Guia entity)
    {
        var ruta = await context.Guias.AddAsync(entity);
        return ruta.Entity;
    }

    public void Delete(Guia entity)
    {
        context.Guias.Remove(entity);
    }
}