using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class EmpresaRepository(AppDbContext context) : IEmpresaRepository
{
    public async Task<Empresa?> GetByIdAsync(Guid empresaId, bool asTracking = true)
    {
        IQueryable<Empresa> query = context.Empresas;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(s => s.Id == empresaId);
    }
    
    public async Task<IReadOnlyList<Empresa>> GetAllAsync(bool asTracking = true)
    {
        IQueryable<Empresa> query = context.Empresas;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query
            .ToListAsync();
    }
    
    public async Task<Empresa> AddAsync(Empresa entity)
    {
        var ruta = await context.Empresas.AddAsync(entity);
        return ruta.Entity;
    }

    public void Delete(Empresa entity)
    {
        context.Empresas.Remove(entity);
    }
}