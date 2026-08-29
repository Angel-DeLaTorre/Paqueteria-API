using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Modulos.Roles.Dtos;

namespace Paqueteria.Application.Modulos.Usuarios.Dtos;

public record UsuarioRespuestaDto
(
    [param: Required] Guid Id,
    [param: Required] string Nombre,
    [param: Required] string Username,
    List<RolRespuestaDto> Roles,
    DateTime? FechaUltimoAcesso
);