using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.DTOs;

public abstract record SucursalCreateDto(
    string Nombre,
    string Codigo,
    bool EsMatriz,
    string Calle,
    string Colonia,
    string NumeroExterior,
    string? NumeroInterior,
    string? Localidad,
    Guid MunicipioId,
    string Telefono
)
{
    public Sucursal ToEntity() =>
        new Sucursal
        {
            Nombre = Nombre,
            Codigo = Codigo,
            EsMatriz = EsMatriz,
            Calle = Calle,
            Colonia = Colonia,
            NumeroExterior = NumeroExterior,
            NumeroInterior = NumeroInterior,
            Localidad = Localidad,
            MunicipioId = MunicipioId,
            Telefono = Telefono
        };
};

public record SucursaUpdateDto(
    Guid SucursalId,
    string Nombre,
    string Codigo,
    bool EsMatriz,
    string Calle,
    string Colonia,
    string NumeroExterior,
    string NumeroInterior,
    string Localidad,
    Guid MunicipioId,
    string Telefono
)
{
    public void UpdateEntity(Sucursal entity)
    {
        entity.Nombre = Nombre;
        entity.Codigo = Codigo;
        entity.EsMatriz = EsMatriz;
        entity.Calle = Calle;
        entity.Colonia = Colonia;
        entity.NumeroExterior = NumeroExterior;
        entity.NumeroInterior = NumeroInterior;
        entity.Localidad = Localidad;
        entity.MunicipioId =  MunicipioId;
        entity.Telefono = Telefono;
    }
};

public record SucursalResponseDto(
    Guid SucursalId,
    string Nombre,
    string Codigo,
    string Calle,
    string Colonia,
    string NumeroExterior,
    string? NumeroInterior,
    string? Localidad,
    Guid MunicipioId,
    string MunicipioNombre,
    string? Telefono,
    EstatusGenerico Estatus
)
{
    public static SucursalResponseDto FromEntity(Sucursal entity) =>
        new
        (
            entity.Id,
            entity.Nombre,
            entity.Codigo,
            entity.Calle,
            entity.Colonia,
            entity.NumeroExterior,
            entity.NumeroInterior,
            entity.Localidad,
            entity.MunicipioId,
            entity.Municipio?.Nombre ?? "",
            entity.Telefono,
            entity.Estatus
        );
};