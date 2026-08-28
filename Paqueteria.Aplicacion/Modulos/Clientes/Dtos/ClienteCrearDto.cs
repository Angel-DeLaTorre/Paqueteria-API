using Paqueteria.Application.Comun.Dtos;
using Paqueteria.Application.Dtos;

namespace Paqueteria.Application.Modulos.Clientes.Dtos;

public record ClienteCrearDto
(
    string Nombre,
    string Rfc,
    string Telefono,
    string? Telefono2,
    string Correo,
    string Contacto,
    string? NumConvenio,
    string? PolizaSeguro,
    DireccionDto? DireccionC
);