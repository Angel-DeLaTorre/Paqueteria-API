using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Application.DTOs;

public record DireccionGuiaSnapCreateDto(
    DireccionDto Direccion
)
{
    public DireccionGuiaSnapshot ToEntity()
    {
        var direccion = Direccion.ToEntity();
        return DireccionGuiaSnapshot.Create(direccion);
    }
};

public record DireccionGuiaSnapUpdateDto(
    Guid DireccionGuiaId,
    DireccionDto Direccion
)
{
    public void UpdateEntity(DireccionGuiaSnapshot entity)
    {
        entity.Direccion = Direccion.ToEntity();
    }
}

public record DireccionGuiaSnapResponseDto(
    Guid DireccionGuiaId,
    DireccionResponseDto Direccion,
    string MunicipioNombre
)
{
    public static DireccionGuiaSnapResponseDto FromEntity(DireccionGuiaSnapshot entity) =>
        new (
            entity.Id,
            DireccionResponseDto.FromEntity(entity.Direccion),
            entity.Direccion.Municipio.Nombre
        );
}