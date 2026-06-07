using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class PermisoService(IUnitOfWork unit, IUserContextService userContext) : IPermisoService
{
    public async Task<Result<PermisoResponseDto>> GetByIdAsync(Guid id)
    {
        var permiso = await unit.Permisos.GetByIdAsync(id, userContext.EmpresaId);

        if (permiso == null)
            return Result<PermisoResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<PermisoResponseDto>.Success(PermisoResponseDto.FromEntity(permiso));
    }
    
    public async Task<Result<IEnumerable<PermisoResponseDto>>> GetAllAsync()
    {
        var listaPermisos = await unit.Permisos.GetAllAsync(userContext.EmpresaId);
        
        var dtos = listaPermisos.Select(PermisoResponseDto.FromEntity).ToList();
        
        return Result<IEnumerable<PermisoResponseDto>>.Success(dtos);
    }
    
    public async Task<Result<PermisoResponseDto>> CreateAsync(PermisoCreateDto dto)
    {
        var permisoExistente = await unit.Permisos.GetByNameAsync(dto.Nombre, userContext.EmpresaId);
        if (permisoExistente != null)
            return Result<PermisoResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);
        
        var nuevoPermiso = dto.ToEntity(userContext.EmpresaId);
        
        await unit.Permisos.AddAsync(nuevoPermiso);
        await unit.CompleteAsync();

        return Result<PermisoResponseDto>.Success(PermisoResponseDto.FromEntity(nuevoPermiso));
    }
    
    public async Task<Result> UpdateAsync(PermisoUpdateDto dto)
    {
        var permiso = await unit.Permisos.GetByIdAsync(dto.PermisoId, userContext.EmpresaId);
        if (permiso == null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        dto.UpdateEntity(permiso);
        await unit.CompleteAsync();

        return Result.Success();
    }
    
    public async Task<Result> DesactivarAsync(Guid permisoId)
    {
        var permiso = await unit.Permisos.GetByIdAsync(permisoId, userContext.EmpresaId);
        if (permiso == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        permiso.Estatus = EstatusBasico.Inactivo;
        await  unit.CompleteAsync();
        
        return Result.Success();
    }
    
    public async Task<Result> ActivarAsync(Guid permisoId)
    {
        var permiso = await unit.Permisos.GetByIdAsync(permisoId, userContext.EmpresaId);
        if (permiso == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        permiso.Estatus = EstatusBasico.Activo;
        await  unit.CompleteAsync();
        
        return Result.Success();
    }
    
    public async Task<Result> DeleteAsync(Guid id)
    {
        var permiso = await unit.Permisos.GetByIdAsync(id, userContext.EmpresaId);
        if (permiso == null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);

        unit.Permisos.Delete(permiso);
        await unit.CompleteAsync();

        return Result.Success();
    }
}