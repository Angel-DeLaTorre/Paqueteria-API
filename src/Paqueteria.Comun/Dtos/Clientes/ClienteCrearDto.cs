using Paqueteria.Comun.Dtos.Comun;
using Paqueteria.Comun.Enums;

namespace Paqueteria.Comun.Dtos.Clientes;

public record ClienteCrearDto
(
    string Nombre,
    string Rfc,
    TipoPersona TipoPersona,
    string Telefono,
    string? Telefono2,
    string Correo,
    string Contacto,
    string? NumConvenio,
    string? PolizaSeguro,
    DireccionDto? DireccionC
);