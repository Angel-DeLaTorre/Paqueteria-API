using Paqueteria.Core.Entities;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.DTOs;

public record SeguroCreateDto(
    string Nombre
)
{
    public Seguro ToEntity(Guid empresaId) =>
        Seguro.Create(Nombre, empresaId);
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
    public static SeguroResponseDto FromEntity(Seguro entity) =>
        new (
            entity.Id,
            entity.Nombre
        );
};