using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;

using Paqueteria.Application.Modulos.Usuarios.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;
using Paqueteria.Core.Entidades.Sistema;

namespace Paqueteria.Application.Modulos.Usuarios;

public class UsuarioServicio(
    IUnitOfWork unit,
    IUsuarioContextoServicio contextoUsuario,
    IHashServicio hashServicio
) : IUsuarioServicio
{
    public async Task<Respuesta<IReadOnlyList<UsuarioRespuestaDto>>> ObtenerTodosAsync()
    {
        var usuarios = await unit.Usuarios.ObtenerTodosAsync(contextoUsuario.EmpresaId);
        var respuesta = usuarios.Select(u => u.ARespuestaDto()).ToList();
        
        return Respuesta<IReadOnlyList<UsuarioRespuestaDto>>.Exitoso(respuesta);
    }

    public Task<Respuesta<UsuarioRespuestaDto>> ObtenerPorIdAsync(Guid usuarioId)
    {
        throw new NotImplementedException();
    }

    public async Task<Respuesta<UsuarioRespuestaDto>> ObtenerPorUsername(string username)
    {
        var usuario = await unit.Usuarios.ObtenerPorUsernameAsync(username);

        if (usuario == null)
            return Respuesta<UsuarioRespuestaDto>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<UsuarioRespuestaDto>.Exitoso(usuario.ARespuestaDto());
    }

    public async Task<Respuesta<UsuarioRespuestaDto>> AgregarAsync(UsuarioCrearDto dto)
    {
        var passwordHash = hashServicio.Hash(dto.Password);
        
        
        var usuario = Usuario.Create(dto.Nombre, dto.Username, passwordHash,
            contextoUsuario.EmpresaId);
        
        await unit.Usuarios.AgregarAsync(usuario);

        foreach (var rolId in dto.Roles)
        {
            await unit.Usuarios.AgregarRolAlUsuarioAsync(usuario.Id, rolId, contextoUsuario.EmpresaId);
        }
        
        await unit.GuardarCambiosAsync();

        return Respuesta<UsuarioRespuestaDto>.Exitoso(usuario.ARespuestaDto());
    }

    public async Task<Respuesta> ActualizarAsync(UsuarioActualizarDto dto)
    {
        var usuario = (await unit.Usuarios.ObtenerPorIdAsync(dto.UsuarioId, contextoUsuario.EmpresaId));

        if (usuario == null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        usuario.Actualizar(dto.Nombre);
        await unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> DesactivarAsync(Guid usuarioId)
    {
        var usuario = await unit.Usuarios.ObtenerPorIdAsync(usuarioId, contextoUsuario.EmpresaId);
        if (usuario == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        usuario.Desactivar();
        await  unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }
    
    public async Task<Respuesta> ActivarAsync(Guid usuarioId)
    {
        var usuario = await unit.Usuarios.ObtenerPorIdAsync(usuarioId, contextoUsuario.EmpresaId);
        if (usuario == null) return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        usuario.Activar();
        await  unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> EliminarAsync(Guid usuarioId)
    {
        var usuario = (await unit.Usuarios.ObtenerPorIdAsync(usuarioId, contextoUsuario.EmpresaId));

        if (usuario == null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        unit.Usuarios.Eliminar(usuario);
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }
}