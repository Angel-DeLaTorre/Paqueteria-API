using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Infrastructure.Services;

public class GuiaService(IUnitOfWork unit, IUserContextService userContext) : IGuiaService
{
    public async Task<Result<IReadOnlyList<GuiaResponseDto>>> GetAllAsync()
    {
        var guias = ( await unit.Guias.GetAllAsync(userContext.EmpresaId, false) )
            .Select( GuiaResponseDto.FromEntity ).ToList();
        return Result<IReadOnlyList<GuiaResponseDto>>.Success(guias);
    }

    public async Task<Result<GuiaResponseDto>> GetByIdAsync(Guid guiaId)
    {
        var guia = await unit.Guias.GetByIdAsync(guiaId, userContext.EmpresaId, false);

        if (guia is null)
            return Result<GuiaResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<GuiaResponseDto>.Success(GuiaResponseDto.FromEntity(guia));
    }

    public async Task<Result<GuiaResponseDto>> CreateAsync(GuiaCreateDto dto)
    {
        var guia = await unit.Guias.AddAsync(dto.ToEntity(userContext.UserId, userContext.EmpresaId));

        var result = unit.CompleteAsync();

        if (result.IsCompletedSuccessfully)
            return Result<GuiaResponseDto>.Failure(ErrorCodes.Generic.NoCreado);

        return Result<GuiaResponseDto>.Success(GuiaResponseDto.FromEntity(guia));
    }

    public async Task<Result> UpdateAsync(GuiaUpdateDto dto)
    {
        var guia = await unit.Guias.GetByIdAsync(dto.GuiaId, userContext.EmpresaId);

        if (guia is null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        dto.UpdateEntity(guia);
        await unit.CompleteAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid guiaId)
    {
        var guia = await unit.Guias.GetByIdAsync(guiaId, userContext.EmpresaId, false);

        if (guia is null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        unit.Guias.Delete(guia);
        await unit.CompleteAsync();

        return Result.Success();
    }
}