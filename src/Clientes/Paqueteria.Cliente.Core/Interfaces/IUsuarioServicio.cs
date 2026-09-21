using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Usuarios;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface IUsuarioServicio
{
    Task<Respuesta<List<UsuarioRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<UsuarioRespuestaDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<UsuarioRespuestaDto>> CrearAsync(UsuarioCrearDto dto);
    Task<Respuesta> ActualizarAsync(Guid id, UsuarioActualizarDto dto);
}