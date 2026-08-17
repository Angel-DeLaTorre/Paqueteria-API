using Paqueteria.Application.Modulos.GuiasTransbordo.Dtos;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.GuiasTransbordo.Interfaces;

public interface IGuiaTransbordoServicio
{
    Task<Resultado<GuiaTransbordoResponseDto>> RegistrarIngresoAsync(RegistrarIngresoTransbordoDto dto);
    Task<Resultado> RegistrarSalidaAsync(Guid guiaId);
    Task<Resultado<IReadOnlyList<GuiaTransbordoResponseDto>>> ObtenerHistorialPorGuiaAsync(Guid guiaId);
}