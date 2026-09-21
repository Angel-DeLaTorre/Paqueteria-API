namespace Paqueteria.Comun.Dtos.Clientes;

public record ClienteActualizarDto
(
    Guid ClienteId,
    string Nombre,
    string Rfc,
    string Telefono,
    string? Telefono2,
    string Correo,
    string Contacto,
    string? NumConvenio,
    string? PolizaSeguro
);