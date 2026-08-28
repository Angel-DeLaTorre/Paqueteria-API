using System.ComponentModel.DataAnnotations;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.Modulos.Clientes.Dtos;

public record ClienteRespuestaDto
(
    [property: Required] Guid ClienteId,
    [property: Required] string Nombre,
    string? Rfc,
    [property: Required] EstatusBasico Estatus,
    string? Telefono,
    string? Telefono2,
    string? Correo,
    string? Contacto,
    string? NumConvenio,
    string? PolizaSeguro,
    IEnumerable<ClienteDireccionRespuestaDto>? Direcciones
);