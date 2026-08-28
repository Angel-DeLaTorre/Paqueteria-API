using Paqueteria.Application.Modulos.GuiasTransbordo.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.GuiasTransbordo.Interfaces;

public interface IGuiaTransbordoServicio
{
    Task<Respuesta<GuiaTransbordoResponseDto>> RegistrarIngresoAsync(RegistrarIngresoTransbordoDto dto);
    Task<Respuesta> RegistrarSalidaAsync(Guid guiaId, Guid asignacionId);
    Task<Respuesta<IReadOnlyList<GuiaTransbordoResponseDto>>> ObtenerHistorialPorGuiaAsync(Guid guiaId);
}