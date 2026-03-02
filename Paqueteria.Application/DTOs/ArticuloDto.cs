using Paqueteria.Core.Entities;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.DTOs;

public record ArticuloCreateDto(
    string Clave,
    string Descripcion
)
{
    public Articulo ToEntity() => new Articulo(Clave, Descripcion);

    public Articulo ToEntity(EstatusGenerico estatus) => new Articulo(Clave, Descripcion,  estatus);
};

public record ArticuloUpdateDto(
    Guid ArticuloId,
    string Clave,
    string Descripcion
)
{
    public void UpdateEntity(Articulo entity)
    {
        entity.Clave = Clave;
        entity.Descripcion = Descripcion;
    }
};

public record ArticuloResponseDto(
    Guid ArticuloId,
    string Clave,
    string Descripcion
)
{
    public static ArticuloResponseDto FromEntity(Articulo entity)
    {
        return new ArticuloResponseDto(
            entity.Id,
            entity.Clave,
            entity.Descripcion
        );
    }
};