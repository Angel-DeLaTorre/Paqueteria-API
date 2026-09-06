using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Asignaciones.Dtos;

public record AsignacionActualizarDto
(
    Guid AsignacionId,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    DateTime FechaPartida
);