using Paqueteria.Core.Enums;

namespace Paqueteria.Application.DTOs;

public record UsuarioCreateDto(
    string Nombre,
    string Username,
    string Password,
    RolUsuario Rol
);

public record UsuarioUpdateDto(
    Guid UsuarioId,
    string? Nombre,
    RolUsuario? Rol
);

public record UsuarioResponseDto(
    Guid UsuarioId,
    string Nombre,
    string Username,
    RolUsuario Rol,
    DateTime FechaUltimoAcesso
);