using Paqueteria.Application.DTOs;
using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Asignaciones;

public interface IAsignacionServicio
{
    Task<Resultado<IReadOnlyList<AsignacionResponseDto>>> ObtenerTodosAsync();
    Task<Resultado<AsignacionResponseDto>> ObtenerPorIdAsync(Guid asignacionId);
    Task<Resultado<AsignacionResponseDto>> AgregarAsync(AsignacionCreateDto dto);
    Task<Resultado> ActualizarAsync(AsignacionUpdateDto dto);
    Task<Resultado> EliminarAsync(Guid asignacionId);
    
    Task<Resultado<byte[]>> GenerarReporteSalidasPdfAsync(Guid? sucursalOrigenId, DateTime? fechaInicio, DateTime? fechaFin);
}