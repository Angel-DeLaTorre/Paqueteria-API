using Paqueteria.Core.Entities;

namespace Paqueteria.Application.DTOs;

public record MunicipioResponseDto(
    Guid MunicipioId,
    string Nombre,
    string EstadoId,
    string EstadoNombre
)
{
    public static MunicipioResponseDto FromEntity(Municipio entity)
    {
        return new MunicipioResponseDto(
            entity.Id,
            entity.Nombre,
            entity.EstadoId,
            entity.Estado.Nombre
        );
    }
};