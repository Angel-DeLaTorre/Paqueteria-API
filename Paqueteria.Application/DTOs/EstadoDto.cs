using Paqueteria.Core.Entities.Catalogos;

namespace Paqueteria.Application.DTOs;

public record EstadoResponseDto(
    string Id,
    string Nombre,
    string Acronimo2
)
{
    public static EstadoResponseDto FromEntity(Estado entity) =>
        new
        (
            entity.Id,
            entity.Nombre,
            entity.Acronimo2
        );
};