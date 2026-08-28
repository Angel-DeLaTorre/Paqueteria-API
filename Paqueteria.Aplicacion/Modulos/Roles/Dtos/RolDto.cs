using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Modulos.Permisos.Dtos;
using Paqueteria.Core.Entidades.Sistema;

namespace Paqueteria.Application.Modulos.Roles.Dtos;

public record RolCreateDto(
    string Nombre,
    string Descripcion,
    List<Guid>? PermisosIds
)
{
    public Rol ToEntity(Guid empresaId) => Rol.Create(Nombre, Descripcion, empresaId);
};

public record RolUpdateDto(
    [property: Required] Guid RoleId,
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
    [property: Required]
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