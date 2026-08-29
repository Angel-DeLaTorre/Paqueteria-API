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
    [param: Required] Guid SucursalId,
    [param: Required] string Nombre,
    [param: Required] string Codigo,
    [param: Required] bool EsMatriz,
    [param: Required] DireccionDto Direccion,
    [param: Required] string Telefono
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
    [param: Required] Guid SucursalId,
    [param: Required] string Nombre,
    [param: Required] string Codigo,
    [param: Required] bool EsMatriz,
    [param: Required] DireccionResponseDto Direccion,
    string? Telefono,
    [param: Required] EstatusBasico Estatus
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