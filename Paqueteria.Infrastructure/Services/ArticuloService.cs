using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class ArticuloService(IArticuloRepository articuloRepository, IUnitOfWork unitOfWork) : IArticuloService
{
    public async Task<Result<IReadOnlyList<ArticuloResponseDto>>> GetAllAsync()
    {
        var articulos = ( await articuloRepository.GetAllAsync() )
            .Select( ArticuloResponseDto.FromEntity ).ToList();
        return Result<IReadOnlyList<ArticuloResponseDto>>.Success(articulos);
    }

    public async Task<Result<ArticuloResponseDto>> GetByIdAsync(Guid id)
    {
        var articulo = await articuloRepository.GetByIdAsync(id);

        if (articulo == null)
            return Result<ArticuloResponseDto>.Failure(CodigoRespuesta.NotFound, "Articulo no encontrado");

        return Result<ArticuloResponseDto>.Success(ArticuloResponseDto.FromEntity(articulo));
    }

    public async Task<Result<ArticuloResponseDto>> CreateAsync(ArticuloCreateDto dto, UserContext currentUser)
    {
        var articulo = await articuloRepository.AddAsync(dto.ToEntity());

        var result = unitOfWork.CompleteAsync();

        if (result.IsCompletedSuccessfully)
            return Result<ArticuloResponseDto>.Failure(CodigoRespuesta.Failure, "Error al crear articulo");

        return Result<ArticuloResponseDto>.Success(ArticuloResponseDto.FromEntity(articulo));
    }

    public async Task<Result> UpdateAsync(ArticuloUpdateDto dto, UserContext currentUser)
    {
        var articulo = await articuloRepository.GetByIdAsync(dto.ArticuloId);

        if (articulo == null)
            return Result.Failure(CodigoRespuesta.NotFound, "Articulo no encontrado");

        dto.UpdateEntity(articulo);
        articuloRepository.Update(articulo);
        var result = await  unitOfWork.CompleteAsync();

        if (result != 0)
            return Result.Failure(CodigoRespuesta.Failure, "Error al actualizar");

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid articuloId, UserContext currentUser)
    {
        try
        {
            var articulo = (await articuloRepository.GetByIdAsync(articuloId));

            if (articulo == null)
                return Result.Failure(CodigoRespuesta.NotFound, "Articulo no encontrado");

            articuloRepository.Delete(articulo);

            var result = await unitOfWork.CompleteAsync();
            if (result <= 0)
                return Result.Failure(CodigoRespuesta.Failure, "No se realizaron cambios");

            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}