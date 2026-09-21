namespace Paqueteria.Comun.Dtos.Rutas;

public record RutaCrearDto
(
    string Descripcion,
    Guid SucursalOrigenId,
    Guid SucursalDestinoId
);