namespace Paqueteria.Comun.Dtos.Guias;

public record ArticulosGuiaDto(
    Guid ArticuloGuiaId,
    string ClaveProdServSat,
    string Descripcion,
    int Cantidad,
    string ClaveUnidadSat,
    decimal PesoUnitarioKg,
    decimal PesoTotalKg,
    string? ClaveTipoEmbalajeSat,
    decimal ValorUnidad,
    decimal Largo,
    decimal Ancho,
    decimal Alto,
    bool EsMaterialPeligroso,
    string? ClaveMaterialPeligrosoSat,
    Guid? ArticuloId
);

