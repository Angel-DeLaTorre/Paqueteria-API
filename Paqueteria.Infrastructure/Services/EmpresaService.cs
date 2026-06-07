using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Infrastructure.Services;

public class EmpresaService(IUnitOfWork unit, IUserContextService userContext) : IEmpresaService
{
    public async Task<Result<IReadOnlyList<EmpresaResponseDto>>> GetAllAsync()
    {

        var empresas =  ( await unit.Empresas.GetAllAsync(false) )
            .Select( EmpresaResponseDto.FromEntity ).ToList();

        return Result<IReadOnlyList<EmpresaResponseDto>>.Success(empresas);
    }

    public async Task<Result<EmpresaResponseDto>> GetByIdAsync()
    {
        var empresa = await unit.Empresas.GetByIdAsync(userContext.EmpresaId);

        if (empresa is null)
            return Result<EmpresaResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<EmpresaResponseDto>.Success(EmpresaResponseDto.FromEntity(empresa));
    }

    public async Task<Result<EmpresaResponseDto>> CreateAsync(EmpresaCreateDto dto)
    {
        var empresa = await unit.Empresas.AddAsync(dto.ToEntity());

        var result = await unit.CompleteAsync();

        if (result <= 0)
            return Result<EmpresaResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<EmpresaResponseDto>.Success(EmpresaResponseDto.FromEntity(empresa));
    }

    public async Task<Result> UpdateAsync(EmpresaUpdateDto dto)
    {
        var empresa = (await unit.Empresas.GetByIdAsync(dto.EmpresaId));

        if (empresa is null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        dto.UpdateEntity(empresa);
        await unit.CompleteAsync();

        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid empresaId)
    {
        var empresa = (await unit.Empresas.GetByIdAsync(empresaId));

        if ( empresa is null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        unit.Empresas.Delete(empresa);

        var result = await unit.CompleteAsync();
        if (result <= 0)
            return Result.Failure(ErrorCodes.Generic.NoEliminado);

        return Result.Success();
    }
}