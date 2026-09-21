using Microsoft.EntityFrameworkCore;
using Paqueteria.Comun.Enums;
using Paqueteria.Dominio.Entidades.Sistema;
using Paqueteria.Dominio.Interfaces.Repositorios;

namespace Paqueteria.Infrastructure.Persistencia.Repositorios;

public class UsuarioRepositorio(AppDbContext context) : IUsuarioRepositorio
{
    public async Task<Usuario?> ObtenerPorIdAsync(Guid id, Guid empresaId, bool includeRoles = false)
    {
        var query = context.Usuarios.AsQueryable();

        if (includeRoles)
        {
            query = query
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol);
        }

        return await query.FirstOrDefaultAsync(u => u.Id == id && u.EmpresaId == empresaId);
    }
    public async Task<Usuario?> ObtenerPorUsernameAsync(string username, Guid empresaId, bool includeRolesAndPermissions = false)
    {
        var query = context.Usuarios.AsQueryable();

        if (includeRolesAndPermissions)
        {
            query = query
                .Include(u => u.UsuarioRoles)
                .ThenInclude(ur => ur.Rol)
                .ThenInclude(r => r.Permisos)
                .ThenInclude(rp => rp.Permiso);
        }
        
        return await query.FirstOrDefaultAsync(u => u.Username == username && u.EmpresaId == empresaId);
    }
    
    public async Task<Usuario?> ObtenerPorUsernameAsync(string username, bool includeRolesAndPermissions = false)
    {
        var query = context.Usuarios.AsQueryable();

        if (includeRolesAndPermissions)
        {
            query = query
                .Include(u => u.UsuarioRoles 
                    .Where(ur => ur.Rol.Estatus == EstatusBasico.Activo) )
                .ThenInclude(ur => ur.Rol)
                .ThenInclude(r => r.Permisos 
                    .Where(ur => ur.Permiso.Estatus == EstatusBasico.Activo))
                .ThenInclude(rp => rp.Permiso);
        }
        
        return await query.FirstOrDefaultAsync(u => u.Username == username);
    }
    
    public async Task<IEnumerable<Usuario>> ObtenerTodosAsync(Guid empresaId)
    {
        return await context.Usuarios
            .Where(u => u.EmpresaId == empresaId)
            .Include(u => u.UsuarioRoles)
            .ThenInclude(ur => ur.Rol)
            .ThenInclude(r => r.Permisos)
            .ThenInclude(rp => rp.Permiso)
            .ToListAsync();
    }
    
    public async Task AgregarAsync(Usuario user)
    {
        if (user.EmpresaId == Guid.Empty)
        {
            throw new ArgumentException("El EmpresaId es obligatorio para registrar un usuario.");
        }
        await context.Usuarios.AddAsync(user);
    }
    
    public void Eliminar(Usuario user)
    {
        context.Usuarios.Remove(user);
    }
    
    public async Task AgregarRolAlUsuarioAsync(Guid userId, Guid roleId, Guid empresaId)
    {
        //var userExists = await context.Usuarios.AnyAsync(u => u.Id == userId && u.EmpresaId == empresaId);
        var roleExists = await context.Roles.AnyAsync(r => r.Id == roleId && r.EmpresaId == empresaId);

        if (!roleExists)
        {
            throw new InvalidOperationException("El Usuario o el Rol no existen");
        }
        
        var alreadyHasRole = await context.UsuarioRol
            .AnyAsync(ur => ur.UsuarioId == userId && ur.RolId == roleId);

        if (!alreadyHasRole)
        {
            var usuarioRol = UsuarioRol.Create(userId, roleId);
            await context.UsuarioRol.AddAsync(usuarioRol);
        }
    }
    
    public async Task RemoverRolAlUsuarioAsync(Guid userId, Guid roleId, Guid empresaId)
    {
        var userRole = await context.UsuarioRol
            .FirstOrDefaultAsync(ur => ur.UsuarioId == userId && 
                                      ur.RolId == roleId && 
                                      ur.Usuario.EmpresaId == empresaId);

        if (userRole != null)
        {
            context.UsuarioRol.Remove(userRole);
        }
    }
}