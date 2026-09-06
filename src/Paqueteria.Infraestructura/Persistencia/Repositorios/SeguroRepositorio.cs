using Microsoft.EntityFrameworkCore;
using Paqueteria.Dominio.Entidades.Remisiones;
using Paqueteria.Dominio.Enums;
using Paqueteria.Dominio.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class SeguroRepositorio(AppDbContext context) : ISeguroRepositorio
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