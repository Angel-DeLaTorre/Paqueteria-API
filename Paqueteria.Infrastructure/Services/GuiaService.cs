using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;
using Paqueteria.Infrastructure.Persistence;

namespace Paqueteria.Infrastructure.Services;

public class GuiaService(IGuiaRepository guiaRepository, UnitOfWork unitOfWork) : IGuiaService
{
    public async Task<Result<IReadOnlyList<GuiaResponseDto>>> GetAllAsync()
    {
        try
        {
            var guias = ( await guiaRepository.GetAllAsync() )
                .Select( GuiaResponseDto.FromEntity ).ToList();
            return Result<IReadOnlyList<GuiaResponseDto>>.Success(guias);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<GuiaResponseDto>> GetByIdAsync(Guid guiaId)
    {
        try
        {
            var guia = await guiaRepository.GetByIdAsync(guiaId);

            if (guia is null)
                return Result<GuiaResponseDto>.Failure(CodigoRespuesta.NotFound, "Asignacion no encontrada");

            return Result<GuiaResponseDto>.Success(GuiaResponseDto.FromEntity(guia));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<GuiaResponseDto>> CreateAsync(GuiaCreateDto dto, UserContext currentUser)
    {
        try
        {
            var guia = await guiaRepository.AddAsync(dto.ToEntity());

            var result = unitOfWork.CompleteAsync();

            if (result.IsCompletedSuccessfully)
                return Result<GuiaResponseDto>.Failure(CodigoRespuesta.Failure, "Error al crear articulo");

            return Result<GuiaResponseDto>.Success(GuiaResponseDto.FromEntity(guia));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> UpdateAsync(GuiaUpdateDto dto, UserContext currentUser)
    {
        try
        {
            var guia = await guiaRepository.GetByIdAsync(dto.GuiaId);

            if (guia is null)
                return Result.Failure(CodigoRespuesta.NotFound, "Guia no encontrado");

            dto.UpdateEntity(guia);
            guiaRepository.Update(guia);
            var result = await  unitOfWork.CompleteAsync();

            if (result != 0)
                return Result.Failure(CodigoRespuesta.Failure, "Error al actualizar");

            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> DeleteAsync(Guid guiaId, UserContext currentUser)
    {
        try
        {
            var guia = (await guiaRepository.GetByIdAsync(guiaId));

            if (guia is null)
                return Result.Failure(CodigoRespuesta.NotFound, "Guia no encontrado");

            guiaRepository.Delete(guia);

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