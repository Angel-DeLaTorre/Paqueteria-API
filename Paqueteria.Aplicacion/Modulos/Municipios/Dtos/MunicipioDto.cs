using System.ComponentModel.DataAnnotations;
using Paqueteria.Core.Entidades.Catalogos;

namespace Paqueteria.Application.Modulos.Municipios.Dtos;

public record MunicipioResponseDto(
    [property: Required] Guid MunicipioId,
    string Nombre,
    string EstadoId,
    string? EstadoNombre
)
{
    public static MunicipioResponseDto FromEntity(Municipio entity)
    {
        return new MunicipioResponseDto(
            entity.Id,
            entity.Nombre,
            entity.EstadoId,
            entity.Estado?.Nombre ?? "Sin Estado"
        );
    }
};