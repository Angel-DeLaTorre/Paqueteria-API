using System.Linq.Expressions;
using Paqueteria.Application.Modulos.GuiasTransbordo.Dtos;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.Modulos.GuiasTransbordo.Interfaces;

public interface IGuiaTransbordoMapeador
{
    /// <summary>
    /// Expresión de proyección para ser utilizada en consultas IQueryable de EF Core.
    /// Permite traer de la base de datos únicamente los campos requeridos por el DTO.
    /// </summary>
    Expression<Func<GuiaTransbordo, GuiaTransbordoResponseDto>> ProyeccionRespuesta { get; }

    /// <summary>
    /// Mapea una entidad en memoria a su DTO de respuesta.
    /// </summary>
    GuiaTransbordoResponseDto ARespuestaDto(GuiaTransbordo entidad);

    /// <summary>
    /// Mapea una colección de entidades en memoria a una lista de DTOs.
    /// </summary>
    IReadOnlyList<GuiaTransbordoResponseDto> AListaRespuestaDto(IEnumerable<GuiaTransbordo> entidades);
}