using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class GuiaService(IGuiaRepository guiaRepository, IUnitOfWorkBase unitOfWorkBase) : IGuiaService
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
                return Result<GuiaResponseDto>.Failure(Errors.Generic.NoEncontrado);

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
            var guia = await guiaRepository.AddAsync(dto.ToEntity(currentUser.UserId, currentUser.EmpresaId));

            var result = unitOfWorkBase.CompleteAsync();

            if (result.IsCompletedSuccessfully)
                return Result<GuiaResponseDto>.Failure(Errors.Generic.NoCreado);

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
                return Result.Failure(Errors.Generic.NoEncontrado);

            dto.UpdateEntity(guia);
            guiaRepository.Update(guia);
            var result = await  unitOfWorkBase.CompleteAsync();

            if (result != 0)
                return Result.Failure(Errors.Generic.NoActualizado);

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
                return Result.Failure(Errors.Generic.NoEncontrado);

            guiaRepository.Delete(guia);

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