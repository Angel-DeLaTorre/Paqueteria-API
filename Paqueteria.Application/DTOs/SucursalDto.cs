using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.DTOs;

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
    Guid SucursalId,
    string Nombre,
    string Codigo,
    bool EsMatriz,
    DireccionDto Direccion,
    string Telefono
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
    Guid SucursalId,
    string Nombre,
    string Codigo,
    DireccionResponseDto Direccion,
    string? Telefono,
    EstatusBasico Estatus
)
{
    public static SucursalResponseDto FromEntity(Sucursal entity)
    {
        var dir = DireccionResponseDto.FromEntity(entity.Direccion);
        
        return new SucursalResponseDto(
            entity.Id,
            entity.Nombre,
            entity.Codigo,
            dir,
            entity.Telefono,
            entity.Estatus
        );
    }
        
};