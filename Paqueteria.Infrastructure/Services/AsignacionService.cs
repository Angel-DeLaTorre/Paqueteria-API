using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class AsignacionService (IAsignacionRepository asignacionRepository, IUnitOfWorkBase unitOfWorkBase) : IAsignacionSerivce
{
    public async Task<Result<IReadOnlyList<AsignacionResponseDto>>> GetAllAsync()
    {
        try
        {
            var asignaciones = ( await asignacionRepository.GetAllAsync() )
                .Select( AsignacionResponseDto.FromEntity ).ToList();
            return Result<IReadOnlyList<AsignacionResponseDto>>.Success(asignaciones);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<AsignacionResponseDto>> GetByIdAsync(Guid asignacionId)
    {
        try
        {
            var asignacion = await asignacionRepository.GetByIdAsync(asignacionId);

            if (asignacion is null)
                return Result<AsignacionResponseDto>.Failure(Errors.Generic.NoEncontrado);

            return Result<AsignacionResponseDto>.Success(AsignacionResponseDto.FromEntity(asignacion));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<AsignacionResponseDto>> CreateAsync(AsignacionCreateDto dto, UserContext currentUser)
    {
        try
        {
            var asignacion = await asignacionRepository.AddAsync(dto.ToEntity());

            var result = unitOfWorkBase.CompleteAsync();

            if (result.IsCompletedSuccessfully)
                return Result<AsignacionResponseDto>.Failure(Errors.Generic.NoCreado);

            return Result<AsignacionResponseDto>.Success(AsignacionResponseDto.FromEntity(asignacion));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> UpdateAsync(AsignacionUpdateDto dto, UserContext currentUser)
    {
        try
        {
            var asignacion = await asignacionRepository.GetByIdAsync(dto.Id);

            if (asignacion == null)
                return Result.Failure(Errors.Generic.NoEncontrado);

            dto.UpdateEntity(asignacion);
            asignacionRepository.Update(asignacion);
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

    public async Task<Result> DeleteAsync(Guid asignacionId, UserContext currentUser)
    {
        try
        {
            var asignacion = (await asignacionRepository.GetByIdAsync(asignacionId));

            if (asignacion == null)
                return Result.Failure(Errors.Generic.NoEncontrado);

            asignacionRepository.Delete(asignacion);

            var result = await unitOfWorkBase.CompleteAsync();
            if (result <= 0)
                return Result.Failure(Errors.Generic.NoActualizado);

            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}