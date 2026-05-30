using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class SucursalService(ISucursalRepository sucursalRepository, IUnitOfWorkBase unitOfWorkBase) : ISucursalService
{
    public async Task<Result<IReadOnlyList<SucursalResponseDto>>> GetAllAsync()
    {
        try
        {
            var sucursales =  ( await sucursalRepository.GetAllAsync() )
                .Select( SucursalResponseDto.FromEntity ).ToList();

            return Result<IReadOnlyList<SucursalResponseDto>>.Success(sucursales);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result<SucursalResponseDto>> GetByIdAsync(Guid sucursalId)
    {
        try
        {
            var sucursal = (await sucursalRepository.GetByIdAsync(sucursalId));

            if (sucursal == null)
                return Result<SucursalResponseDto>.Failure(Errors.Generic.NoEncontrado);

            return Result<SucursalResponseDto>.Success(SucursalResponseDto.FromEntity(sucursal));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

    }

    public async Task<Result<SucursalResponseDto>> CreateAsync(SucursalCreateDto dto, UserContext currentUser)
    {
        try
        {
            var sucursalIn = dto.ToEntity(currentUser.EmpresaId);
            var sucursal = await sucursalRepository.AddAsync( sucursalIn );

            var result = await unitOfWorkBase.CompleteAsync();

            if (result <= 0)
                return Result<SucursalResponseDto>.Failure(Errors.Generic.NoCreado);

            return Result<SucursalResponseDto>.Success(SucursalResponseDto.FromEntity(sucursal));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<Result> UpdateAsync(SucursaUpdateDto dto, UserContext currentUser)
    {
        try
        {
            var sucursal = await sucursalRepository.GetByIdAsync(dto.SucursalId);

            if (sucursal == null) return Result.Failure(Errors.Generic.NoEncontrado);

            dto.UpdateEntity(sucursal);
            sucursalRepository.Update( sucursal );
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

    public async Task<Result> DeleteAsync(Guid idSucursal,  UserContext currentUser)
    {
        try
        {
            var sucursal = await sucursalRepository.GetByIdAsync(idSucursal);

            if (sucursal == null) return Result.Failure(Errors.Generic.NoEncontrado);

            sucursalRepository.Delete(sucursal);
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
}