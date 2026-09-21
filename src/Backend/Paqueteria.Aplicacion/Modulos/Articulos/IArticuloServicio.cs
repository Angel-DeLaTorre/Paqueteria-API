using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Articulos;

namespace Paqueteria.Aplicacion.Modulos.Articulos;

public interface IArticuloServicio
{
    Task<Respuesta<IReadOnlyList<ArticuloRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<ArticuloRespuestaDto>> ObtenerPorIdAsync(Guid articuloId);
    Task<Respuesta<ArticuloRespuestaDto>> AgregarAsync(ArticuloCrearDto dto);
    Task<Respuesta> ActualizarAsync(ArticuloActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid articuloId);
}