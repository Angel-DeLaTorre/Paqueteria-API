using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Sucursales;

namespace Paqueteria.Aplicacion.Modulos.Sucursales;

public interface ISucursalServicio
{
    Task<Respuesta<IReadOnlyList<SucursalRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<SucursalRespuestaDto>> ObtenerPorIdAsync(Guid sucursalId);
    Task<Respuesta<SucursalRespuestaDto>> AgregarAsync(SucursalCrearDto dto);
    Task<Respuesta> ActualizarAsync(SucursalActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid sucursalId);
}