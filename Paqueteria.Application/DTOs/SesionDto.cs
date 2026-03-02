using Microsoft.IdentityModel.Tokens;
using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.DTOs;

public record LoginRequestDto(string Username, string Password);

public record SesionResponseDto(
    string Username,
    string Nombre,
    RolUsuario Rol,
    SecurityToken Token,
    DateTime? Expiracion
)
{
    public static SesionResponseDto FromEntity(Usuario entity, SecurityToken token, DateTime? expiracion) =>
        new(
            entity.Username,
            entity.Nombre,
            entity.Rol,
            token,
            expiracion
        );
};