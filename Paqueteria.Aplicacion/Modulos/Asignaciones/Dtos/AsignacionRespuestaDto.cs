using System.ComponentModel.DataAnnotations;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Asignaciones.Dtos;

public record AsignacionRespuestaDto(
    [property: Required] Guid Id,
    [property: Required] string Clave,
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
    public static AsignacionRespuestaDto FromEntity(Asignacion entity)
    {
        return new AsignacionRespuestaDto(
            entity.Id,
            entity.Clave,
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