using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class RolService( IUnitOfWork unit) : IRolService
{
    public async Task<Result<RolResponseDto>> GetByIdAsync(Guid id, UserContext currentUser)
    {
        try
        {
            var rol = await unit.Roles.GetByIdAsync(id, currentUser.EmpresaId, includePermissions: true);

            if (rol == null)
                return Result<RolResponseDto>.Failure(Errors.Generic.NoEncontrado);

            return Result<RolResponseDto>.Success(RolResponseDto.FromEntity(rol));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Result<IEnumerable<RolResponseDto>>> GetAllAsync(UserContext currentUser)
    {
        try
        {
            var listaRoles = await unit.Roles.GetAllAsync(currentUser.EmpresaId);
            
            var dtos = listaRoles.Select(RolResponseDto.FromEntity).ToList();
            
            return Result<IEnumerable<RolResponseDto>>.Success(dtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Result<RolResponseDto>> CreateAsync(RolCreateDto dto, UserContext currentUser)
    {
        try
        {
            var rolExistente = await unit.Roles.GetByNameAsync(dto.Nombre, currentUser.EmpresaId);
            if (rolExistente != null)
                return Result<RolResponseDto>.Failure(Errors.Generic.Conflicto);
            
            var rol = dto.ToEntity(currentUser.EmpresaId);
            await unit.Roles.AddAsync(rol);
            
            if (dto.PermisosIds != null && dto.PermisosIds.Count != 0)
            {
                foreach (var permisoId in dto.PermisosIds)
                {
                    await unit.Roles.AddPermissionToRoleAsync(rol.Id, permisoId, currentUser.EmpresaId);
                }
            }
            await unit.CompleteAsync();
            var rolCompleto = await unit.Roles.GetByIdAsync(rol.Id, currentUser.EmpresaId, includePermissions: true);
            
            return Result<RolResponseDto>.Success(RolResponseDto.FromEntity(rolCompleto !));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Result> UpdateAsync(RolUpdateDto dto, UserContext currentUser)
    {
        try
        {
            var rol = await unit.Roles.GetByIdAsync(dto.RoleId, currentUser.EmpresaId, includePermissions: true);
            if (rol == null)
                return Result.Failure(Errors.Generic.NoEncontrado);
            
            dto.UpdateEntity(rol);

            foreach (var rp in rol.RolPermiso.ToList())
            {
                await unit.Roles.RemovePermissionFromRoleAsync(rol.Id, rp.PermisoId, currentUser.EmpresaId);
            }

            if (dto.PermisosIds != null && dto.PermisosIds.Count != 0)
            {
                foreach (var permisoId in dto.PermisosIds)
                {
                    await unit.Roles.AddPermissionToRoleAsync(rol.Id, permisoId, currentUser.EmpresaId);
                }
            }
            
            await unit.CompleteAsync();
            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Result> DeleteAsync(Guid id, UserContext currentUser)
    {
        try
        {
            var rol = await unit.Roles.GetByIdAsync(id, currentUser.EmpresaId);
            if (rol == null)
                return Result.Failure(Errors.Generic.NoEncontrado);
            
            unit.Roles.Delete(rol);
            await unit.CompleteAsync();

            return Result.Success();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}