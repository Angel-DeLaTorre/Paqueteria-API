using Paqueteria.Application.Modulos.Guias.Dtos;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Guias.Mapeador;

public static class ArticulosGuiaMapeador
{
    internal static ArticulosGuiaDto ARespuestaDto(this ArticuloGuia entidad)
    {
        return new ArticulosGuiaDto
        (
            entidad.Id,
            entidad.ClaveProdServSat,
            entidad.Descripcion,
            entidad.Cantidad,
            entidad.ClaveUnidadSat,
            entidad.PesoUnitarioKg,
            entidad.PesoTotalKg,
            entidad.ClaveTipoEmbalajeSat,
            entidad.ValorUnidad,
            entidad.Largo,
            entidad.Ancho,
            entidad.Alto,
            entidad.EsMaterialPeligroso,
            entidad.ClaveMaterialPeligrosoSat,
            entidad.ArticuloId
        );
    }
}