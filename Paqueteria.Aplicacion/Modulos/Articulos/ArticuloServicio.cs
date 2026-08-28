using Paqueteria.Application.Dtos;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Articulos.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;

namespace Paqueteria.Application.Modulos.Articulos;

public class ArticuloServicio(IUnitOfWork unit) : IArticuloServicio
{
    public async Task<Respuesta<IReadOnlyList<ArticuloResponseDto>>> ObtenerTodosAsync()
    {
        var articulos = ( await unit.Articulos.ObtenerTodosAsync() )
            .Select( ArticuloResponseDto.FromEntity ).ToList();
        return Respuesta<IReadOnlyList<ArticuloResponseDto>>.Exitoso(articulos);
    }

    public async Task<Respuesta<ArticuloResponseDto>> ObtenerPorIdAsync(Guid id)
    {
        var articulo = await unit.Articulos.ObtenerPorIdAsync(id);

        if (articulo == null)
            return Respuesta<ArticuloResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<ArticuloResponseDto>.Exitoso(ArticuloResponseDto.FromEntity(articulo));
    }

    public async Task<Respuesta<ArticuloResponseDto>> AgregarAsync(ArticuloCreateDto dto)
    {
        var articulo = await unit.Articulos.AgregarAsync(dto.ToEntity());
        await unit.GuardarCambiosAsync();
        return Respuesta<ArticuloResponseDto>.Exitoso(ArticuloResponseDto.FromEntity(articulo));
    }

    public async Task<Respuesta> ActualizarAsync(ArticuloUpdateDto dto)
    {
        var articulo = await unit.Articulos.ObtenerPorIdAsync(dto.ArticuloId);

        if (articulo == null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(articulo);
        await  unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> EliminarAsync(Guid articuloId)
    {
        var articulo = await unit.Articulos.ObtenerPorIdAsync(articuloId);

        if (articulo == null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        unit.Articulos.Eliminar(articulo);
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }
}