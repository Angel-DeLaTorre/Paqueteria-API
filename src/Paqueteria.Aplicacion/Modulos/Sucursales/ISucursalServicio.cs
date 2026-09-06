using Paqueteria.Application.Modulos.Sucursales.Dtos;
using Paqueteria.Dominio.Comun;

namespace Paqueteria.Application.Modulos.Sucursales;

public interface ISucursalServicio
{
    Task<Respuesta<IReadOnlyList<SucursalRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<SucursalRespuestaDto>> ObtenerPorIdAsync(Guid sucursalId);
    Task<Respuesta<SucursalRespuestaDto>> AgregarAsync(SucursalCrearDto dto);
    Task<Respuesta> ActualizarAsync(SucursalActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid sucursalId);
}