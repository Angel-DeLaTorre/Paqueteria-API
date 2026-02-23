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
    public Usuario ToEntity() => new Usuario(Nombre, Username, Password, Rol);
};

public record UsuarioUpdateDto(
    Guid UsuarioId,
    string? Nombre,
    RolUsuario? Rol
)
{
    public void UpdateEntity(Usuario entity)
    {
        if (!string.IsNullOrWhiteSpace(Nombre) && Nombre != entity.Nombre)
        {
            entity.Nombre = Nombre.Trim();
        }
        if (Rol.HasValue && Rol.Value != entity.Rol)
        {
            entity.Rol = Rol.Value;
        }
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
    public static UsuarioResponseDto FromEntity(Usuario entity)
    {
        return new UsuarioResponseDto(
            entity.Id,
            entity.Nombre,
            entity.Username,
            entity.Rol,
            entity.FechaUltimoAcceso
        );
    }
};