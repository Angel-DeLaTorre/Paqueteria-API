namespace Paqueteria.Application.Modulos.Articulos.Dtos;

public record ArticuloActualizarDto
(
    Guid ArticuloId,
    string Texto,
    string Similares,
    string MaterialPeligroso,
    DateTime VigenciaDesde,
    DateTime VigenciaHasta
);