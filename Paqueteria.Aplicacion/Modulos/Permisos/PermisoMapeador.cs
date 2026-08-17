using System.Linq.Expressions;
using Paqueteria.Application.Modulos.Permisos.Dtos;
using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Application.Modulos.Permisos;

public static class PermisoMapeador
{
    public static Expression<Func<Permiso, PermisoRespuestaDto>> AProyeccionRespuesta => entidad => new PermisoRespuestaDto(
        entidad.Id,
        entidad.Nombre,
        entidad.Descripcion ?? string.Empty
    );
    
    public static PermisoRespuestaDto ARespuestaDto(this Permiso entidad)
    {
        return new PermisoRespuestaDto(
            entidad.Id,
            entidad.Nombre,
            entidad.Descripcion ?? string.Empty
        );
    }
}