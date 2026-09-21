using Paqueteria.Aplicacion.Comun;
using Paqueteria.Aplicacion.Modulos.GuiasTransbordo.Dtos;
using Paqueteria.Comun.Comun;

namespace Paqueteria.Aplicacion.Modulos.GuiasTransbordo.Interfaces;

public interface IGuiaTransbordoServicio
{
    Task<Respuesta<GuiaTransbordoRespuestaDto>> RegistrarIngresoAsync(RegistrarIngresoTransbordoDto dto);
    Task<Respuesta> RegistrarSalidaAsync(Guid guiaId, Guid asignacionId);
}