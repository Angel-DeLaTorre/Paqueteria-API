using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class RolService( IUnitOfWork unit, IUserContextService userContext) : IRolService
{
    public async Task<Result<RolResponseDto>> GetByIdAsync(Guid id)
    {
        var rol = await unit.Roles.GetByIdAsync(id, userContext.EmpresaId, includePermissions: true);
        if (rol == null)
            return Result<RolResponseDto>.Failure(ErrorCodes.Generic.NoEncontrado);

        return Result<RolResponseDto>.Success(RolResponseDto.FromEntity(rol));
    }
    
    public async Task<Result<IEnumerable<RolResponseDto>>> GetAllAsync()
    {
        var listaRoles = await unit.Roles.GetAllAsync(userContext.EmpresaId);
        var dtos = listaRoles.Select(RolResponseDto.FromEntity).ToList();
        return Result<IEnumerable<RolResponseDto>>.Success(dtos);
    }
    
    public async Task<Result<RolResponseDto>> CreateAsync(RolCreateDto dto)
    {
        var rolExistente = await unit.Roles.GetByNameAsync(dto.Nombre, userContext.EmpresaId);
        if (rolExistente != null)
            return Result<RolResponseDto>.Failure(ErrorCodes.Generic.Conflicto);
        
        var rol = dto.ToEntity(userContext.EmpresaId);
        await unit.Roles.AddAsync(rol);
        
        if (dto.PermisosIds != null && dto.PermisosIds.Count != 0)
        {
            foreach (var permisoId in dto.PermisosIds)
            {
                await unit.Roles.AddPermissionToRoleAsync(rol.Id, permisoId, userContext.EmpresaId);
            }
        }
        await unit.CompleteAsync();
        var rolCompleto = await unit.Roles.GetByIdAsync(rol.Id, userContext.EmpresaId, includePermissions: true);
        
        return Result<RolResponseDto>.Success(RolResponseDto.FromEntity(rolCompleto !));
    }
    
    public async Task<Result> UpdateAsync(RolUpdateDto dto)
    {
        var rol = await unit.Roles.GetByIdAsync(dto.RoleId, userContext.EmpresaId, includePermissions: true);
        if (rol == null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        dto.UpdateEntity(rol);

        foreach (var rp in rol.RolPermiso.ToList())
        {
            await unit.Roles.RemovePermissionFromRoleAsync(rol.Id, rp.PermisoId, userContext.EmpresaId);
        }

        if (dto.PermisosIds != null && dto.PermisosIds.Count != 0)
        {
            foreach (var permisoId in dto.PermisosIds)
            {
                await unit.Roles.AddPermissionToRoleAsync(rol.Id, permisoId, userContext.EmpresaId);
            }
        }
        
        await unit.CompleteAsync();
        return Result.Success();
    }
    public async Task<Result> ActivarAsync(Guid rolId)
    {
        var rol = await unit.Roles.GetByIdAsync(rolId, userContext.EmpresaId);
        if (rol == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        rol.Estatus = EstatusBasico.Activo;
        await unit.CompleteAsync();
        
        return Result.Success();
    }
    public async Task<Result> DesactivarAsync(Guid rolId)
    {
        var rol = await unit.Roles.GetByIdAsync(rolId, userContext.EmpresaId);
        if (rol == null) return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        rol.Estatus = EstatusBasico.Inactivo;
        await unit.CompleteAsync();
        
        return Result.Success();
    }
    public async Task<Result> DeleteAsync(Guid id)
    {
        var rol = await unit.Roles.GetByIdAsync(id, userContext.EmpresaId);
        if (rol == null)
            return Result.Failure(ErrorCodes.Generic.NoEncontrado);
        
        unit.Roles.Delete(rol);
        await unit.CompleteAsync();

        return Result.Success();
    }
}