using Paqueteria.Application.Dtos;
using Paqueteria.Application.Modulos.Asignaciones.Dtos;
using Paqueteria.Core.Comun;

namespace Paqueteria.Application.Modulos.Asignaciones;

public interface IAsignacionServicio
{
    Task<Respuesta<IReadOnlyList<AsignacionRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<AsignacionRespuestaDto>> ObtenerPorIdAsync(Guid asignacionId);
    Task<Respuesta<AsignacionRespuestaDto>> AgregarAsync(AsignacionCrearDto dto);
    Task<Respuesta> ActualizarAsync(AsignacionActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid asignacionId);
    
    Task<Respuesta<byte[]>> GenerarReporteSalidasPdfAsync(Guid? sucursalOrigenId, DateTime? fechaInicio, DateTime? fechaFin);
}