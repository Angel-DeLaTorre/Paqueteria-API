using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class RutaService(IRutaRepository rutaRepository, IUnitOfWorkBase unitOfWorkBase) : IRutaService
{
    public async Task<Result<IReadOnlyList<RutaResponseDto>>> GetAllAsync()
    {
        try
        {
            var rutas = ( await rutaRepository.GetAllAsync() )
                .Select( RutaResponseDto.FromEntity ).ToList();
            return Result<IReadOnlyList<RutaResponseDto>>.Success(rutas);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<RutaResponseDto>> GetByIdAsync(Guid rutaId)
    {
        try
        {
            var ruta = await rutaRepository.GetByIdAsync(rutaId);

            if (ruta is null)
                return Result<RutaResponseDto>.Failure(CodigoRespuesta.NotFound, "Asignacion no encontrada");

            return Result<RutaResponseDto>.Success(RutaResponseDto.FromEntity(ruta));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<RutaResponseDto>> CreateAsync(RutaCreateDto dto, UserContext currentUser)
    {
        try
        {
            var ruta = await rutaRepository.AddAsync(dto.ToEntity(currentUser.EmpresaId));

            var result = unitOfWorkBase.CompleteAsync();

            if (result.IsCompletedSuccessfully)
                return Result<RutaResponseDto>.Failure(CodigoRespuesta.Failure, "Error al crear articulo");

            return Result<RutaResponseDto>.Success(RutaResponseDto.FromEntity(ruta));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> UpdateAsync(RutaUpdateDto dto, UserContext currentUser)
    {
        try
        {
            var ruta = await rutaRepository.GetByIdAsync(dto.RutaId);

            if (ruta is null)
                return Result.Failure(CodigoRespuesta.NotFound, "Chofer no encontrado");

            dto.UpdateEntity(ruta);
            rutaRepository.Update(ruta);
            var result = await  unitOfWorkBase.CompleteAsync();

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

    public async Task<Result> DeleteAsync(Guid rutaId, UserContext currentUser)
    {
        try
        {
            var ruta = (await rutaRepository.GetByIdAsync(rutaId));

            if (ruta is null)
                return Result.Failure(CodigoRespuesta.NotFound, "Ruta no encontrada");

            rutaRepository.Delete(ruta);

            var result = await unitOfWorkBase.CompleteAsync();
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