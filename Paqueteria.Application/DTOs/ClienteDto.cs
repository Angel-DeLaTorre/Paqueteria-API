using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Application.DTOs;

public record ClienteCreateDto(
    string Nombre,
    string Rfc,
    string Telefono,
    string? Telefono2,
    string Correo,
    string Contacto,
    string? NumConvenio,
    string? PolizaSeguro,
    DireccionDto? DireccionC
)
{
    public Cliente ToClienteEntity(Guid empresaId) => Cliente.Create
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

    public DireccionCliente ToDireccionEntity(Guid clienteId)
    {
        if (DireccionC == null) return null!;
        
        var d = Direccion.Create(
            DireccionC.Calle,
            DireccionC.NumeroExterior,
            DireccionC.NumeroInterior,
            DireccionC.Colonia,
            DireccionC.CodigoPostal,
            DireccionC.Localidad,
            DireccionC.MunicipioId
        );
            
        return DireccionCliente.Create(d,clienteId);

    } 
};

public record ClienteUpdateDto(
    Guid ClienteId,
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
    Guid ClienteId,
    string Nombre,
    string? Rfc,
    string? Telefono,
    string? Telefono2,
    string? Correo,
    string? Contacto,
    string? NumConvenio,
    string? PolizaSeguro,
    IEnumerable<ClienteDireccionResponseDto> Direcciones
){
    public static ClienteResponseDto FromEntity(Cliente cliente)
    {
        return new ClienteResponseDto(
            cliente.Id,
            cliente.Nombre,
            cliente.Rfc,
            cliente.Telefono,
            cliente.Telefono2,
            cliente.Correo,
            cliente.Contacto,
            cliente.NumConvenio,
            cliente.PolizaSeguro,
            cliente.Direcciones?.Select(ClienteDireccionResponseDto.FromEntity).ToList() 
            ?? []
        );
    }
        
};

public record ClienteDireccionResponseDto(
    Guid IdDireccion,
    DireccionResponseDto Direccion
)
{
    public static ClienteDireccionResponseDto FromEntity(DireccionCliente entity)
    {
        var direccion = DireccionResponseDto.FromEntity(entity.Direccion);
        return new ClienteDireccionResponseDto(
            entity.Id,
            direccion
        );        
    }
}

