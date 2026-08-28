using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entidades.Sistema;
using Paqueteria.Core.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class PermisoRepositorio(AppDbContext context) : IPermisoRepositorio
{
    public async Task<Permiso?> ObtenerPorIdAsync(Guid id, Guid empresaId)
    {
        return await context.Permisos
            .FirstOrDefaultAsync(p => p.Id == id && p.EmpresaId == empresaId);
    }
    
    public async Task<Permiso?> ObtenerPorNombreAsync(string nombre, Guid empresaId)
    {
        return await context.Permisos
            .FirstOrDefaultAsync(p => p.Nombre == nombre && p.EmpresaId == empresaId);
    }
    
    public async Task<IEnumerable<Permiso>> ObtenerTodosAsync(Guid empresaId)
    {
        return await context.Permisos
            .Where(p => p.EmpresaId == empresaId)
            .ToListAsync();
    }
    
    public async Task AgregarAsync(Permiso permission)
    {
        await context.Permisos.AddAsync(permission);
    }
    
    public void Eliminar(Permiso permission)
    {
        context.Permisos.Remove(permission);
    }
}