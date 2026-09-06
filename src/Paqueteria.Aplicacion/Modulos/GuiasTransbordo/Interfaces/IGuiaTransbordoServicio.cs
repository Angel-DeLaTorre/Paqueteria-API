using Paqueteria.Application.Modulos.GuiasTransbordo.Dtos;
using Paqueteria.Dominio.Comun;

namespace Paqueteria.Application.Modulos.GuiasTransbordo.Interfaces;

public interface IGuiaTransbordoServicio
{
    Task<Respuesta<GuiaTransbordoRespuestaDto>> RegistrarIngresoAsync(RegistrarIngresoTransbordoDto dto);
    Task<Respuesta> RegistrarSalidaAsync(Guid guiaId, Guid asignacionId);
}