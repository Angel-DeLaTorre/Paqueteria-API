using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Dtos.Asignaciones;

namespace Paqueteria.Aplicacion.Modulos.Asignaciones;

public interface IAsignacionServicio
{
    Task<Respuesta<IReadOnlyList<AsignacionRespuestaDto>>> ObtenerTodosAsync();
    Task<Respuesta<AsignacionRespuestaDto>> ObtenerPorIdAsync(Guid asignacionId);
    Task<Respuesta<AsignacionRespuestaDto>> AgregarAsync(AsignacionCrearDto dto);
    Task<Respuesta> ActualizarAsync(AsignacionActualizarDto dto);
    Task<Respuesta> EliminarAsync(Guid asignacionId);

    Task<Respuesta<byte[]>> GenerarReporteSalidasPdfAsync(Guid? sucursalOrigenId, DateTime? fechaInicio,
        DateTime? fechaFin);
}