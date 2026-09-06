using System.ComponentModel.DataAnnotations;
using Paqueteria.Application.Modulos.Sucursales.Dtos;

namespace Paqueteria.Application.Modulos.Asignaciones.Dtos;

public record AsignacionRespuestaDto(
    Guid Id,
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