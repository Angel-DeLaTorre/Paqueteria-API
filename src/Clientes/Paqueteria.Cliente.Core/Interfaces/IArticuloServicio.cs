using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Articulos;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface IArticuloServicio
{
    Task<Respuesta<List<ArticuloRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<ArticuloRespuestaDto>> CrearAsync(ArticuloCrearDto dto);
}