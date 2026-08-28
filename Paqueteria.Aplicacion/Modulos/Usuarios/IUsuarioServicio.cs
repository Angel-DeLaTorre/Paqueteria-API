using Paqueteria.Application.Modulos.Usuarios.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Usuarios;

public interface IUsuarioServicio
{
    Task<Respuesta<IReadOnlyList<UsuarioRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<UsuarioRespuestaDto>> ObtenerPorIdAsync(Guid usuarioId);
    Task<Respuesta<UsuarioRespuestaDto>> ObtenerPorUsername(string username);
    Task<Respuesta<UsuarioRespuestaDto>> AgregarAsync(UsuarioCrearDto dto);
    Task<Respuesta> ActualizarAsync(UsuarioActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid usuarioId);
}