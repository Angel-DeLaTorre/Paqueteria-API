using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class SucursalService(IUnitOfWork unit, IUserContextService userContext) : ISucursalService
{
    public async Task<Result<IReadOnlyList<SucursalResponseDto>>> GetAllAsync()
    {
        var sucursales =  ( await unit.Sucursales.GetAllAsync(userContext.EmpresaId) )
            .Select( SucursalResponseDto.FromEntity ).ToList();

        return Result<IReadOnlyList<SucursalResponseDto>>.Success(sucursales);
    }

    public async Task<Result<SucursalResponseDto>> GetByIdAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.GetByIdAsync(sucursalId, userContext.EmpresaId);
        if (sucursal == null)
            return Result<SucursalResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<SucursalResponseDto>.Success(SucursalResponseDto.FromEntity(sucursal));
    }

    public async Task<Result<SucursalResponseDto>> CreateAsync(SucursalCreateDto dto)
    {
        var sucursalIn = dto.ToEntity(userContext.EmpresaId);
        var sucursal = await unit.Sucursales.AddAsync( sucursalIn );

        var result = await unit.CompleteAsync();

        if (result <= 0)
            return Result<SucursalResponseDto>.Failure(ErrorCodes.Generic.NoCreado);

        return Result<SucursalResponseDto>.Success(SucursalResponseDto.FromEntity(sucursal));
    }

    public async Task<Result> UpdateAsync(SucursaUpdateDto dto)
    {
        var sucursal = await unit.Sucursales.GetByIdAsync(dto.SucursalId, userContext.EmpresaId);

        if (sucursal == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        dto.UpdateEntity(sucursal);
        unit.Sucursales.Update( sucursal );
        var result = await  unit.CompleteAsync();

        if (result != 0)
            return Result.Failure(ErrorCodes.Generic.NoActualizado);

        return Result.Success();
    }
    
    public async Task<Result> DesactivarAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.GetByIdAsync(sucursalId, userContext.EmpresaId);
        if (sucursal == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        sucursal.Estatus = EstatusBasico.Inactivo;
        await  unit.CompleteAsync();
        
        return Result.Success();
    }
    
    public async Task<Result> ActivarAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.GetByIdAsync(sucursalId, userContext.EmpresaId);
        if (sucursal == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        sucursal.Estatus = EstatusBasico.Activo;
        await  unit.CompleteAsync();
        
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid idSucursal)
    {
        var sucursal = await unit.Sucursales.GetByIdAsync(idSucursal, userContext.EmpresaId);

        if (sucursal == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        unit.Sucursales.Delete(sucursal);
        var result = await  unit.CompleteAsync();

        if (result != 0)
            return Result.Failure(ErrorCodes.Generic.NoActualizado);

        return Result.Success();
    }
}