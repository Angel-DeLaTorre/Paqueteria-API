namespace Paqueteria.Application.DTOs;

public record ArticuloGuiaCreateDtoo(
    Guid GuiaId,
    string Descripcion,
    Guid ArticuloId,
    int Cantidad,
    decimal PesoUnidad,
    decimal ValorUnidad
);

public record ArticuloGuiaCreateDto(
    string ClaveProdServSat,
    string Descripcion,
    int Cantidad,
    string ClaveUnidadSat,
    decimal PesoUnitarioKg,
    string? ClaveTipoEmbalajeSat,
    decimal ValorUnidad,
    decimal Largo,
    decimal Ancho,
    decimal Alto,
    bool EsMaterialPeligroso,
    string? ClaveMaterialPeligrosoSat
);

public record ArticuloGuiaUpdateDto(
    Guid GuiaId,
    string Descripcion,
    Guid ArticuloId,
    int Cantidad,
    decimal PesoUnidad,
    decimal ValorUnidad
);

public record ArticuloGuiaResponseDto
(
    Guid ArticuloGuiaId,
    string Descripcion,
    Guid GuiaId,
    Guid ArticuloId,
    int Cantidad,
    decimal PesoUnidad,
    decimal ValorUnidad
);