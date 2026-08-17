using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Application.Modulos.Articulos;

public class ArticuloServicio(IUnitOfWork unit) : IArticuloServicio
{
    public async Task<Resultado<IReadOnlyList<ArticuloResponseDto>>> ObtenerTodosAsync()
    {
        var articulos = ( await unit.Articulos.ObtenerTodosAsync() )
            .Select( ArticuloResponseDto.FromEntity ).ToList();
        return Resultado<IReadOnlyList<ArticuloResponseDto>>.Exitoso(articulos);
    }

    public async Task<Resultado<ArticuloResponseDto>> ObtenerPorIdAsync(Guid id)
    {
        var articulo = await unit.Articulos.ObtenerPorIdAsync(id);

        if (articulo == null)
            return Resultado<ArticuloResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<ArticuloResponseDto>.Exitoso(ArticuloResponseDto.FromEntity(articulo));
    }

    public async Task<Resultado<ArticuloResponseDto>> AgregarAsync(ArticuloCreateDto dto)
    {
        var articulo = await unit.Articulos.AgregarAsync(dto.ToEntity());
        await unit.CompletarAsync();
        return Resultado<ArticuloResponseDto>.Exitoso(ArticuloResponseDto.FromEntity(articulo));
    }

    public async Task<Resultado> ActualizarAsync(ArticuloUpdateDto dto)
    {
        var articulo = await unit.Articulos.ObtenerPorIdAsync(dto.ArticuloId);

        if (articulo == null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(articulo);
        await  unit.CompletarAsync();

        return Resultado.Exitoso();
    }

    public async Task<Resultado> EliminarAsync(Guid articuloId)
    {
        var articulo = await unit.Articulos.ObtenerPorIdAsync(articuloId);

        if (articulo == null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        unit.Articulos.Eliminar(articulo);
        await unit.CompletarAsync();

        return Resultado.Exitoso();
    }
}