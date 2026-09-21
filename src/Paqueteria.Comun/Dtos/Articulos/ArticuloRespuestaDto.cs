namespace Paqueteria.Comun.Dtos.Articulos;

public record ArticuloRespuestaDto
(
    Guid ArticuloId,
    string Texto,
    string Similares,
    string MaterialPeligroso
);