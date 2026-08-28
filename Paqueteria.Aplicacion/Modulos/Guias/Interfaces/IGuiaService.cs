using Paqueteria.Application.Modulos.Guias.Dtos;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Dto;

namespace Paqueteria.Application.Modulos.Guias.Interfaces;

public interface IGuiaServicio
{
    Task<Respuesta<IReadOnlyList<GuiaRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<GuiaRespuestaDto>> ObtenerPorIdAsync(Guid guiaId);
    Task<Respuesta<IReadOnlyList<GuiaRespuestaDto>>>  ObtenerFiltroAsync(GuiaFiltroDto request);
    Task<Respuesta<GuiaCreadaDto>> AgregarAsync(GuiaCrearDto dto);
    Task<Respuesta> ActualizarAsync(GuiaActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid guiaId);
}