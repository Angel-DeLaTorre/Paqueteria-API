using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Application.Comun.Dtos;

public record DireccionDto(
    string Calle,
    string NumeroExterior,
    string? NumeroInterior,
    string Colonia,
    string CodigoPostal,
    string? Localidad,
    Guid MunicipioId
)
{
    public Direccion ToEntity() => 
        Direccion.Create(
            Calle,
            NumeroExterior,
            NumeroInterior,
            Colonia,
            CodigoPostal,
            Localidad,
            MunicipioId
        );
};

public record DireccionResponseDto(
    string? Calle,
    string? NumeroExterior,
    string? NumeroInterior,
    string? Colonia,
    string? CodigoPostal,
    string? Localidad,
    Guid? MunicipioId,
    string? MunicipioNombre,
    string? Estado
)
{
    public static DireccionResponseDto FromEntity(Direccion? direccion) =>
        new(
            direccion?.Calle,
            direccion?.NumeroExterior,
            direccion?.NumeroInterior,
            direccion?.Colonia,
            direccion?.CodigoPostal,
            direccion?.Localidad,
            direccion?.MunicipioId,
            direccion?.Municipio?.Nombre,
            direccion?.Municipio?.EstadoId
        );
}