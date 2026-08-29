using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Sesion.Dtos;

public record SesionRespuestaDto(
    [param: Required] string Username,
    [param: Required] string Nombre,
    [param: Required] List<string> Permisos,
    [param: Required] string Token,
    [param: Required] DateTime Expiracion
);