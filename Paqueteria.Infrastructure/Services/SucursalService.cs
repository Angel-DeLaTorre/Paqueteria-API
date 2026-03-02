using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Repositories;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class SucursalService(ISucursalRepository repoSucursal) : ISucursalService
{
    public async Task<Result<IReadOnlyList<SucursalResponseDto>>> GetAllAsync()
    {
        try
        {
            var sucursales =  ( await repoSucursal.GetAllAsync() )
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
            var sucursal = (await repoSucursal.GetByIdAsync(sucursalId));

            if (sucursal == null)
                return Result<SucursalResponseDto>.Failure(CodigoRespuesta.NotFound, "Sucursal no encontrada");

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
            var sucursal = await repoSucursal.AddAsync( dto.ToEntity() );

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
            var sucursal = await repoSucursal.GetByIdAsync(dto.SucursalId);

            if (sucursal == null) return Result.Failure(CodigoRespuesta.NotFound, "Sucursal no encontrada");

            dto.UpdateEntity(sucursal);
            repoSucursal.Update( sucursal );

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
            var sucursal = await repoSucursal.GetByIdAsync(idSucursal);

            if (sucursal == null) return Result.Failure(CodigoRespuesta.NotFound, "Sucursal no encontrada");

            repoSucursal.Delete(sucursal);

            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}