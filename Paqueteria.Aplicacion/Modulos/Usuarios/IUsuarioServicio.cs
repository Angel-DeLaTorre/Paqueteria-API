using Paqueteria.Application.DTOs;
using Paqueteria.Application.Modulos.Usuarios.Dtos;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Usuarios;

public interface IUsuarioServicio
{
    Task<Resultado<IReadOnlyList<UsuarioRespuestaDto>>> ObtenerTodosAsync();
    Task<Resultado<UsuarioRespuestaDto>> ObtenerPorIdAsync(Guid usuarioId);
    Task<Resultado<UsuarioRespuestaDto>> ObtenerPorUsername(string username);
    Task<Resultado<UsuarioRespuestaDto>> AgregarAsync(UsuarioCrearDto dto);
    Task<Resultado> ActualizarAsync(UsuarioActualizarDto dto);
    Task<Resultado> EliminarAsync(Guid usuarioId);
}