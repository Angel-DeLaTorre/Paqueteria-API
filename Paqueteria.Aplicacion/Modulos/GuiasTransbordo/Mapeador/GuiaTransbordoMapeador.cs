using System.Linq.Expressions;
using Paqueteria.Application.Modulos.GuiasTransbordo.Dtos;
using Paqueteria.Application.Modulos.GuiasTransbordo.Interfaces;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.GuiasTransbordo.Mapeador;

public class GuiaTransbordoMapeador : IGuiaTransbordoMapeador
{
    public Expression<Func<GuiaTransbordo, GuiaTransbordoResponseDto>> ProyeccionRespuesta => entidad => new GuiaTransbordoResponseDto(
        entidad.Id,
        entidad.GuiaId,
        entidad.AsignacionId,
        entidad.SucursalTransbordoId,
        entidad.SucursalTransbordo != null ? entidad.SucursalTransbordo.Nombre : string.Empty,
        entidad.FechaEscaneoIngreso,
        entidad.FechaEscaneoSalida,
        entidad.Observaciones
    );

    public GuiaTransbordoResponseDto ARespuestaDto(GuiaTransbordo entidad)
    {
        ArgumentNullException.ThrowIfNull(entidad);

        return new GuiaTransbordoResponseDto(
            entidad.Id,
            entidad.GuiaId,
            entidad.AsignacionId,
            entidad.SucursalTransbordoId,
            entidad.SucursalTransbordo?.Nombre ?? string.Empty,
            entidad.FechaEscaneoIngreso,
            entidad.FechaEscaneoSalida,
            entidad.Observaciones
        );
    }

    public IReadOnlyList<GuiaTransbordoResponseDto> AListaRespuestaDto(IEnumerable<GuiaTransbordo> entidades)
    {
        if (entidades == null || !entidades.Any())
            return Array.Empty<GuiaTransbordoResponseDto>();

        return entidades.Select(ARespuestaDto).ToList();
    }
}