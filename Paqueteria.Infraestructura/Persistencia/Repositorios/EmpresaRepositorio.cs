using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entidades.Remisiones;
using Paqueteria.Core.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class EmpresaRepositorio(AppDbContext context) : IEmpresaRepositorio
{
    public async Task<Empresa?> ObtenerPorIdAsync(Guid empresaId, bool asTracking = true)
    {
        IQueryable<Empresa> query = context.Empresas;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query.FirstOrDefaultAsync(s => s.Id == empresaId);
    }
    
    public async Task<IReadOnlyList<Empresa>> ObtenerTodosAsync(bool asTracking = true)
    {
        IQueryable<Empresa> query = context.Empresas;
        if (!asTracking)
            query = query.AsNoTracking();
        
        return await query
            .ToListAsync();
    }
    
    public async Task<Empresa> AgregarAsync(Empresa entity)
    {
        var ruta = await context.Empresas.AddAsync(entity);
        return ruta.Entity;
    }

    public void Eliminar(Empresa entity)
    {
        context.Empresas.Remove(entity);
    }
}