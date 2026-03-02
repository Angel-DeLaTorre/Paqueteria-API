using Paqueteria.Core.Entities;

namespace Paqueteria.Application.DTOs;

public record DireccionGuiaSnapCreateDto(
    string Calle,
    string NumeroExterior,
    string? NumeroInterior,
    string Colonia,
    string CodigoPostal,
    string? Localidad,
    Guid MunicipioId
)
{
    public DireccionGuiaSnapshot ToEntity() =>
        new DireccionGuiaSnapshot
        {
            Calle = Calle,
            NumeroExterior = NumeroExterior,
            NumeroInterior = NumeroInterior,
            Colonia =  Colonia,
            CodigoPostal = CodigoPostal,
            Localidad =  Localidad,
            MunicipioId =  MunicipioId
        };
};

public record DireccionGuiaSnapUpdateDto(
    Guid DireccionGuiaId,
    string Calle,
    string NumeroExterior,
    string? NumeroInterior,
    string Colonia,
    string CodigoPostal,
    string? Localidad,
    Guid MunicipioId
)
{
    public void UpdateEntity(DireccionGuiaSnapshot entity)
    {
        entity.Calle = Calle;
        entity.NumeroExterior = NumeroExterior;
        entity.NumeroInterior = NumeroInterior;
        entity.Colonia = Colonia;
        entity.CodigoPostal = CodigoPostal;
        entity.Localidad = Localidad;
        entity.MunicipioId = MunicipioId;
    }
}

public record DireccionGuiaSnapResponseDto(
    Guid DireccionGuiaId,
    string Calle,
    string NumeroExterior,
    string? NumeroInterior,
    string Colonia,
    string CodigoPostal,
    string? Localidad,
    Guid MunicipioId,
    string MunicipioNombre
)
{
    public static DireccionGuiaSnapResponseDto FromEntity(DireccionGuiaSnapshot entity) =>
        new (
            entity.Id,
            entity.Calle,
            entity.NumeroExterior,
            entity.NumeroInterior,
            entity.Colonia,
            entity.CodigoPostal,
            entity.Localidad,
            entity.MunicipioId,
            entity.Municipio.Nombre
        );
}