using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.DTOs;

public record UsuarioCreateDto(
    string Nombre,
    string Username,
    string Password,
    RolUsuario Rol
)
{
    public Usuario ToEntity() => new (Nombre, Username, Password, Rol);
};

public record UsuarioUpdateDto(
    Guid UsuarioId,
    string Nombre,
    RolUsuario Rol
)
{
    public void UpdateEntity(Usuario entity)
    {
        entity.Nombre = Nombre;
        entity.Rol = Rol;
    }
};

public record UsuarioResponseDto(
    Guid Id,
    string Nombre,
    string Username,
    RolUsuario Rol,
    DateTime? FechaUltimoAcesso
)
{
    public static UsuarioResponseDto FromEntity(Usuario entity) =>
        new (
            entity.Id,
            entity.Nombre,
            entity.Username,
            entity.Rol,
            entity.FechaUltimoAcceso
        );
};