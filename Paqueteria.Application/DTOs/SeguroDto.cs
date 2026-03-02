using Paqueteria.Core.Entities;

namespace Paqueteria.Application.DTOs;

public record SeguroCreateDto(
    string Nombre
)
{
    public Seguro ToEntity() =>
        new(
            Nombre
        );
};

public record SeguroUpdateDto(
    Guid SeguroId,
    string Nombre
)
{
    public void UpdateEntity(Seguro entity)
    {
        entity.Nombre = Nombre;
    }
};

public record SeguroResponseDto(
    Guid SeguroId,
    string Nombre
)
{
    private static SeguroResponseDto FromEntity(Seguro entity) =>
        new (
            entity.Id,
            entity.Nombre
        );
};