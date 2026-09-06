using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Sesion.Dtos;

public record SesionRespuestaDto(
    string Username,
    string Nombre,
    List<string> Permisos,
    string Token,
    DateTime Expiracion
);