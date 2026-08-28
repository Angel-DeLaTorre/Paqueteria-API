using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entidades.Remisiones;
using Paqueteria.Core.Enums;
using Paqueteria.Core.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class ChoferRepositorio(AppDbContext context) : IChoferRepositorio
{
    public async Task<Chofer?> ObtenerPorIdAsync(Guid choferId, Guid empresaId, bool asTracking = true)
    {
        IQueryable<Chofer> query = context.Choferes;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(c => c.Id == choferId && c.EmpresaId == empresaId);
    }
    
    public async Task<IReadOnlyList<Chofer>> ObtenerTodosAsync(Guid empresaId, bool asTracking = true)
    {
        IQueryable<Chofer> query = context.Choferes;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query
            .Where( c => c.EmpresaId == empresaId && c.Estatus == EstatusBasico.Activo )
            .ToListAsync();
    }
    
    public async Task<Chofer> AgregarAsync(Chofer entity)
    {
        var ruta = await context.Choferes.AddAsync(entity);
        return ruta.Entity;
    }

    public void Eliminar(Chofer entity)
    {
        context.Choferes.Remove(entity);
    }
}