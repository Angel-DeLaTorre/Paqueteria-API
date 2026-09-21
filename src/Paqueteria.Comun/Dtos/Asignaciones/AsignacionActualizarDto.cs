namespace Paqueteria.Comun.Dtos.Asignaciones;

public record AsignacionActualizarDto
(
    Guid AsignacionId,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    DateTime FechaPartida
);