using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Modulos.Roles.Dtos;

namespace Paqueteria.Application.Modulos.Usuarios.Dtos;

public record UsuarioRespuestaDto
(
    [property: Required] Guid Id,
    [property: Required] string Nombre,
    [property: Required] string Username,
    List<RolRespuestaDto> Roles,
    DateTime? FechaUltimoAcesso
);