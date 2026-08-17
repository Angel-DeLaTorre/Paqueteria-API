using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Application.Modulos.Permisos;

public sealed class PermisoServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IPermisoServicio
{
    public async Task<Resultado<PermisoResponseDto>> ObtenerPorIdAsync(Guid id)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(id, contextoUsuario.EmpresaId);

        if (permiso == null)
            return Resultado<PermisoResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<PermisoResponseDto>.Exitoso(PermisoResponseDto.FromEntity(permiso));
    }
    
    public async Task<Resultado<IEnumerable<PermisoResponseDto>>> ObtenerTodosAsync()
    {
        var listaPermisos = await unit.Permisos.ObtenerTodosAsync(contextoUsuario.EmpresaId);
        
        var dtos = listaPermisos.Select(PermisoResponseDto.FromEntity).ToList();
        
        return Resultado<IEnumerable<PermisoResponseDto>>.Exitoso(dtos);
    }
    
    public async Task<Resultado<PermisoResponseDto>> AgregarAsync(PermisoCreateDto dto)
    {
        var permisoExistente = await unit.Permisos.ObtenerPorNombreAsync(dto.Nombre, contextoUsuario.EmpresaId);
        if (permisoExistente != null)
            return Resultado<PermisoResponseDto>.Error(CodigosError.Generic.NoEncontrado);
        
        var nuevoPermiso = dto.ToEntity(contextoUsuario.EmpresaId);
        
        await unit.Permisos.AgregarAsync(nuevoPermiso);
        await unit.CompletarAsync();

        return Resultado<PermisoResponseDto>.Exitoso(PermisoResponseDto.FromEntity(nuevoPermiso));
    }
    
    public async Task<Resultado> ActualizarAsync(PermisoUpdateDto dto)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(dto.PermisoId, contextoUsuario.EmpresaId);
        if (permiso == null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        dto.UpdateEntity(permiso);
        await unit.CompletarAsync();

        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> DesactivarAsync(Guid permisoId)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(permisoId, contextoUsuario.EmpresaId);
        if (permiso == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        permiso.Desactivar();
        await  unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> ActivarAsync(Guid permisoId)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(permisoId, contextoUsuario.EmpresaId);
        if (permiso == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        permiso.Activar();
        await  unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> EliminarAsync(Guid id)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(id, contextoUsuario.EmpresaId);
        if (permiso == null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        unit.Permisos.Eliminar(permiso);
        await unit.CompletarAsync();

        return Resultado.Exitoso();
    }
}