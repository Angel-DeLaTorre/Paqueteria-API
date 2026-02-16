namespace Paqueteria.Application.DTOs;

public record LoginRequestDto(string Username, string Password);

public record SesionResponseDto(
    Guid Id,
    string Username,
    string Nombre,
    string Rol,
    string Token,
    DateTime Expiracion
);