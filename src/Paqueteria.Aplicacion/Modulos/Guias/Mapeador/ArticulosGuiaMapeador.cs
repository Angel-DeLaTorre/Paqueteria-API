using Paqueteria.Application.Modulos.Guias.Dtos;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Guias.Mapeador;

public static class ArticulosGuiaMapeador
{
    internal static ArticulosGuiaDto MapeaRespuestaDto(this ArticuloGuia entidad)
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