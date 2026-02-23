using Paqueteria.Core.Entities;

namespace Paqueteria.Application.DTOs;

public record SeguroCreateDto(
    string Nombre
)
{
    public Seguro ToEntity()
    {
        return new Seguro(
            Nombre
        );
    }
};

public record SeguroUpdateDto(
    Guid SeguroId,
    string Nombre
)
{
    public void UpdateEntity(Seguro entity)
    {
        if (entity.Nombre != Nombre)
            entity.Nombre = Nombre.Trim();
    }
};

public record SeguroResponseDto(
    Guid SeguroId,
    string Nombre
)
{
    private static SeguroResponseDto FromEntity(Seguro entity)
    {
        return new SeguroResponseDto(
            entity.Id,
            entity.Nombre
        );
    }
};