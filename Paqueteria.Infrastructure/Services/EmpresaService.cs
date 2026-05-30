using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class EmpresaService(IEmpresaRepository empresaRepository, IUnitOfWorkBase unitOfWorkBase) : IEmpresaService
{
    public async Task<Result<IReadOnlyList<EmpresaResponseDto>>> GetAllAsync()
    {
        try
        {
            var empresas =  ( await empresaRepository.GetAllAsync() )
                .Select( EmpresaResponseDto.FromEntity ).ToList();

            return Result<IReadOnlyList<EmpresaResponseDto>>.Success(empresas);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<EmpresaResponseDto>> GetByIdAsync(Guid id)
    {
        try
        {
            var empresa = (await empresaRepository.GetByIdAsync(id));

            if (empresa is null)
                return Result<EmpresaResponseDto>.Failure(Errors.Generic.NoEncontrado);

            return Result<EmpresaResponseDto>.Success(EmpresaResponseDto.FromEntity(empresa));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<EmpresaResponseDto>> CreateAsync(EmpresaCreateDto dto, UserContext currentUser)
    {
        try
        {
            var empresa = await empresaRepository.AddAsync(dto.ToEntity());

            var result = await unitOfWorkBase.CompleteAsync();

            if (result <= 0)
                return Result<EmpresaResponseDto>.Failure(Errors.Generic.NoEncontrado);

            return Result<EmpresaResponseDto>.Success(EmpresaResponseDto.FromEntity(empresa));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> UpdateAsync(EmpresaUpdateDto dto, UserContext currentUser)
    {
        try
        {
            var empresa = (await empresaRepository.GetByIdAsync(dto.EmpresaId));

            if (empresa is null)
                return Result.Failure(Errors.Generic.NoEncontrado);

            dto.UpdateEntity(empresa);
            empresaRepository.Update(empresa);
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

    public async Task<Result> DeleteAsync(Guid empresaId, UserContext currentUser)
    {
        try
        {
            var empresa = (await empresaRepository.GetByIdAsync(empresaId));

            if ( empresa is null)
                return Result.Failure(Errors.Generic.NoEncontrado);

            empresaRepository.Delete(empresa);

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