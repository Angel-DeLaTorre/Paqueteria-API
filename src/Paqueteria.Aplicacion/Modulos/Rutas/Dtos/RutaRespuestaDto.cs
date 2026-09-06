using Paqueteria.Application.Modulos.Sucursales.Dtos;

namespace Paqueteria.Application.Modulos.Rutas.Dtos;

public record RutaRespuestaDto
(
    Guid RutaId,
    Guid SucursalOrigenId,
    SucursalRespuestaDto SucursalOrigen,
    Guid SucursalDestinoId,
    SucursalRespuestaDto SucursalDestino,
    string? Descripcion
);