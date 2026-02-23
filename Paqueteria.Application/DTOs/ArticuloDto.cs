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
    Guid IdArticulo,
    string Clave,
    string Descripcion
);

public record ArticuloResponseDto(
    Guid IdArticulo,
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