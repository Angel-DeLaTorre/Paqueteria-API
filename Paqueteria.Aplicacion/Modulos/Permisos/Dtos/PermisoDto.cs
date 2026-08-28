using System.ComponentModel.DataAnnotations;
using Paqueteria.Core.Entidades.Sistema;

namespace Paqueteria.Application.Modulos.Permisos.Dtos;

public record PermisoCreateDto(
    string Nombre,
    string Descripcion
)
{
    public Permiso ToEntity(Guid empresaId) => Permiso.Create(Nombre, Descripcion, empresaId);
};

public record PermisoUpdateDto(
    [property: Required] Guid PermisoId,
    string Nombre,
    string Descripcion
)
{
    public void UpdateEntity(Permiso entity)
    {
        entity.Nombre = Nombre;
        entity.Descripcion = Descripcion;
    }
};

public record PermisoResponseDto(
    [property: Required] Guid PermisoId,
    string Nombre,
    string Descripcion
)
{
    public static PermisoResponseDto FromEntity(Permiso entity) =>
        new (
            entity.Id,
            entity.Nombre,
            entity.Descripcion
        );
};