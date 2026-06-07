using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Application.DTOs;

public record PermisoCreateDto(
    string Nombre,
    string Descripcion
)
{
    public Permiso ToEntity(Guid empresaId) => Permiso.Create(Nombre, Descripcion, empresaId);
};

public record PermisoUpdateDto(
    Guid PermisoId,
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
    Guid PermisoId,
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