using System.Linq.Expressions;
using Paqueteria.Aplicacion.Modulos.Roles;
using Paqueteria.Comun.Dtos.Permisos;
using Paqueteria.Comun.Dtos.Roles;
using Paqueteria.Comun.Dtos.Usuarios;
using Paqueteria.Dominio.Entidades.Sistema;

namespace Paqueteria.Aplicacion.Modulos.Usuarios;

public static class UsuarioMapeador
{
    // Proyección para consultas IQueryable
    public static Expression<Func<Usuario, UsuarioRespuestaDto>> AProyeccionRespuesta => entidad => new UsuarioRespuestaDto(
        entidad.Id,
        entidad.Nombre,
        entidad.Username,
        entidad.UsuarioRoles
            .Where(ur => ur.Rol != null)
            .Select(ur => new RolRespuestaDto(
                ur.Rol!.Id,
                ur.Rol.Nombre,
                ur.Rol.Descripcion ?? string.Empty,
                ur.Rol.Permisos
                    .Where(rp => rp.Permiso != null)
                    .Select(rp => new PermisoRespuestaDto(
                        rp.Permiso!.Id,
                        rp.Permiso.Nombre,
                        rp.Permiso.Descripcion ?? string.Empty
                    ))
            ))
            .ToList(),
        entidad.FechaUltimoAcceso
    );

    // Método de extensión para objetos en memoria
    public static UsuarioRespuestaDto MapeaRespuestaDto(this Usuario entidad)
    {
        
        var roles = entidad.UsuarioRoles?
            .Where(ur => ur.Rol != null)
            .Select<UsuarioRol, RolRespuestaDto>(ur => ur.Rol!.MapeaRespuestaDto())
            .ToList() ?? [];

        return new UsuarioRespuestaDto(
            entidad.Id,
            entidad.Nombre,
            entidad.Username,
            roles,
            entidad.FechaUltimoAcceso
        );
    }
}