using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entidades.Sat;
using Paqueteria.Core.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class ArticuloRepositorio(AppDbContext context) : IArticuloRepositorio
{
    public async Task<Articulo?> ObtenerPorIdAsync(Guid articuloId, bool asTracking = true)
    {
        IQueryable<Articulo> query = context.Articulos;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(a => a.Id == articuloId);
    }
    
    public async Task<IReadOnlyList<Articulo>> ObtenerTodosAsync(bool asTracking = true)
    {
        IQueryable<Articulo> query = context.Articulos;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query
            .ToListAsync();
    }
    
    public async Task<Articulo> AgregarAsync(Articulo entity)
    {
        var ruta = await context.Articulos.AddAsync(entity);
        return ruta.Entity;
    }

    public void Eliminar(Articulo entity)
    {
        context.Articulos.Remove(entity);
    }
}