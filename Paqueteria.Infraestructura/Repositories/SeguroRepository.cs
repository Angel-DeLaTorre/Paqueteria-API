using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Enums;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class SeguroRepository(AppDbContext context) : ISeguroRepository
{
    public async Task<Seguro?> ObtenerPorIdAsync(Guid seguroId, Guid empresaId, bool asTracking = true)
    {
        IQueryable<Seguro> query = context.Seguros;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(s => s.Id == seguroId && s.EmpresaId == empresaId);
    }
    
    public async Task<IReadOnlyList<Seguro>> ObtenerTodosAsync(Guid empresaId, bool asTracking = true)
    {
        IQueryable<Seguro> query = context.Seguros;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query
            .Where(s => s.EmpresaId == empresaId && s.Estatus == EstatusBasico.Activo)
            .ToListAsync();
    }
    
    public async Task<Seguro> AgregarAsync(Seguro entity)
    {
        var ruta = await context.Seguros.AddAsync(entity);
        return ruta.Entity;
    }

    public void Eliminar(Seguro entity)
    {
        context.Seguros.Remove(entity);
    }
}