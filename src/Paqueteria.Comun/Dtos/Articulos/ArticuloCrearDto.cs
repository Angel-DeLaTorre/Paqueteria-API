namespace Paqueteria.Comun.Dtos.Articulos;

public record ArticuloCrearDto
(
    string ArticuloId,
    string Texto,
    string Similares,
    string MaterialPeligroso,
    DateTime VigenciaDesde,
    DateTime VigenciaHasta
);