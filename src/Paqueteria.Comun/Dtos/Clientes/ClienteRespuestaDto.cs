using Paqueteria.Comun.Enums;

namespace Paqueteria.Comun.Dtos.Clientes;

public record ClienteRespuestaDto
(
    Guid ClienteId,
    string Nombre,
    string? Rfc,
    EstatusBasico Estatus,
    string? Telefono,
    string? Telefono2,
    string? Correo,
    string? Contacto,
    string? NumConvenio,
    string? PolizaSeguro,
    IEnumerable<ClienteDireccionRespuestaDto>? Direcciones
);