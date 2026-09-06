using System.Linq.Expressions;
using Paqueteria.Application.Modulos.Permisos;
using Paqueteria.Application.Modulos.Permisos.Dtos;
using Paqueteria.Application.Modulos.Roles.Dtos;
using Paqueteria.Dominio.Entidades.Sistema;

namespace Paqueteria.Application.Modulos.Roles;

public static class RolMapeador
{
    extension(Rol rol)
    {
        public RolRespuestaDto MapeaRespuestaDto()
        {
            var permisos = rol.Permisos?
                .Select( rp => rp.Permiso.MapeaRespuestaDto() )
                .ToList() ?? [];
            
            return new RolRespuestaDto(
                rol.Id,
                rol.Nombre,
                rol.Descripcion,
                permisos
            );


        }
    }
    // Proyección para consultas IQueryable
    public static Expression<Func<Rol, RolRespuestaDto>> AProyeccionRespuesta => entidad => new RolRespuestaDto(
        entidad.Id,
        entidad.Nombre,
        entidad.Descripcion ?? string.Empty,
        entidad.Permisos
            .Where(rp => rp.Permiso != null)
            .Select(rp => new PermisoRespuestaDto(
                rp.Permiso!.Id,
                rp.Permiso.Nombre,
                rp.Permiso.Descripcion ?? string.Empty
            ))
    );
}