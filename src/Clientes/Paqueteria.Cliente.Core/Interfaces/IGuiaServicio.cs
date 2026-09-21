using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Guias;

namespace Paqueteria.Cliente.Core.Interfaces;

public interface IGuiaServicio
{
    Task<Respuesta<List<GuiaRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<GuiaRespuestaDto>> ObtenerPorIdAsync(Guid id);
    Task<Respuesta<GuiaRespuestaDto>> ObtenerPorNumeroGuiaAsync(string numeroGuia);

    Task<Respuesta<GuiaRespuestaDto>> CrearAsync(GuiaCrearDto dto);
    
    Task<Respuesta> ActualizarAsync(GuiaActualizarDto dto);

    Task<Respuesta> GenerarEtiquetaAsync(Guid guiaId);
    //Task<Respuesta<CotizacionGuiaDto>> CotizarGuiaAsync(CotizacionGuiaRequestDto dto);
}