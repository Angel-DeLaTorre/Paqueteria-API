using System.Linq.Expressions;
using Paqueteria.Application.Modulos.Permisos;
using Paqueteria.Application.Modulos.Permisos.Dtos;
using Paqueteria.Application.Modulos.Roles.Dtos;
using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Application.Modulos.Roles;

public static class RolMapeador
{
    // Proyección para consultas IQueryable
    public static Expression<Func<Rol, RolRespuestaDto>> AProyeccionRespuesta => entidad => new RolRespuestaDto(
        entidad.Id,
        entidad.Nombre,
        entidad.Descripcion ?? string.Empty,
        entidad.RolPermiso
            .Where(rp => rp.Permiso != null)
            .Select(rp => new PermisoRespuestaDto(
                rp.Permiso!.Id,
                rp.Permiso.Nombre,
                rp.Permiso.Descripcion ?? string.Empty
            ))
    );
    
    // Método de extensión para objetos en memoria
    public static RolRespuestaDto ARespuestaDto(this Rol entidad)
    {
        var permisos = entidad.RolPermiso?
            .Where(rp => rp.Permiso != null)
            .Select(rp => rp.Permiso!.ARespuestaDto()) ?? [];

        return new RolRespuestaDto(
            entidad.Id,
            entidad.Nombre,
            entidad.Descripcion ?? string.Empty,
            permisos
        );
    }
}