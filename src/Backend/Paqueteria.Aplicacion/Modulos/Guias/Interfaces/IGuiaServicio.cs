using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Guias;
using Paqueteria.Dominio.Dto;

namespace Paqueteria.Aplicacion.Modulos.Guias.Interfaces;

public interface IGuiaServicio
{
    Task<Respuesta<IReadOnlyList<GuiaRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<GuiaRespuestaDto>> ObtenerPorIdAsync(Guid guiaId);
    Task<Respuesta<IReadOnlyList<GuiaRespuestaDto>>> ObtenerFiltroAsync(GuiaFiltroDto request);
    Task<Respuesta<GuiaCreadaDto>> AgregarAsync(GuiaCrearDto dto);
    Task<Respuesta> ActualizarAsync(GuiaActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid guiaId);
}