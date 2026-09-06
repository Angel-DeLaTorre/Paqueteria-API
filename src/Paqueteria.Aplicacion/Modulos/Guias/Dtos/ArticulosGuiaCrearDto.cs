namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record ArticulosGuiaCrearDto
(
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