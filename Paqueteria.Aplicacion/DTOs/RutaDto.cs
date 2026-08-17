using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.DTOs;

public record RutaCreateDto(
    string Descripcion,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId
)
{
    public Ruta ToEntity(Guid empresaId) =>
        Ruta.Create
        (
            Descripcion,
            SucursalOrigenId,
            SucursalDestinoId,
            empresaId
        );
};

public record RutaUpdateDto(
    Guid RutaId,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    string? Descripcion
)
{
    public void UpdateEntity(Ruta entity)
    {
        entity.SucursalOrigenId = SucursalOrigenId;
        entity.SucursalDestinoId = SucursalDestinoId;
        entity.Descripcion = Descripcion;
    }
}

public record RutaResponseDto(
    Guid RutaId,
    Guid SucursalOrigenId,
    SucursalResponseDto SucursalOrigen,
    Guid SucursalDestinoId,
    SucursalResponseDto SucursalDestino,
    string? Descripcion
)
{
    public static RutaResponseDto FromEntity(Ruta entity)
    {
        var sucursalOrigen = SucursalResponseDto.FromEntity(entity.SucursalOrigen);
        var sucursalDestino = SucursalResponseDto.FromEntity(entity.SucursalDestino);
        
        return new RutaResponseDto(
            entity.Id,
            entity.SucursalOrigenId,
            sucursalOrigen,
            entity.SucursalDestinoId,
            sucursalDestino,
            entity.Descripcion
        );
    }
        
}