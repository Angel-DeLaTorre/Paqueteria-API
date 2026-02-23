using Paqueteria.Core.Entities;

namespace Paqueteria.Application.DTOs;

public record ClienteCreateDto(
    string Nombre,
    string Rfc,
    string Direccion,
    string? DireccionComplemento,
    string CodigoPostal,
    string IdMunicipio,
    string Telefono,
    string? Telefono2,
    string Correo,
    string Contacto,
    string? NumConvenio,
    string? PolizaSeguro,
    string IdSucursal
);

public record ClienteUpdateDto(
    Guid IdCliente,
    string Nombre,
    string Rfc,
    string Direccion,
    string? DireccionComplemento,
    string CodigoPostal,
    string IdMunicipio,
    string Telefono,
    string? Telefono2,
    string Correo,
    string Contacto,
    string? NumConvenio,
    string? PolizaSeguro,
    string IdSucursal
);

public record ClienteResponseDto(
    Guid IdCliente,
    string NombreCompleto
){
    public static ClienteResponseDto FromEntity(Cliente cliente)
    {
        return new ClienteResponseDto(
            cliente.Id,
            cliente.Nombre
        );
    }
};