using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.DTOs;

public record AsignacionCreateDto(
    Guid Id,
    DateTime? FechaPartida,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    Guid[] GuiasId,
    string? St1,
    string? St2,
    string? St3,
    string? St4,
    Guid? ChoferId
)
{
    public Asignacion ToEntity(Guid empresaId) => Asignacion.Crear(
        SucursalOrigenId,
        SucursalDestinoId,
        FechaPartida,
        St1,
        St2,
        St3,
        St4,
        ChoferId,
        empresaId
        );
};

public record AsignacionUpdateDto(
    Guid Id,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    DateTime FechaPartida,
    string? St1,
    string? St2,
    string? St3,
    string? St4
)
{
    public void UpdateEntity(Asignacion entity)
    {
        entity.FechaPartida = FechaPartida;
        entity.SucursalOrigenId = SucursalOrigenId;
        entity.SucursalDestinoId = SucursalDestinoId;
        entity.St1 = St1;
        entity.St2 = St2;
        entity.St3 = St3;
        entity.St4 = St4;
    }
};

public record AsignacionResponseDto(
    Guid Id,
    Sucursal? SucursalOrigen,
    Sucursal? SucursalDestino,
    Guid? ChoferId,
    DateTime? FechaPartida,
    string? St1,
    string? St2,
    string? St3,
    string? St4
)
{
    public static AsignacionResponseDto FromEntity(Asignacion entity)
    {
        return new AsignacionResponseDto(
            entity.Id,
            entity.SucursalOrigen,
            entity.SucursalDestino,
            entity.ChoferId,
            entity.FechaPartida,
            entity.St1,
            entity.St2,
            entity.St3,
            entity.St4
        );
    }
}