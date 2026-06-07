using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Sat;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class ArticuloRepository(AppDbContext context) : IArticuloRepository
{
    public async Task<Articulo?> GetByIdAsync(string articuloId, bool asTracking = true)
    {
        IQueryable<Articulo> query = context.Articulos;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(a => a.Id == articuloId);
    }
    
    public async Task<IReadOnlyList<Articulo>> GetAllAsync(bool asTracking = true)
    {
        IQueryable<Articulo> query = context.Articulos;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query
            .ToListAsync();
    }
    
    public async Task<Articulo> AddAsync(Articulo entity)
    {
        var ruta = await context.Articulos.AddAsync(entity);
        return ruta.Entity;
    }

    public void Delete(Articulo entity)
    {
        context.Articulos.Remove(entity);
    }
}