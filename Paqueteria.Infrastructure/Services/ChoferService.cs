using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;
using Paqueteria.Infrastructure.Persistence;

namespace Paqueteria.Infrastructure.Services;

public class ChoferService(IChoferRepository choferRepository, UnitOfWork unitOfWork) : IChoferService
{
    public async Task<Result<IReadOnlyList<ChoferResponseDto>>> GetAllAsync()
    {
        try
        {
            var choferes = ( await choferRepository.GetAllAsync() )
                .Select( ChoferResponseDto.FromEntity ).ToList();
            return Result<IReadOnlyList<ChoferResponseDto>>.Success(choferes);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ChoferResponseDto>> GetByIdAsync(Guid choferId)
    {
        try
        {
            var chofer = await choferRepository.GetByIdAsync(choferId);

            if (chofer is null)
                return Result<ChoferResponseDto>.Failure(CodigoRespuesta.NotFound, "Asignacion no encontrada");

            return Result<ChoferResponseDto>.Success(ChoferResponseDto.FromEntity(chofer));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<ChoferResponseDto>> CreateAsync(ChoferCreateDto dto, UserContext currentUser)
    {
        try
        {
            var chofer = await choferRepository.AddAsync(dto.ToEntity());

            var result = unitOfWork.CompleteAsync();

            if (result.IsCompletedSuccessfully)
                return Result<ChoferResponseDto>.Failure(CodigoRespuesta.Failure, "Error al crear articulo");

            return Result<ChoferResponseDto>.Success(ChoferResponseDto.FromEntity(chofer));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> UpdateAsync(ChoferUpdateDto dto, UserContext currentUser)
    {
        try
        {
            var chofer = await choferRepository.GetByIdAsync(dto.ChoferId);

            if (chofer is null)
                return Result.Failure(CodigoRespuesta.NotFound, "Chofer no encontrado");

            dto.UpdateEntity(chofer);
            choferRepository.Update(chofer);
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

    public async Task<Result> DeleteAsync(Guid choferId, UserContext currentUser)
    {
        try
        {
            var chofer = (await choferRepository.GetByIdAsync(choferId));

            if (chofer is null)
                return Result.Failure(CodigoRespuesta.NotFound, "Chofer no encontrado");

            choferRepository.Delete(chofer);

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