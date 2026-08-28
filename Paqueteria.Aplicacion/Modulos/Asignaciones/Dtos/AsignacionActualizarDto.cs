using System.ComponentModel.DataAnnotations;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Asignaciones.Dtos;

public record AsignacionActualizarDto
(
    [property: Required] Guid AsignacionId,
    [property: Required] Guid SucursalOrigenId,
    [property: Required] Guid SucursalDestinoId,
    [property: Required] DateTime FechaPartida,
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