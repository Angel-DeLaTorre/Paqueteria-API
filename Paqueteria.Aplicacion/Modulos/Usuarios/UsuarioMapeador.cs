using System.Linq.Expressions;
using Paqueteria.Application.Modulos.Permisos.Dtos;
using Paqueteria.Application.Modulos.Roles;
using Paqueteria.Application.Modulos.Roles.Dtos;
using Paqueteria.Application.Modulos.Usuarios.Dtos;
using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Application.Modulos.Usuarios;

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
                ur.Rol.RolPermiso
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
    public static UsuarioRespuestaDto ARespuestaDto(this Usuario entidad)
    {
        var roles = entidad.UsuarioRoles?
            .Where(ur => ur.Rol != null)
            .Select(ur => ur.Rol!.ARespuestaDto())
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