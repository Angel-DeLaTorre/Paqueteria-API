using Paqueteria.Core.Entities;

namespace Paqueteria.Application.DTOs;

public record ClienteCreateDto(
    string Nombre,
    string Rfc,
    string Direccion,
    string? DireccionComplemento,
    string CodigoPostal,
    Guid MunicipioId,
    string Telefono,
    string? Telefono2,
    string Correo,
    string Contacto,
    string? NumConvenio,
    string? PolizaSeguro,
    Guid SucursalId
)
{
    public Cliente ToEntity() => new Cliente
    {
        Nombre = Nombre,
        Rfc = Rfc,
        Direccion = Direccion,
        DireccionComplemento = DireccionComplemento,
        CodigoPostal = CodigoPostal,
        MunicipioId = MunicipioId,
        Telefono = Telefono,
        Telefono2 = Telefono2,
        Correo = Correo,
        Contacto = Contacto,
        NumConvenio = NumConvenio,
        PolizaSeguro = PolizaSeguro,
        SucursalId = SucursalId
    };
};

public record ClienteUpdateDto(
    Guid IdCliente,
    string Nombre,
    string Rfc,
    string Direccion,
    string? DireccionComplemento,
    string CodigoPostal,
    Guid MunicipioId,
    string Telefono,
    string? Telefono2,
    string Correo,
    string Contacto,
    string? NumConvenio,
    string? PolizaSeguro,
    Guid SucursalId
)
{
    public void UpdateEntity(Cliente cliente)
    {
        cliente.Nombre = Nombre;
        cliente.Rfc = Rfc;
        cliente.Direccion = Direccion;
        cliente.DireccionComplemento = DireccionComplemento;
        cliente.CodigoPostal = CodigoPostal;
        cliente.MunicipioId = MunicipioId;
        cliente.Telefono = Telefono;
        cliente.Telefono2 = Telefono2;
        cliente.Correo = Correo;
        cliente.Contacto = Contacto;
        cliente.NumConvenio = NumConvenio;
        cliente.PolizaSeguro = PolizaSeguro;
        cliente.SucursalId = SucursalId;
    }
};

public record ClienteResponseDto(
    Guid IdCliente,
    string NombreCompleto
){
    public static ClienteResponseDto FromEntity(Cliente cliente) =>
        new (
            cliente.Id,
            cliente.Nombre
        );
};