namespace Paqueteria.Comun.Dtos.Asignaciones;

public record AsignacionCrearDto
(
    Guid AsignacionId,
    DateTime? FechaPartida,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    Guid[] GuiasId,
    string? St1,
    string? St2,
    string? St3,
    string? St4,
    Guid? ChoferId
);