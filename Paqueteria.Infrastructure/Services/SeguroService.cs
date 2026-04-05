using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class SeguroService(ISeguroRepository seguroRepository, IUnitOfWork unitOfWork) : ISeguroService
{
    public async Task<Result<IReadOnlyList<SeguroResponseDto>>> GetAllAsync()
    {
        try
        {
            var seguros = ( await seguroRepository.GetAllAsync() )
                .Select( SeguroResponseDto.FromEntity ).ToList();
            return Result<IReadOnlyList<SeguroResponseDto>>.Success(seguros);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<SeguroResponseDto>> GetByIdAsync(Guid seguroId)
    {
        try
        {
            var seguro = await seguroRepository.GetByIdAsync(seguroId);

            if (seguro is null)
                return Result<SeguroResponseDto>.Failure(CodigoRespuesta.NotFound, "Asignacion no encontrada");

            return Result<SeguroResponseDto>.Success(SeguroResponseDto.FromEntity(seguro));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<SeguroResponseDto>> CreateAsync(SeguroCreateDto dto, UserContext currentUser)
    {
        try
        {
            var seguro = await seguroRepository.AddAsync(dto.ToEntity());

            var result = unitOfWork.CompleteAsync();

            if (result.IsCompletedSuccessfully)
                return Result<SeguroResponseDto>.Failure(CodigoRespuesta.Failure, "Error al crear articulo");

            return Result<SeguroResponseDto>.Success(SeguroResponseDto.FromEntity(seguro));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> UpdateAsync(SeguroUpdateDto dto, UserContext currentUser)
    {
        try
        {
            var seguro = await seguroRepository.GetByIdAsync(dto.SeguroId);

            if (seguro is null)
                return Result.Failure(CodigoRespuesta.NotFound, "Chofer no encontrado");

            dto.UpdateEntity(seguro);
            seguroRepository.Update(seguro);
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

    public async Task<Result> DeleteAsync(Guid seguroId, UserContext currentUser)
    {
        throw new NotImplementedException();
    }
}