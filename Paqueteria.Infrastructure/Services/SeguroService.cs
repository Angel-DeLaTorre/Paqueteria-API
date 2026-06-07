using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Infrastructure.Services;

public class SeguroService(IUnitOfWork unit, IUserContextService userContext) : ISeguroService
{
    public async Task<Result<IReadOnlyList<SeguroResponseDto>>> GetAllAsync()
    {
        var seguros = ( await unit.Seguros.GetAllAsync(userContext.EmpresaId, false) )
            .Select( SeguroResponseDto.FromEntity ).ToList();
        return Result<IReadOnlyList<SeguroResponseDto>>.Success(seguros);
    }

    public async Task<Result<SeguroResponseDto>> GetByIdAsync(Guid seguroId)
    {
        var seguro = await unit.Seguros.GetByIdAsync(seguroId, userContext.EmpresaId, false);

        if (seguro is null)
            return Result<SeguroResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<SeguroResponseDto>.Success(SeguroResponseDto.FromEntity(seguro));
    }

    public async Task<Result<SeguroResponseDto>> CreateAsync(SeguroCreateDto dto)
    {
        var seguro = await unit.Seguros.AddAsync(dto.ToEntity());

        var result = unit.CompleteAsync();

        if (result.IsCompletedSuccessfully)
            return Result<SeguroResponseDto>.Failure(ErrorCodes.Generic.NoCreado);

        return Result<SeguroResponseDto>.Success(SeguroResponseDto.FromEntity(seguro));
    }

    public async Task<Result> UpdateAsync(SeguroUpdateDto dto)
    {
        var seguro = await unit.Seguros.GetByIdAsync(dto.SeguroId, userContext.EmpresaId);

        if (seguro is null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        dto.UpdateEntity(seguro);
        var result = await  unit.CompleteAsync();

        if (result != 0)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid seguroId)
    {
        throw new NotImplementedException();
    }
}