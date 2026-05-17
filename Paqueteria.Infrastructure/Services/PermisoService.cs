using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Interfaces.Services;
using Paqueteria.Core.Common;
using Paqueteria.Core.Enums;

namespace Paqueteria.Infrastructure.Services;

public class PermisoService(IUnitOfWork unit) : IPermisoService
{
    public async Task<Result<PermisoResponseDto>> GetByIdAsync(Guid id, UserContext currentUser)
    {
        try
        {
            var permiso = await unit.Permisos.GetByIdAsync(id, currentUser.EmpresaId);

            if (permiso == null)
                return Result<PermisoResponseDto>.Failure(CodigoRespuesta.NotFound, "Permiso no encontrado.");

            return Result<PermisoResponseDto>.Success(PermisoResponseDto.FromEntity(permiso));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // Listar todos los permisos que le pertenecen a la empresa actual
    public async Task<Result<IEnumerable<PermisoResponseDto>>> GetAllAsync(UserContext currentUser)
    {
        try
        {
            var listaPermisos = await unit.Permisos.GetAllAsync(currentUser.EmpresaId);
            
            var dtos = listaPermisos.Select(PermisoResponseDto.FromEntity).ToList();
            
            return Result<IEnumerable<PermisoResponseDto>>.Success(dtos);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    // Crear un nuevo permiso dentro del catálogo de la empresa
    public async Task<Result<PermisoResponseDto>> CreateAsync(PermisoCreateDto dto, UserContext currentUser)
    {
        try
        {
            var permisoExistente = await unit.Permisos.GetByNameAsync(dto.Nombre, currentUser.EmpresaId);
            if (permisoExistente != null)
                return Result<PermisoResponseDto>.Failure(CodigoRespuesta.Conflict, $"El permiso '{dto.Nombre}' ya existe en esta empresa.");
            
            var nuevoPermiso = dto.ToEntity(currentUser.EmpresaId);
            
            await unit.Permisos.AddAsync(nuevoPermiso);
            await unit.CompleteAsync();

            return Result<PermisoResponseDto>.Success(PermisoResponseDto.FromEntity(nuevoPermiso));
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public async Task<Result> UpdateAsync(PermisoUpdateDto dto, UserContext currentUser)
    {
        try
        {
            var permiso = await unit.Permisos.GetByIdAsync(dto.PermisoId, currentUser.EmpresaId);
            if (permiso == null)
                return Result.Failure(CodigoRespuesta.NotFound, "Permiso no encontrado.");
            
            dto.UpdateEntity(permiso);
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
            var permiso = await unit.Permisos.GetByIdAsync(id, currentUser.EmpresaId);
            if (permiso == null)
                return Result.Failure(CodigoRespuesta.NotFound, "Permiso no encontrado.");

            unit.Permisos.Delete(permiso);
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