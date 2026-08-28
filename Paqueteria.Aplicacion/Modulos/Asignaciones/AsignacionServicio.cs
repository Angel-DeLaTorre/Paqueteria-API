using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Dtos;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Asignaciones.Dtos;
using Paqueteria.Application.Modulos.Folios;
using Paqueteria.Application.Modulos.Reportes.Constantes;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;
using Paqueteria.Core.Entidades.Remisiones;
using Paqueteria.Core.Enums;

namespace Paqueteria.Application.Modulos.Asignaciones;

public sealed class AsignacionServicio (
    IUnitOfWork unit,
    IUsuarioContextoServicio contextoUsuario,
    IFabricaPdf fabricaPdf,
    IFolioServicio folioServicio
) : IAsignacionServicio
{
    public async Task<Respuesta<IReadOnlyList<AsignacionRespuestaDto>>> ObtenerTodosAsync()
    {
        var asignaciones = ( await unit.Asignaciones.ObtenerTodosAsync(contextoUsuario.EmpresaId) )
            .Select( AsignacionRespuestaDto.FromEntity ).ToList();
        return Respuesta<IReadOnlyList<AsignacionRespuestaDto>>.Exitoso(asignaciones);
    }

    public async Task<Respuesta<AsignacionRespuestaDto>> ObtenerPorIdAsync(Guid asignacionId)
    {
        var asignacion = await unit.Asignaciones.ObtenerPorIdAsync(asignacionId, contextoUsuario.EmpresaId);

        if (asignacion is null)
            return Respuesta<AsignacionRespuestaDto>.Error(CodigosError.Generic.NoEncontrado);

        return Respuesta<AsignacionRespuestaDto>.Exitoso(AsignacionRespuestaDto.FromEntity(asignacion));
    }

    public async Task<Respuesta<AsignacionRespuestaDto>> AgregarAsync(AsignacionCrearDto dto)
    {

        var clave = await folioServicio.GenerarSiguienteFolioAsync(dto.SucursalOrigenId, TipoFolio.Asignacion);
        
        var asignacion = Asignacion.Crear(
            clave,
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
        
        
        var resultado = await unit.GuardarCambiosAsync();
        
        if (resultado <= 0)
            return Respuesta<AsignacionRespuestaDto>.Error(CodigosError.Generic.NoCreado);

        return Respuesta<AsignacionRespuestaDto>.Exitoso(AsignacionRespuestaDto.FromEntity(asignacion));
    }

    public async Task<Respuesta> ActualizarAsync(AsignacionActualizarDto dto)
    {
        var asignacion = await unit.Asignaciones.ObtenerPorIdAsync(dto.AsignacionId, contextoUsuario.EmpresaId);

        if (asignacion == null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        dto.UpdateEntity(asignacion);
        await unit.GuardarCambiosAsync();
        
        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> EliminarAsync(Guid asignacionId)
    {
        var asignacion = (await unit.Asignaciones.ObtenerPorIdAsync(asignacionId, contextoUsuario.EmpresaId));

        if (asignacion == null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        unit.Asignaciones.Eliminar(asignacion);
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }

    #region Reportes

    public async Task<Respuesta<byte[]>> GenerarReporteSalidasPdfAsync(Guid? sucursalOrigenId, DateTime? fechaInicio, DateTime? fechaFin)
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
            return Respuesta<byte[]>.Error(CodigosError.Generic.NoEncontrado);

        // 3. Generar el PDF
        var estrategia = fabricaPdf.SeleccionarEstrategia(TipoDocumentoPdf.ReporteSalidaOperador);
        var pdfBytes = await estrategia.GenerarPdfAsync(asignaciones);

        return Respuesta<byte[]>.Exitoso(pdfBytes);
    }

    #endregion
}