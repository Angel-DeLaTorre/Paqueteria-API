using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Dtos;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Permisos.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;

namespace Paqueteria.Application.Modulos.Permisos;

public sealed class PermisoServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IPermisoServicio
{
    public async Task<Respuesta<PermisoResponseDto>> ObtenerPorIdAsync(Guid id)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(id, contextoUsuario.EmpresaId);

        if (permiso == null)
            return Respuesta<PermisoResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<PermisoResponseDto>.Exitoso(PermisoResponseDto.FromEntity(permiso));
    }
    
    public async Task<Respuesta<IEnumerable<PermisoResponseDto>>> ObtenerTodosAsync()
    {
        var listaPermisos = await unit.Permisos.ObtenerTodosAsync(contextoUsuario.EmpresaId);
        
        var dtos = listaPermisos.Select(PermisoResponseDto.FromEntity).ToList();
        
        return Respuesta<IEnumerable<PermisoResponseDto>>.Exitoso(dtos);
    }
    
    public async Task<Respuesta<PermisoResponseDto>> AgregarAsync(PermisoCreateDto dto)
    {
        var permisoExistente = await unit.Permisos.ObtenerPorNombreAsync(dto.Nombre, contextoUsuario.EmpresaId);
        if (permisoExistente != null)
            return Respuesta<PermisoResponseDto>.Error(CodigosError.Generic.NoEncontrado);
        
        var nuevoPermiso = dto.ToEntity(contextoUsuario.EmpresaId);
        
        await unit.Permisos.AgregarAsync(nuevoPermiso);
        await unit.GuardarCambiosAsync();

        return Respuesta<PermisoResponseDto>.Exitoso(PermisoResponseDto.FromEntity(nuevoPermiso));
    }
    
    public async Task<Respuesta> ActualizarAsync(PermisoUpdateDto dto)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(dto.PermisoId, contextoUsuario.EmpresaId);
        if (permiso == null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        dto.UpdateEntity(permiso);
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> DesactivarAsync(Guid permisoId)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(permisoId, contextoUsuario.EmpresaId);
        if (permiso == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        permiso.Desactivar();
        await  unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> ActivarAsync(Guid permisoId)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(permisoId, contextoUsuario.EmpresaId);
        if (permiso == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        permiso.Activar();
        await  unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> EliminarAsync(Guid id)
    {
        var permiso = await unit.Permisos.ObtenerPorIdAsync(id, contextoUsuario.EmpresaId);
        if (permiso == null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        unit.Permisos.Eliminar(permiso);
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }
}