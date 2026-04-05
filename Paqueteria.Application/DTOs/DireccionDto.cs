using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Catalogos;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Application.DTOs;

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
        new (
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
    string? MunicipioNombre
)
{
    public static DireccionResponseDto FromEntity(Direccion? direccion, Municipio? municipio) =>
        new(
            direccion?.Calle,
            direccion?.NumeroExterior,
            direccion?.NumeroInterior,
            direccion?.Colonia,
            direccion?.CodigoPostal,
            direccion?.Localidad,
            direccion?.MunicipioId,
            municipio?.Nombre
        );
}