using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Asignaciones.Dtos;

public record AsignacionCrearDto
(
    [property: Required] Guid AsignacionId,
    DateTime? FechaPartida,
    [property: Required] Guid SucursalOrigenId,
    [property: Required] Guid SucursalDestinoId,
    [property: Required] Guid[] GuiasId,
    string? St1,
    string? St2,
    string? St3,
    string? St4,
    Guid? ChoferId
);