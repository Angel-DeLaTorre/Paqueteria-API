using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Modulos.Sucursales.Dtos;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Rutas.Dtos;

public record RutaCrearDto(
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

public record RutaActualizarDto(
    [param: Required] Guid RutaId,
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

public record RutaRespuestaDto(
    [param: Required] Guid RutaId,
    Guid SucursalOrigenId,
    SucursalResponseDto SucursalOrigen,
    Guid SucursalDestinoId,
    SucursalResponseDto SucursalDestino,
    string? Descripcion
)
{
    public static RutaRespuestaDto FromEntity(Ruta entity)
    {
        var sucursalOrigen = SucursalResponseDto.FromEntity(entity.SucursalOrigen);
        var sucursalDestino = SucursalResponseDto.FromEntity(entity.SucursalDestino);
        
        return new RutaRespuestaDto(
            entity.Id,
            entity.SucursalOrigenId,
            sucursalOrigen,
            entity.SucursalDestinoId,
            sucursalDestino,
            entity.Descripcion
        );
    }
        
}