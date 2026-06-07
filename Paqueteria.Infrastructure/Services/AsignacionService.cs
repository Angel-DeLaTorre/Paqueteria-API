using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Infrastructure.Services;

public class AsignacionService (IUnitOfWork unit, IUserContextService userContext) : IAsignacionSerivce
{
    public async Task<Result<IReadOnlyList<AsignacionResponseDto>>> GetAllAsync()
    {
        var asignaciones = ( await unit.Asignaciones.GetAllAsync(userContext.EmpresaId) )
            .Select( AsignacionResponseDto.FromEntity ).ToList();
        return Result<IReadOnlyList<AsignacionResponseDto>>.Success(asignaciones);
    }

    public async Task<Result<AsignacionResponseDto>> GetByIdAsync(Guid asignacionId)
    {
        var asignacion = await unit.Asignaciones.GetByIdAsync(asignacionId, userContext.EmpresaId);

        if (asignacion is null)
            return Result<AsignacionResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<AsignacionResponseDto>.Success(AsignacionResponseDto.FromEntity(asignacion));
    }

    public async Task<Result<AsignacionResponseDto>> CreateAsync(AsignacionCreateDto dto)
    {
        var asignacion = await unit.Asignaciones.AddAsync(dto.ToEntity());
        await unit.CompleteAsync();

        return Result<AsignacionResponseDto>.Success(AsignacionResponseDto.FromEntity(asignacion));
    }

    public async Task<Result> UpdateAsync(AsignacionUpdateDto dto)
    {
        var asignacion = await unit.Asignaciones.GetByIdAsync(dto.Id, userContext.EmpresaId);

        if (asignacion == null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        dto.UpdateEntity(asignacion);
        await unit.CompleteAsync();
        
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid asignacionId)
    {
        var asignacion = (await unit.Asignaciones.GetByIdAsync(asignacionId, userContext.EmpresaId));

        if (asignacion == null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        unit.Asignaciones.Delete(asignacion);
        await unit.CompleteAsync();

        return Result.Success();
    }
}