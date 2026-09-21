using Paqueteria.Comun.Dtos.Roles;

namespace Paqueteria.Comun.Dtos.Usuarios;

public record UsuarioRespuestaDto
(
    Guid Id,
    string Nombre,
    string Username,
    List<RolRespuestaDto> Roles,
    DateTime? FechaUltimoAcesso
);