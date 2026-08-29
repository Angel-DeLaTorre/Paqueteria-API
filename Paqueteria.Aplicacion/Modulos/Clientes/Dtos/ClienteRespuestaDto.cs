using System.ComponentModel.DataAnnotations;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.Modulos.Clientes.Dtos;

public record ClienteRespuestaDto
(
    [param: Required] Guid ClienteId,
    [param: Required] string Nombre,
    string? Rfc,
    [param: Required] EstatusBasico Estatus,
    string? Telefono,
    string? Telefono2,
    string? Correo,
    string? Contacto,
    string? NumConvenio,
    string? PolizaSeguro,
    IEnumerable<ClienteDireccionRespuestaDto>? Direcciones
);