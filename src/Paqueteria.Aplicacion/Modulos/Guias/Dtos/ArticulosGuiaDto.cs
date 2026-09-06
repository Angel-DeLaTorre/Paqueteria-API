using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Guias.Dtos;

public record ArticulosGuiaDto(
    Guid ArticuloGuiaId,
    string ClaveProdServSat,
    string Descripcion,
    int Cantidad,
    string ClaveUnidadSat,
    decimal PesoUnitarioKg,
    decimal PesoTotalKg,
    string? ClaveTipoEmbalajeSat,
    decimal ValorUnidad,
    decimal Largo,
    decimal Ancho,
    decimal Alto,
    bool EsMaterialPeligroso,
    string? ClaveMaterialPeligrosoSat,
    Guid? ArticuloId
)
{
    public static ArticulosGuiaDto FromEntity(ArticuloGuia entity)
    {
        return new ArticulosGuiaDto
        (
            entity.Id,
            entity.ClaveProdServSat,
            entity.Descripcion,
            entity.Cantidad,
            entity.ClaveUnidadSat,
            entity.PesoUnitarioKg,
            entity.PesoTotalKg,
            entity.ClaveTipoEmbalajeSat,
            entity.ValorUnidad,
            entity.Largo,
            entity.Ancho,
            entity.Alto,
            entity.EsMaterialPeligroso,
            entity.ClaveMaterialPeligrosoSat,
            entity.ArticuloId
        );
    }
};

