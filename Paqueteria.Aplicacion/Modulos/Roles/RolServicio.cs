using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Dtos;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Roles.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;

namespace Paqueteria.Application.Modulos.Roles;

public sealed class RolServicio( IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IRolServicio
{
    public async Task<Respuesta<RolResponseDto>> ObtenerPorIdAsync(Guid id)
    {
        var rol = await unit.Roles.ObtenerPorIdAsync(id, contextoUsuario.EmpresaId, includePermissions: true);
        if (rol == null)
            return Respuesta<RolResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<RolResponseDto>.Exitoso(RolResponseDto.FromEntity(rol));
    }
    
    public async Task<Respuesta<IEnumerable<RolResponseDto>>> ObtenerTodosAsync()
    {
        var listaRoles = await unit.Roles.ObtenerTodosAsync(contextoUsuario.EmpresaId);
        var dtos = listaRoles.Select(RolResponseDto.FromEntity).ToList();
        return Respuesta<IEnumerable<RolResponseDto>>.Exitoso(dtos);
    }
    
    public async Task<Respuesta<RolResponseDto>> AgregarAsync(RolCreateDto dto)
    {
        var rolExistente = await unit.Roles.ObtenerPorNombreAsync(dto.Nombre, contextoUsuario.EmpresaId);
        if (rolExistente != null)
            return Respuesta<RolResponseDto>.Error(CodigosError.Generic.Conflicto);
        
        var rol = dto.ToEntity(contextoUsuario.EmpresaId);
        await unit.Roles.AgregarAsync(rol);
        
        if (dto.PermisosIds != null && dto.PermisosIds.Count != 0)
        {
            foreach (var permisoId in dto.PermisosIds)
            {
                await unit.Roles.AgregarPermisoAlRolAsync(rol.Id, permisoId, contextoUsuario.EmpresaId);
            }
        }
        await unit.GuardarCambiosAsync();
        var rolCompleto = await unit.Roles.ObtenerPorIdAsync(rol.Id, contextoUsuario.EmpresaId, includePermissions: true);
        
        return Respuesta<RolResponseDto>.Exitoso(RolResponseDto.FromEntity(rolCompleto !));
    }
    
    public async Task<Respuesta> ActualizarAsync(RolUpdateDto dto)
    {
        var rol = await unit.Roles.ObtenerPorIdAsync(dto.RoleId, contextoUsuario.EmpresaId, includePermissions: true);
        if (rol == null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        dto.UpdateEntity(rol);

        foreach (var rp in rol.RolPermiso.ToList())
        {
            await unit.Roles.RemoverPermisoAlRolAsync(rol.Id, rp.PermisoId, contextoUsuario.EmpresaId);
        }

        if (dto.PermisosIds != null && dto.PermisosIds.Count != 0)
        {
            foreach (var permisoId in dto.PermisosIds)
            {
                await unit.Roles.AgregarPermisoAlRolAsync(rol.Id, permisoId, contextoUsuario.EmpresaId);
            }
        }
        
        await unit.GuardarCambiosAsync();
        return Respuesta.Exitoso();
    }
    public async Task<Respuesta> ActivarAsync(Guid rolId)
    {
        var rol = await unit.Roles.ObtenerPorIdAsync(rolId, contextoUsuario.EmpresaId);
        if (rol == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        rol.Activar();
        await unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
    public async Task<Respuesta> DesactivarAsync(Guid rolId)
    {
        var rol = await unit.Roles.ObtenerPorIdAsync(rolId, contextoUsuario.EmpresaId);
        if (rol == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        rol.Desactivar();
        await unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
    public async Task<Respuesta> EliminarAsync(Guid id)
    {
        var rol = await unit.Roles.ObtenerPorIdAsync(id, contextoUsuario.EmpresaId);
        if (rol == null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        unit.Roles.Eliminar(rol);
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }
}