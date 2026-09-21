using Paqueteria.Aplicacion.Comun;
using Paqueteria.Aplicacion.Comun.Interfaces;
using Paqueteria.Aplicacion.Interfaces.Persistence;
using Paqueteria.Aplicacion.Modulos.Folios;
using Paqueteria.Aplicacion.Modulos.Reportes.Constantes;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Comun.Errores;
using Paqueteria.Comun.Dtos.Asignaciones;
using Paqueteria.Comun.Enums;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Aplicacion.Modulos.Asignaciones;

public sealed class AsignacionServicio(
    IUnitOfWork unit,
    IUsuarioContextoServicio contextoUsuario,
    IFabricaPdf fabricaPdf,
    IFolioServicio folioServicio
) : IAsignacionServicio
{
    public async Task<Respuesta<IReadOnlyList<AsignacionRespuestaDto>>> ObtenerTodosAsync()
    {
        var asignaciones = (await unit.Asignaciones.ObtenerTodosAsync(contextoUsuario.EmpresaId))
            .Select(a => a.MapearRespuestaDto()).ToList();
        return Respuesta<IReadOnlyList<AsignacionRespuestaDto>>.Exitoso(asignaciones);
    }

    public async Task<Respuesta<AsignacionRespuestaDto>> ObtenerPorIdAsync(Guid asignacionId)
    {
        var asignacion = await unit.Asignaciones.ObtenerPorIdAsync(asignacionId, contextoUsuario.EmpresaId);

        return asignacion is not null
            ? Respuesta<AsignacionRespuestaDto>.Exitoso(asignacion.MapearRespuestaDto())
            : Respuesta<AsignacionRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);
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
        return resultado <= 0
            ? Respuesta<AsignacionRespuestaDto>.Exitoso(asignacion.MapearRespuestaDto())
            : Respuesta<AsignacionRespuestaDto>.Error(CodigosError.Comun.NoCreado);
    }

    public async Task<Respuesta> ActualizarAsync(AsignacionActualizarDto dto)
    {
        var asignacion = await unit.Asignaciones.ObtenerPorIdAsync(dto.AsignacionId, contextoUsuario.EmpresaId);

        if (asignacion == null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        asignacion.ActualizarDatos
        (
            dto.FechaPartida,
            dto.SucursalOrigenId,
            dto.SucursalDestinoId
        );

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0
            ? Respuesta.Exitoso()
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    public async Task<Respuesta> EliminarAsync(Guid asignacionId)
    {
        var asignacion = await unit.Asignaciones.ObtenerPorIdAsync(asignacionId, contextoUsuario.EmpresaId);

        if (asignacion == null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        unit.Asignaciones.Eliminar(asignacion);

        var resultado = await unit.GuardarCambiosAsync();
        return resultado > 0
            ? Respuesta.Exitoso()
            : Respuesta.Error(CodigosError.Comun.NoActualizado);
    }

    #region Reportes

    public async Task<Respuesta<byte[]>> GenerarReporteSalidasPdfAsync(Guid? sucursalOrigenId, DateTime? fechaInicio,
        DateTime? fechaFin)
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
            contextoUsuario.EmpresaId,
            sucursalOrigenId,
            inicio,
            fin
        );

        if (!asignaciones.Any())
            return Respuesta<byte[]>.Error(CodigosError.Comun.NoEncontrado);

        // 3. Generar el PDF
        var estrategia = fabricaPdf.SeleccionarEstrategia(TipoDocumentoPdf.ReporteSalidaOperador);
        var pdfBytes = await estrategia.GenerarPdfAsync(asignaciones);

        return Respuesta<byte[]>.Exitoso(pdfBytes);
    }

    #endregion
}