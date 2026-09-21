namespace Paqueteria.Comun.Dtos.Rutas;

public record RutaActualizarDto
(
    Guid RutaId,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId,
    string? Descripcion
);