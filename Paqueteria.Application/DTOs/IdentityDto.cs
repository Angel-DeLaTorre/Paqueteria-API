namespace Paqueteria.Application.DTOs;

public record LoginRequest(string Username, string Password);

public record AuthResponse(
    Guid Id,
    string Username,
    string Nombre,
    string Rol,
    string Token,
    DateTime Expiracion
);