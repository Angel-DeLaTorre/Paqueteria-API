using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;

namespace Paqueteria.Infrastructure.Services;

public class ArticuloService(IArticuloRepository articuloRepository, IUnitOfWorkBase unitOfWorkBase) : IArticuloService
{
    public async Task<Result<IReadOnlyList<ArticuloResponseDto>>> GetAllAsync()
    {
        var articulos = ( await articuloRepository.GetAllAsync() )
            .Select( ArticuloResponseDto.FromEntity ).ToList();
        return Result<IReadOnlyList<ArticuloResponseDto>>.Success(articulos);
    }

    public async Task<Result<ArticuloResponseDto>> GetByIdAsync(string id)
    {
        var articulo = await articuloRepository.GetByIdAsync(id);

        if (articulo == null)
            return Result<ArticuloResponseDto>.Failure(Errors.Generic.NoEncontrado);

        return Result<ArticuloResponseDto>.Success(ArticuloResponseDto.FromEntity(articulo));
    }

    public async Task<Result<ArticuloResponseDto>> CreateAsync(ArticuloCreateDto dto, UserContext currentUser)
    {
        var articulo = await articuloRepository.AddAsync(dto.ToEntity());

        var result = unitOfWorkBase.CompleteAsync();

        if (result.IsCompletedSuccessfully)
            return Result<ArticuloResponseDto>.Failure(Errors.Generic.NoCreado);

        return Result<ArticuloResponseDto>.Success(ArticuloResponseDto.FromEntity(articulo));
    }

    public async Task<Result> UpdateAsync(ArticuloUpdateDto dto, UserContext currentUser)
    {
        var articulo = await articuloRepository.GetByIdAsync(dto.ArticuloId);

        if (articulo == null)
            return Result.Failure(Errors.Generic.NoEncontrado);

        dto.UpdateEntity(articulo);
        articuloRepository.Update(articulo);
        var result = await  unitOfWorkBase.CompleteAsync();

        if (result != 0)
            return Result.Failure(Errors.Generic.NoActualizado);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid articuloId, UserContext currentUser)
    {
        try
        {
            var articulo = (await articuloRepository.GetByIdAsync(articuloId));

            if (articulo == null)
                return Result.Failure(Errors.Generic.NoEncontrado);

            articuloRepository.Delete(articulo);

            var result = await unitOfWorkBase.CompleteAsync();
            if (result <= 0)
                return Result.Failure(Errors.Generic.NoEliminado);

            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}