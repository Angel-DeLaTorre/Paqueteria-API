using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;

using Paqueteria.Application.Modulos.Usuarios.Dtos;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Entities.Sistema;

namespace Paqueteria.Application.Modulos.Usuarios;

public class UsuarioServicio(
    IUnitOfWork unit,
    IUsuarioContextoServicio contextoUsuario,
    IHashServicio hashServicio
) : IUsuarioServicio
{
    public async Task<Resultado<IReadOnlyList<UsuarioRespuestaDto>>> ObtenerTodosAsync()
    {
        var usuarios = await unit.Usuarios.ObtenerTodosAsync(contextoUsuario.EmpresaId);
        var respuesta = usuarios.Select(u => u.ARespuestaDto()).ToList();
        
        return Resultado<IReadOnlyList<UsuarioRespuestaDto>>.Exitoso(respuesta);
    }

    public Task<Resultado<UsuarioRespuestaDto>> ObtenerPorIdAsync(Guid usuarioId)
    {
        throw new NotImplementedException();
    }

    public async Task<Resultado<UsuarioRespuestaDto>> ObtenerPorUsername(string username)
    {
        var usuario = await unit.Usuarios.ObtenerPorUsernameAsync(username);

        if (usuario == null)
            return Resultado<UsuarioRespuestaDto>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<UsuarioRespuestaDto>.Exitoso(usuario.ARespuestaDto());
    }

    public async Task<Resultado<UsuarioRespuestaDto>> AgregarAsync(UsuarioCrearDto dto)
    {
        var passwordHash = hashServicio.Hash(dto.Password);
        
        
        var usuario = Usuario.Create(dto.Nombre, dto.Username, passwordHash,
            contextoUsuario.EmpresaId);
        
        await unit.Usuarios.AgregarAsync(usuario);

        foreach (var rolId in dto.Roles)
        {
            await unit.Usuarios.AgregarRolAlUsuarioAsync(usuario.Id, rolId, contextoUsuario.EmpresaId);
        }
        
        await unit.CompletarAsync();

        return Resultado<UsuarioRespuestaDto>.Exitoso(usuario.ARespuestaDto());
    }

    public async Task<Resultado> ActualizarAsync(UsuarioActualizarDto dto)
    {
        var usuario = (await unit.Usuarios.ObtenerPorIdAsync(dto.UsuarioId, contextoUsuario.EmpresaId));

        if (usuario == null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        usuario.Actualizar(dto.Nombre);
        await unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> DesactivarAsync(Guid usuarioId)
    {
        var usuario = await unit.Usuarios.ObtenerPorIdAsync(usuarioId, contextoUsuario.EmpresaId);
        if (usuario == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        usuario.Desactivar();
        await  unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }
    
    public async Task<Resultado> ActivarAsync(Guid usuarioId)
    {
        var usuario = await unit.Usuarios.ObtenerPorIdAsync(usuarioId, contextoUsuario.EmpresaId);
        if (usuario == null) return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        usuario.Activar();
        await  unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }

    public async Task<Resultado> EliminarAsync(Guid usuarioId)
    {
        var usuario = (await unit.Usuarios.ObtenerPorIdAsync(usuarioId, contextoUsuario.EmpresaId));

        if (usuario == null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        unit.Usuarios.Eliminar(usuario);
        await unit.CompletarAsync();

        return Resultado.Exitoso();
    }
}