using Paqueteria.Core.Entities;

namespace Paqueteria.Application.DTOs;

public record MunicipioDto(
    Guid IdMunicipio,
    string Nombre,
    string EstadoId,
    string Estado
)
{
    public static MunicipioDto FromEntity(Municipio entity)
    {
        return new MunicipioDto(
            entity.Id,
            entity.Nombre,
            entity.EstadoId,
            entity.Estado.Nombre
        );
    }
};