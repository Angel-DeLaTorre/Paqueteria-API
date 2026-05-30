using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.DTOs;

public record ArticuloGuiaCreateDto
(
    Guid GuiaId,
    string Descripcion,
    string ArticuloId,
    int Cantidad,
    decimal PesoUnidad,
    decimal ValorUnidad
)
{
    public ArticuloGuia ToEntity()
    {
        return ArticuloGuia.Create(GuiaId, Descripcion, ArticuloId,  Cantidad, PesoUnidad, ValorUnidad);
    }
}

public record ArticuloGuiaUpdateDto
(
    Guid GuiaId,
    string Descripcion,
    string ArticuloId,
    int Cantidad,
    decimal PesoUnidad,
    decimal ValorUnidad
)
{
    public ArticuloGuia ToEntity()
    {
        return ArticuloGuia.Create(GuiaId, Descripcion, ArticuloId,  Cantidad, PesoUnidad, ValorUnidad);
    }
}

public record ArticuloGuiaResponseDto
(
    Guid ArticuloGuiaId,
    string Descripcion,
    Guid GuiaId,
    string ArticuloId,
    int Cantidad,
    decimal PesoUnidad,
    decimal ValorUnidad
)
{
    public static ArticuloGuiaResponseDto FromEntity(ArticuloGuia entity)
    {
        return new ArticuloGuiaResponseDto
        (
            entity.Id,
            entity.Descripcion,
            entity.GuiaId,
            entity.ArticuloId,
            entity.Cantidad,
            entity.PesoUnidad,
            entity.ValorUnidad
        );
    }
}