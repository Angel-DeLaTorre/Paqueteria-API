using Paqueteria.Application.Modulos.Articulos.Dtos;
using Paqueteria.Dominio.Comun;

namespace Paqueteria.Application.Modulos.Articulos;

public interface IArticuloServicio
{
    Task<Respuesta<IReadOnlyList<ArticuloRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<ArticuloRespuestaDto>> ObtenerPorIdAsync(Guid articuloId);
    Task<Respuesta<ArticuloRespuestaDto>> AgregarAsync(ArticuloCrearDto dto);
    Task<Respuesta> ActualizarAsync(ArticuloActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid articuloId);
}