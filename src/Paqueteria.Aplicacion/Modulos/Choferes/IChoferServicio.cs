using Paqueteria.Application.Modulos.Choferes.Dtos;
using Paqueteria.Dominio.Comun;

namespace Paqueteria.Application.Modulos.Choferes;

public interface IChoferServicio
{
    Task<Respuesta<IReadOnlyList<ChoferRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<ChoferRespuestaDto>> ObtenerPorIdAsync(Guid choferId);
    Task<Respuesta<ChoferRespuestaDto>> AgregarAsync(ChoferCrearDto dto);
    Task<Respuesta> ActualizarAsync(ChoferActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid choferId);
    Task<Respuesta> ActivarAsync(Guid choferId);
    Task<Respuesta> DesactivarAsync(Guid choferId);
}