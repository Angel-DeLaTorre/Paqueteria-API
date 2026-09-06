using System.ComponentModel.DataAnnotations;
using Paqueteria.Dominio.Enums;

namespace Paqueteria.Application.Modulos.Clientes.Dtos;

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