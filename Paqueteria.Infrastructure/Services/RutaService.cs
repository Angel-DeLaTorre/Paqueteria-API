using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class RutaService(IUnitOfWork unit, IUserContextService userContext) : IRutaService
{
    public async Task<Result<IReadOnlyList<RutaResponseDto>>> GetAllAsync()
    {
        var rutas = ( await unit.Rutas.GetAllAsync(userContext.EmpresaId) )
            .Select( RutaResponseDto.FromEntity ).ToList();
        return Result<IReadOnlyList<RutaResponseDto>>.Success(rutas);
    }

    public async Task<Result<RutaResponseDto>> GetByIdAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.GetByIdAsync(rutaId, userContext.EmpresaId);

        if (ruta is null)
            return Result<RutaResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<RutaResponseDto>.Success(RutaResponseDto.FromEntity(ruta));
    }

    public async Task<Result<RutaResponseDto>> CreateAsync(RutaCreateDto dto)
    {
        var ruta = await unit.Rutas.AddAsync(dto.ToEntity(userContext.EmpresaId));

        await unit.CompleteAsync();
        
        ruta = await unit.Rutas.GetByIdAsync(ruta.Id, userContext.EmpresaId);

        return Result<RutaResponseDto>.Success(RutaResponseDto.FromEntity(ruta));
    }

    public async Task<Result> UpdateAsync(RutaUpdateDto dto)
    {
        var ruta = await unit.Rutas.GetByIdAsync(dto.RutaId, userContext.EmpresaId);

        if (ruta is null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        dto.UpdateEntity(ruta);
        await unit.CompleteAsync();

        return Result.Success();
    }
    
    public async Task<Result> DesactivarAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.GetByIdAsync(rutaId, userContext.EmpresaId);
        if (ruta == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        ruta.Estatus = EstatusBasico.Inactivo;
        await  unit.CompleteAsync();
        
        return Result.Success();
    }
    
    public async Task<Result> ActivarAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.GetByIdAsync(rutaId, userContext.EmpresaId);
        if (ruta == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        ruta.Estatus = EstatusBasico.Activo;
        await  unit.CompleteAsync();
        
        return Result.Success();
    }

    public async Task<Result> DeleteAsync(Guid rutaId)
    {
        var ruta = await unit.Rutas.GetByIdAsync(rutaId, userContext.EmpresaId);

        if (ruta is null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        unit.Rutas.Delete(ruta);
        await unit.CompleteAsync();

        return Result.Success();
    }
}