using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.DTOs;

public record ClienteCreateDto(
    string Nombre,
    string Rfc,
    string Telefono,
    string? Telefono2,
    string Correo,
    string Contacto,
    string? NumConvenio,
    string? PolizaSeguro
)
{
    public Cliente ToEntity(Guid empresaId) => Cliente.Create
    (
        Nombre,
        Rfc,
        Telefono,
        Telefono2,
        Correo,
        Contacto,
        NumConvenio,
        PolizaSeguro,
        empresaId
    );
};

public record ClienteUpdateDto(
    Guid IdCliente,
    string Nombre,
    string Rfc,
    string Telefono,
    string? Telefono2,
    string Correo,
    string Contacto,
    string? NumConvenio,
    string? PolizaSeguro
)
{
    public void UpdateEntity(Cliente cliente)
    {
        cliente.Nombre = Nombre;
        cliente.Rfc = Rfc;
        cliente.Telefono = Telefono;
        cliente.Telefono2 = Telefono2;
        cliente.Correo = Correo;
        cliente.Contacto = Contacto;
        cliente.NumConvenio = NumConvenio;
        cliente.PolizaSeguro = PolizaSeguro;
    }
};

public record ClienteResponseDto(
    Guid IdCliente,
    string Nombre,
    string? Rfc,
    string? Telefono,
    string? Telefono2,
    string? Correo,
    string? Contacto,
    string? NumConvenio,
    string? PolizaSeguro
){
    public static ClienteResponseDto FromEntity(Cliente cliente) =>
        new (
            cliente.Id,
            cliente.Nombre,
            cliente.Rfc,
            cliente.Telefono,
            cliente.Telefono2,
            cliente.Correo,
            cliente.Contacto,
            cliente.NumConvenio,
            cliente.PolizaSeguro
        );
};