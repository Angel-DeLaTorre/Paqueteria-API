using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Comun.Dtos;
using Paqueteria.Dominio.Enums;

namespace Paqueteria.Application.Modulos.Clientes.Dtos;

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