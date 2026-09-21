using System.Linq.Expressions;
using Paqueteria.Comun.Dtos.Permisos;
using Paqueteria.Dominio.Entidades.Sistema;

namespace Paqueteria.Aplicacion.Modulos.Permisos;

public static class PermisoMapeador
{
    extension(Permiso permiso)
    {
        public PermisoRespuestaDto MapeaRespuestaDto()
        {
            return new PermisoRespuestaDto(
                permiso.Id,
                permiso.Nombre,
                permiso.Descripcion ?? string.Empty
            );
        }
    }
    
    public static Expression<Func<Permiso, PermisoRespuestaDto>> AProyeccionRespuesta => entidad => new PermisoRespuestaDto(
        entidad.Id,
        entidad.Nombre,
        entidad.Descripcion ?? string.Empty
    );
}