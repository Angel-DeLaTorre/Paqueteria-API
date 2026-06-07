using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Application.DTOs;

public record RolCreateDto(
    string Nombre,
    string Descripcion,
    List<Guid>? PermisosIds
)
{
    public Rol ToEntity(Guid empresaId) => Rol.Create(Nombre, Descripcion, empresaId);
};

public record RolUpdateDto(
    Guid RoleId,
    string Nombre,
    string Descripcion,
    List<Guid>? PermisosIds
)
{
    public void UpdateEntity(Rol entity)
    {
        entity.Nombre = Nombre;
        entity.Descripcion = Descripcion;
    }
};

public record RolResponseDto(
    Guid RolId,
    string Nombre,
    string Descripcion,
    IEnumerable<PermisoResponseDto> Permissions
)
{
    public static RolResponseDto FromEntity(Rol entity) =>
        new (
            entity.Id,
            entity.Nombre,
            entity.Descripcion,
            entity.RolPermiso?
                .Select(rp => PermisoResponseDto.FromEntity(rp.Permiso))
                .ToList() ?? new List<PermisoResponseDto>()
        );
};