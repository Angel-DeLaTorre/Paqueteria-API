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
    public Sucursal ToEntity() => new Sucursal(Nombre, Codigo, EsMatriz, Calle, Colonia, NumeroExterior, NumeroInterior,
        Localidad, MunicipioId, Telefono);
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
        if (Nombre != entity.Nombre)
            entity.Nombre = Nombre.Trim();
        if (Codigo != entity.Codigo)
            entity.Codigo = Codigo.Trim();
        if (EsMatriz != entity.EsMatriz)
            entity.EsMatriz = EsMatriz;
        if (Calle != entity.Calle)
            entity.Calle = Calle.Trim();
        if (Colonia != entity.Colonia)
            entity.Colonia = Colonia;
        if (NumeroExterior != entity.NumeroExterior)
            entity.NumeroExterior = NumeroExterior.Trim();
        if (NumeroInterior != entity.NumeroInterior)
            entity.NumeroInterior = NumeroInterior.Trim();
        if (Localidad != entity.Localidad)
            entity.Localidad = Localidad.Trim();
        if (MunicipioId != entity.MunicipioId)
            entity.MunicipioId =  MunicipioId;
        if (Telefono != entity.Telefono)
            entity.Telefono = Telefono.Trim();
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
    string Municipio,
    Guid MunicipioId,
    string? Telefono,
    EstatusGenerico Estatus
)
{
    public static SucursalResponseDto FromEntity(Sucursal entity)
    {
        return new SucursalResponseDto(
            entity.Id,
            entity.Nombre,
            entity.Codigo,
            entity.Calle,
            entity.Colonia,
            entity.NumeroExterior,
            entity.NumeroInterior,
            entity.Localidad,
            entity.Municipio.Nombre,
            entity.Municipio.Id,
            entity.Telefono,
            entity.Estatus
        );
    }
};