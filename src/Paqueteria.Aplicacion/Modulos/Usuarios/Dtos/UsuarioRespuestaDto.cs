using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Modulos.Roles.Dtos;

namespace Paqueteria.Application.Modulos.Usuarios.Dtos;

public record UsuarioRespuestaDto
(
    Guid Id,
    string Nombre,
    string Username,
    List<RolRespuestaDto> Roles,
    DateTime? FechaUltimoAcesso
);