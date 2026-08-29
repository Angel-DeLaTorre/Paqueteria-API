using System.ComponentModel.DataAnnotations;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.Seguros.Dtos;

public record SeguroCreateDto(
    string Nombre
)
{
    public Seguro ToEntity(Guid empresaId) =>
        Seguro.Create(Nombre, empresaId);
};

public record SeguroUpdateDto(
    [param: Required] Guid SeguroId,
    string Nombre
)
{
    public void UpdateEntity(Seguro entity)
    {
        entity.Nombre = Nombre;
    }
};

public record SeguroResponseDto(
    [param: Required] Guid SeguroId,
    string Nombre
)
{
    public static SeguroResponseDto FromEntity(Seguro entity) =>
        new (
            entity.Id,
            entity.Nombre
        );
};