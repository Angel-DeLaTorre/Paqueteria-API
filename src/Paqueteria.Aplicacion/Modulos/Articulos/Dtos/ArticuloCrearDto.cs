namespace Paqueteria.Application.Modulos.Articulos.Dtos;

public record ArticuloCrearDto
(
    string ArticuloId,
    string Texto,
    string Similares,
    string MaterialPeligroso,
    DateTime VigenciaDesde,
    DateTime VigenciaHasta
);