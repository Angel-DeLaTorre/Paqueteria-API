using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Articulos.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Articulos;

public interface IArticuloServicio
{
    Task<Respuesta<IReadOnlyList<ArticuloResponseDto>>> ObtenerTodosAsync();
    Task<Respuesta<ArticuloResponseDto>> ObtenerPorIdAsync(Guid articuloId);
    Task<Respuesta<ArticuloResponseDto>> AgregarAsync(ArticuloCreateDto dto);
    Task<Respuesta> ActualizarAsync(ArticuloUpdateDto dto);
    Task<Respuesta> EliminarAsync(Guid articuloId);
}