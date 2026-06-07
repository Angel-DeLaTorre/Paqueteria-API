using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Infrastructure.Services;

public class ArticuloService(IUnitOfWork unit) : IArticuloService
{
    public async Task<Result<IReadOnlyList<ArticuloResponseDto>>> GetAllAsync()
    {
        var articulos = ( await unit.Articulos.GetAllAsync() )
            .Select( ArticuloResponseDto.FromEntity ).ToList();
        return Result<IReadOnlyList<ArticuloResponseDto>>.Success(articulos);
    }

    public async Task<Result<ArticuloResponseDto>> GetByIdAsync(string id)
    {
        var articulo = await unit.Articulos.GetByIdAsync(id);

        if (articulo == null)
            return Result<ArticuloResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<ArticuloResponseDto>.Success(ArticuloResponseDto.FromEntity(articulo));
    }

    public async Task<Result<ArticuloResponseDto>> CreateAsync(ArticuloCreateDto dto)
    {
        var articulo = await unit.Articulos.AddAsync(dto.ToEntity());
        await unit.CompleteAsync();
        return Result<ArticuloResponseDto>.Success(ArticuloResponseDto.FromEntity(articulo));
    }

    public async Task<Result> UpdateAsync(ArticuloUpdateDto dto)
    {
        var articulo = await unit.Articulos.GetByIdAsync(dto.ArticuloId);

        if (articulo == null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        dto.UpdateEntity(articulo);
        await  unit.CompleteAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(string articuloId)
    {
        var articulo = await unit.Articulos.GetByIdAsync(articuloId);

        if (articulo == null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        unit.Articulos.Delete(articulo);
        await unit.CompleteAsync();

        return Result.Success();
    }
}