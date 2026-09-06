namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record ItemArticuloRemisionDto(
    int Cantidad,
    string Descripcion,
    decimal ValorUnidad,
    decimal PesoUnitarioKg,
    decimal Importe
);