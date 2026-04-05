using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class AsignacionService (IAsignacionRepository asignacionRepository, IUnitOfWork unitOfWork) : IAsignacionSerivce
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
                return Result<AsignacionResponseDto>.Failure(CodigoRespuesta.NotFound, "Asignacion no encontrada");

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

            var result = unitOfWork.CompleteAsync();

            if (result.IsCompletedSuccessfully)
                return Result<AsignacionResponseDto>.Failure(CodigoRespuesta.Failure, "Error al crear articulo");

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
                return Result.Failure(CodigoRespuesta.NotFound, "Asignacion no encontrado");

            dto.UpdateEntity(asignacion);
            asignacionRepository.Update(asignacion);
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

    public async Task<Result> DeleteAsync(Guid asignacionId, UserContext currentUser)
    {
        try
        {
            var asignacion = (await asignacionRepository.GetByIdAsync(asignacionId));

            if (asignacion == null)
                return Result.Failure(CodigoRespuesta.NotFound, "Asignacion no encontrado");

            asignacionRepository.Delete(asignacion);

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