using Paqueteria.Core.Entidades.Sat;

namespace Paqueteria.Application.Modulos.Articulos.Dtos;

public record ArticuloCreateDto(
    string ArticuloId,
    string Texto,
    string Similares,
    string MaterialPeligroso,
    DateTime VigenciaDesde,
    DateTime VigenciaHasta
)
{
    public Articulo ToEntity() => Articulo.Create(Texto, Similares, MaterialPeligroso, VigenciaDesde, VigenciaHasta);
};

public record ArticuloUpdateDto(
    Guid ArticuloId,
    string Texto,
    string Similares,
    string MaterialPeligroso,
    DateTime VigenciaDesde,
    DateTime VigenciaHasta
)
{
    public void UpdateEntity(Articulo entity)
    {
        entity.Texto = Texto;
        entity.Similares = Similares;
        entity.MaterialPeligroso = MaterialPeligroso;
        entity.VigenciaDesde = VigenciaDesde;
        entity.VigenciaHasta = VigenciaHasta;
    }
};

public record ArticuloResponseDto(
    Guid ArticuloId,
    string Texto,
    string Similares,
    string MaterialPeligroso
)
{
    public static ArticuloResponseDto FromEntity(Articulo entity)
    {
        return new ArticuloResponseDto(
            entity.Id,
            entity.Texto,
            entity.Similares,
            entity.MaterialPeligroso
        );
    }
};