using Paqueteria.Aplicacion.Comun;
using Paqueteria.Aplicacion.Comun.Interfaces;
using Paqueteria.Aplicacion.Interfaces.Persistence;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Comun.Errores;
using Paqueteria.Comun.Dtos.Usuarios;
using Paqueteria.Dominio.Entidades.Sistema;

namespace Paqueteria.Aplicacion.Modulos.Usuarios;

public class UsuarioServicio(
    IUnitOfWork unit,
    IUsuarioContextoServicio contextoUsuario,
    IHashServicio hashServicio
) : IUsuarioServicio
{
    public async Task<Respuesta<IReadOnlyList<UsuarioRespuestaDto>>> ObtenerTodosAsync()
    {
        var usuarios = await unit.Usuarios.ObtenerTodosAsync(contextoUsuario.EmpresaId);
        var respuesta = usuarios.Select(u => u.MapeaRespuestaDto()).ToList();

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
            return Respuesta<UsuarioRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);

        return Respuesta<UsuarioRespuestaDto>.Exitoso(usuario.MapeaRespuestaDto());
    }

    public async Task<Respuesta<UsuarioRespuestaDto>> AgregarAsync(UsuarioCrearDto dto)
    {
        var passwordHash = hashServicio.Hash(dto.Password);


        var usuario = Usuario.Create(dto.Nombre, dto.Username, passwordHash,
            contextoUsuario.EmpresaId);

        await unit.Usuarios.AgregarAsync(usuario);

        foreach (var rolId in dto.Roles)
            await unit.Usuarios.AgregarRolAlUsuarioAsync(usuario.Id, rolId, contextoUsuario.EmpresaId);

        await unit.GuardarCambiosAsync();

        return Respuesta<UsuarioRespuestaDto>.Exitoso(usuario.MapeaRespuestaDto());
    }

    public async Task<Respuesta> ActualizarAsync(UsuarioActualizarDto dto)
    {
        var usuario = await unit.Usuarios.ObtenerPorIdAsync(dto.UsuarioId, contextoUsuario.EmpresaId);

        if (usuario == null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        usuario.Actualizar(dto.Nombre);
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> EliminarAsync(Guid usuarioId)
    {
        var usuario = await unit.Usuarios.ObtenerPorIdAsync(usuarioId, contextoUsuario.EmpresaId);

        if (usuario == null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        unit.Usuarios.Eliminar(usuario);
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> DesactivarAsync(Guid usuarioId)
    {
        var usuario = await unit.Usuarios.ObtenerPorIdAsync(usuarioId, contextoUsuario.EmpresaId);
        if (usuario == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        usuario.Desactivar();
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> ActivarAsync(Guid usuarioId)
    {
        var usuario = await unit.Usuarios.ObtenerPorIdAsync(usuarioId, contextoUsuario.EmpresaId);
        if (usuario == null) return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        usuario.Activar();
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }
}