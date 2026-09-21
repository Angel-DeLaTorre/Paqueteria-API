using Paqueteria.Comun.Dtos.Sucursales;

namespace Paqueteria.Comun.Dtos.Rutas;

public record RutaRespuestaDto
(
    Guid RutaId,
    Guid SucursalOrigenId,
    SucursalRespuestaDto SucursalOrigen,
    Guid SucursalDestinoId,
    SucursalRespuestaDto SucursalDestino,
    string? Descripcion
);