using System.ComponentModel.DataAnnotations;
using Paqueteria.Core.Entidades.Catalogos;

namespace Paqueteria.Application.Modulos.Estados.Dtos;

public record EstadoResponseDto(
    [param: Required] string EstadoId,
    string Nombre,
    string Acronimo
)
{
    public static EstadoResponseDto FromEntity(Estado entity) =>
        new
        (
            entity.Id,
            entity.Nombre,
            entity.Acronimo
        );
};