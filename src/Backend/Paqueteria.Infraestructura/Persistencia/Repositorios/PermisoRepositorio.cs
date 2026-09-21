using Microsoft.EntityFrameworkCore;
using Paqueteria.Dominio.Entidades.Sistema;
using Paqueteria.Dominio.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class PermisoRepositorio(AppDbContext context) : IPermisoRepositorio
{
    public async Task<Permiso?> ObtenerPorIdAsync(Guid id, Guid empresaId)
    {
        return await context.Permisos
            .FirstOrDefaultAsync(p => p.Id == id && ( p.EmpresaId == empresaId || p.Empresa == null ) );
    }
    
    public async Task<Permiso?> ObtenerPorNombreAsync(string nombre, Guid empresaId)
    {
        return await context.Permisos
            .FirstOrDefaultAsync(p => p.Nombre == nombre && ( p.EmpresaId == empresaId || p.Empresa == null ) );
    }
    
    public async Task<IEnumerable<Permiso>> ObtenerTodosAsync(Guid empresaId)
    {
        return await context.Permisos
            .Where(p => p.EmpresaId == empresaId || p.Empresa == null)
            .ToListAsync();
    }
    
    public async Task AgregarAsync(Permiso permission) => await context.Permisos.AddAsync(permission);
    public void Eliminar(Permiso permission) => context.Permisos.Remove(permission);
    
    public async Task<IReadOnlyList<Permiso>> ObtenerPorIdsYEmpresaAsync(IEnumerable<Guid> permisosIds, Guid empresaId)
    {
        return await context.Permisos
            .Where(p => permisosIds.Contains(p.Id) && p.EmpresaId == empresaId)
            .ToListAsync();
    }

}