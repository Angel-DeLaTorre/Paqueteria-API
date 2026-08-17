using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Reportes.Constantes;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.Modulos.Asignaciones;

public sealed class AsignacionServicio (
    IUnitOfWork unit,
    IUsuarioContextoServicio contextoUsuario,
    IFabricaPdf fabricaPdf
) : IAsignacionServicio
{
    public async Task<Resultado<IReadOnlyList<AsignacionResponseDto>>> ObtenerTodosAsync()
    {
        var asignaciones = ( await unit.Asignaciones.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( AsignacionResponseDto.FromEntity ).ToList();
        return Resultado<IReadOnlyList<AsignacionResponseDto>>.Exitoso(asignaciones);
    }

    public async Task<Resultado<AsignacionResponseDto>> ObtenerPorIdAsync(Guid asignacionId)
    {
        var asignacion = await unit.Asignaciones.ObtenerPorIdAsync(asignacionId, contextoUsuario.EmpresaId);

        if (asignacion is null)
            return Resultado<AsignacionResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<AsignacionResponseDto>.Exitoso(AsignacionResponseDto.FromEntity(asignacion));
    }

    public async Task<Resultado<AsignacionResponseDto>> AgregarAsync(AsignacionCreateDto dto)
    {
        
        var asignacion = Asignacion.Crear(
            dto.SucursalOrigenId,
            dto.SucursalDestinoId,
            dto.FechaPartida,
            dto.St1,
            dto.St2,
            dto.St3,
            dto.St4,
            dto.ChoferId,
            contextoUsuario.EmpresaId
        );
        

        foreach (var guiaId in dto.GuiasId)
        {
            var guia = await unit.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId);
            if (guia != null) asignacion.AgregarGuia(guia);
        }
        
        await unit.Asignaciones.AgregarAsync(asignacion);
        
        
        var resultado = await unit.CompletarAsync();
        
        if (resultado <= 0)
            return Resultado<AsignacionResponseDto>.Error(CodigosError.Generic.NoCreado);

        return Resultado<AsignacionResponseDto>.Exitoso(AsignacionResponseDto.FromEntity(asignacion));
    }

    public async Task<Resultado> ActualizarAsync(AsignacionUpdateDto dto)
    {
        var asignacion = await unit.Asignaciones.ObtenerPorIdAsync(dto.Id, contextoUsuario.EmpresaId);

        if (asignacion == null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(asignacion);
        await unit.CompletarAsync();
        
        return Resultado.Exitoso();
    }

    public async Task<Resultado> EliminarAsync(Guid asignacionId)
    {
        var asignacion = (await unit.Asignaciones.ObtenerPorIdAsync(asignacionId, contextoUsuario.EmpresaId));

        if (asignacion == null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        unit.Asignaciones.Eliminar(asignacion);
        await unit.CompletarAsync();

        return Resultado.Exitoso();
    }

    #region Reportes

    public async Task<Resultado<byte[]>> GenerarReporteSalidasPdfAsync(Guid? sucursalOrigenId, DateTime? fechaInicio, DateTime? fechaFin)
    {
        var hoyUtc = DateTime.UtcNow.Date;

        var inicio = fechaInicio.HasValue
            ? DateTime.SpecifyKind(fechaInicio.Value.Date, DateTimeKind.Utc)
            : DateTime.SpecifyKind(hoyUtc, DateTimeKind.Utc);

        var fin = fechaFin.HasValue
            ? DateTime.SpecifyKind(fechaFin.Value.Date.AddDays(1).AddTicks(-1), DateTimeKind.Utc)
            : DateTime.SpecifyKind(hoyUtc.AddDays(1).AddTicks(-1), DateTimeKind.Utc);

        // 2. Consulta al repositorio con las fechas en formato UTC
        var asignaciones = await unit.Asignaciones.ObtenerParaReporteSalidasAsync(
            empresaId: contextoUsuario.EmpresaId,
            sucursalOrigenId: sucursalOrigenId,
            fechaInicio: inicio,
            fechaFin: fin
        );

        if (!asignaciones.Any())
            return Resultado<byte[]>.Error(CodigosError.Generic.NoEncontrado);

        // 3. Generar el PDF
        var estrategia = fabricaPdf.SeleccionarEstrategia(TipoDocumentoPdf.ReporteSalidaOperador);
        var pdfBytes = await estrategia.GenerarPdfAsync(asignaciones);

        return Resultado<byte[]>.Exitoso(pdfBytes);
    }

    #endregion
}