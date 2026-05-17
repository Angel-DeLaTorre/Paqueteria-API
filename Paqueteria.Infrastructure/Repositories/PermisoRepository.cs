using Microsoft.EntityFrameworkCore;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class PermisoRepository(AppDbContext context) : IPermisoRepository
{
    public async Task<Permiso?> GetByIdAsync(Guid id, Guid empresaId)
    {
        return await context.Permisos
            .FirstOrDefaultAsync(p => p.Id == id && p.EmpresaId == empresaId);
    }
    
    public async Task<Permiso?> GetByNameAsync(string name, Guid empresaId)
    {
        return await context.Permisos
            .FirstOrDefaultAsync(p => p.Nombre == name && p.EmpresaId == empresaId);
    }
    
    public async Task<IEnumerable<Permiso>> GetAllAsync(Guid empresaId)
    {
        return await context.Permisos
            .Where(p => p.EmpresaId == empresaId)
            .ToListAsync();
    }
    
    public async Task AddAsync(Permiso permission)
    {
        if (permission.EmpresaId == Guid.Empty)
        {
            throw new ArgumentException("El EmpresaId es obligatorio para registrar un permiso.");
        }

        await context.Permisos.AddAsync(permission);
    }
    
    public void Delete(Permiso permission)
    {
        context.Permisos.Remove(permission);
    }
}