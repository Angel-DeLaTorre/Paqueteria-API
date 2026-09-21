namespace Paqueteria.Comun.Dtos.Sesion;

public record SesionRespuestaDto(
    string Username,
    string Nombre,
    List<string> Permisos,
    string Token,
    DateTime Expiracion
);