using Paqueteria.Core.Entities;

namespace Paqueteria.Application.DTOs;

public record EstadoDto(
    string Id,
    string Nombre,
    string Acronimo2
)
{
    public static EstadoDto FromEntity(Estado entity)
    {
        return new EstadoDto(
            entity.Id,
            entity.Nombre,
            entity.Acronimo2
        );
    }
};