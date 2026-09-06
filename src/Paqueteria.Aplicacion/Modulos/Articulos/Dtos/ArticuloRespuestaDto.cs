using System.ComponentModel.DataAnnotations;

namespace Paqueteria.Application.Modulos.Articulos.Dtos;

public record ArticuloRespuestaDto
(
    Guid ArticuloId,
    string Texto,
    string Similares,
    string MaterialPeligroso
);