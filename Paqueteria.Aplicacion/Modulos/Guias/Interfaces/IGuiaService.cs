using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;
using Paqueteria.Core.Dto;

namespace Paqueteria.Application.Modulos.Guias.Interfaces;

public interface IGuiaServicio
{
    Task<Resultado<IReadOnlyList<GuiaResponseDto>>> ObtenerTodosAsync();
    Task<Resultado<GuiaResponseDto>> ObtenerPorIdAsync(Guid guiaId);
    Task<Resultado<IReadOnlyList<GuiaResponseDto>>>  ObtenerFiltroAsync(GuiaFiltroDto request);
    Task<Resultado<GuiaCreadaDto>> AgregarAsync(GuiaCreateDto dto);
    Task<Resultado> ActualizarAsync(GuiaUpdateDto dto);
    Task<Resultado> EliminarAsync(Guid guiaId);
}