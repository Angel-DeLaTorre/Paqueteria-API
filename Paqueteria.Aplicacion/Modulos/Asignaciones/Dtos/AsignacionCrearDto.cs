using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Asignaciones.Dtos;

public record AsignacionCrearDto
(
    [param: Required] Guid AsignacionId,
    DateTime? FechaPartida,
    [param: Required] Guid SucursalOrigenId,
    [param: Required] Guid SucursalDestinoId,
    [param: Required] Guid[] GuiasId,
    string? St1,
    string? St2,
    string? St3,
    string? St4,
    Guid? ChoferId
);