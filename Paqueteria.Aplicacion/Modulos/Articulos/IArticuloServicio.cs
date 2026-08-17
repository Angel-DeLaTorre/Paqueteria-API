using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Articulos;

public interface IArticuloServicio
{
    Task<Resultado<IReadOnlyList<ArticuloResponseDto>>> ObtenerTodosAsync();
    Task<Resultado<ArticuloResponseDto>> ObtenerPorIdAsync(Guid articuloId);
    Task<Resultado<ArticuloResponseDto>> AgregarAsync(ArticuloCreateDto dto);
    Task<Resultado> ActualizarAsync(ArticuloUpdateDto dto);
    Task<Resultado> EliminarAsync(Guid articuloId);
}