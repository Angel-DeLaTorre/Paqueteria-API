namespace Paqueteria.Comun.Dtos.Guias;

public record ItemArticuloRemisionDto(
    int Cantidad,
    string Descripcion,
    decimal ValorUnidad,
    decimal PesoUnitarioKg,
    decimal Importe
);