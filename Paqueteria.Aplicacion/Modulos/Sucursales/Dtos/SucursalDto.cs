using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Comun.Dtos;
using Paqueteria.Core.Entidades.Remisiones;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.Modulos.Sucursales.Dtos;

public record SucursalCreateDto(
    string Nombre,
    string Codigo,
    bool EsMatriz,
    DireccionDto Direccion,
    string Telefono
)
{
    public Sucursal ToEntity(Guid empresaId)
    {
        var direccion = Direccion.ToEntity();
        return Sucursal.Create
        (
            Nombre,
            Codigo,
            EsMatriz,
            direccion,
            Telefono,
            empresaId
        );
    }
};

public record SucursaUpdateDto(
    [property: Required] Guid SucursalId,
    [property: Required] string Nombre,
    [property: Required] string Codigo,
    [property: Required] bool EsMatriz,
    [property: Required] DireccionDto Direccion,
    [property: Required] string Telefono
)
{
    public void UpdateEntity(Sucursal entity)
    {
        entity.Nombre = Nombre;
        entity.Codigo = Codigo;
        entity.EsMatriz = EsMatriz;
        entity.Telefono = Telefono;
        entity.Direccion = Direccion.ToEntity();
    }
};

public record SucursalResponseDto(
    [property: Required] Guid SucursalId,
    [property: Required] string Nombre,
    [property: Required] string Codigo,
    [property: Required] bool EsMatriz,
    [property: Required] DireccionResponseDto Direccion,
    string? Telefono,
    [property: Required] EstatusBasico Estatus
)
{
    public static SucursalResponseDto FromEntity(Sucursal entity)
    {
        var dir = DireccionResponseDto.FromEntity(entity.Direccion);
        
        return new SucursalResponseDto(
            entity.Id,
            entity.Nombre,
            entity.Codigo,
            entity.EsMatriz,
            dir,
            entity.Telefono,
            entity.Estatus
        );
    }
        
};