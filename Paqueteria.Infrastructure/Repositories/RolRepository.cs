using Microsoft.EntityFrameworkCore;
using Paqueteria.Core.Entities.Sistema;
using Paqueteria.Core.Interfaces.Repositories;
using Paqueteria.Infrastructure.Data;

namespace Paqueteria.Infrastructure.Repositories;

public class RolRepository (AppDbContext context) : IRolRepository
{
    public async Task<Rol?> GetByIdAsync(Guid id, Guid empresaId, bool includePermissions = false)
    {
        var query = context.Roles.AsQueryable();

        if (includePermissions)
        {
            query = query
                .Include(r => r.RolPermiso)
                .ThenInclude(rp => rp.Permiso);
        }

        return await query.FirstOrDefaultAsync(r => r.Id == id && r.EmpresaId == empresaId);
    }
    
    public async Task<Rol?> GetByNameAsync(string name, Guid empresaId)
    {
        return await context.Roles
            .FirstOrDefaultAsync(r => r.Nombre == name && r.EmpresaId == empresaId);
    }
    
    public async Task<IEnumerable<Rol>> GetAllAsync(Guid empresaId)
    {
        return await context.Roles
            .Where(r => r.EmpresaId == empresaId)
            .ToListAsync();
    }
    
    public async Task AddAsync(Rol role)
    {
        if (role.EmpresaId == Guid.Empty)
        {
            throw new ArgumentException("El EmpresaId es obligatorio para registrar un rol.");
        }

        await context.Roles.AddAsync(role);
    }
    
    public void Delete(Rol role)
    {
        context.Roles.Remove(role);
    }
    
    public async Task AddPermissionToRoleAsync(Guid roleId, Guid permissionId, Guid empresaId)
    {
        var permissionExists = await context.Permisos
            .AnyAsync(p => p.Id == permissionId && p.EmpresaId == empresaId);

        var roleExists = await context.Roles
            .AnyAsync(r => r.Id == roleId && r.EmpresaId == empresaId);

        if (!permissionExists || !roleExists)
        {
            throw new InvalidOperationException("El Rol o el Permiso no existen o no pertenecen a esta empresa.");
        }
        
        var alreadyExists = await context.RolPermiso
            .AnyAsync(rp => rp.RolId == roleId && rp.PermisoId == permissionId);

        if (!alreadyExists)
        {
            var rolPermiso = RolPermiso.Create(roleId, permissionId);
            await context.RolPermiso.AddAsync(rolPermiso);
        }
    }
    
    public async Task RemovePermissionFromRoleAsync(Guid roleId, Guid permissionId, Guid empresaId)
    {
        // Validamos la existencia bajo el contexto de la empresa a través del Rol
        var rolePermission = await context.RolPermiso
            .FirstOrDefaultAsync(rp => rp.RolId == roleId && 
                                      rp.PermisoId == permissionId && 
                                      rp.Rol.EmpresaId == empresaId);

        if (rolePermission != null)
        {
            context.RolPermiso.Remove(rolePermission);
        }
    }
}