namespace Paqueteria.Comun.Dtos.Articulos;

public record ArticuloActualizarDto
(
    Guid ArticuloId,
    string Texto,
    string Similares,
    string MaterialPeligroso,
    DateTime VigenciaDesde,
    DateTime VigenciaHasta
);