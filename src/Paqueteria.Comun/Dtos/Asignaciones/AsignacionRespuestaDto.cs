using Paqueteria.Comun.Dtos.Sucursales;

namespace Paqueteria.Comun.Dtos.Asignaciones;

public record AsignacionRespuestaDto(
    Guid AsignacionId,
    string Clave,
    SucursalRespuestaDto? SucursalOrigen,
    SucursalRespuestaDto? SucursalDestino,
    Guid? ChoferId,
    DateTime? FechaPartida,
    string? St1,
    string? St2,
    string? St3,
    string? St4
);