using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Sesion.Dtos;

public record SesionRespuestaDto(
    [property: Required] string Username,
    [property: Required] string Nombre,
    [property: Required] List<string> Permisos,
    [property: Required] string Token,
    [property: Required] DateTime Expiracion
);