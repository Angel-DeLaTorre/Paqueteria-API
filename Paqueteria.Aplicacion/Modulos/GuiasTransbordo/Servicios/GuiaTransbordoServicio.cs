using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.GuiasTransbordo.Dtos;
using Paqueteria.Application.Modulos.GuiasTransbordo.Interfaces;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;
using Paqueteria.Core.Entidades.Remisiones;

namespace Paqueteria.Application.Modulos.GuiasTransbordo.Servicios;

public class GuiaTransbordoServicio (
    IUnitOfWork unit, 
    IGuiaTransbordoMapeador mapeador,
    IUsuarioContextoServicio contextoUsuario) : IGuiaTransbordoServicio
{
    /// <summary>
    /// Registra la bajada/arribo de una guía a una sucursal (Descarga física del camión).
    /// </summary>
    public async Task<Respuesta<GuiaTransbordoResponseDto>> RegistrarIngresoAsync(RegistrarIngresoTransbordoDto dto)
    {
        var guia = await unit.Guias.ObtenerPorIdAsync(dto.GuiaId, contextoUsuario.EmpresaId);
        if (guia == null)
            return Respuesta<GuiaTransbordoResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        var asignacion = await unit.Asignaciones.ObtenerPorIdAsync(dto.AsignacionId, contextoUsuario.EmpresaId);
        if (asignacion == null)
            return Respuesta<GuiaTransbordoResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        // 1. Cierra el registro del transbordo anterior si seguía abierto
        var transbordoActivo = await unit.GuiaTransbordos.ObtenerUltimoTransbordoActivoAsync(dto.GuiaId);
        transbordoActivo?.RegistrarSalida();

        // 2. Método de Dominio: Actualiza SucursalActualId, pone AsignacionId = null 
        // y evalúa si el estatus es EnBodegaOrigen, EnBodegaTransbordo o EnBodegaDestino
        guia.ArrivoASucursal(dto.SucursalTransbordoId);

        // 3. Registra el evento de escaneo en la bitácora
        var nuevoTransbordo = GuiaTransbordo.RegistrarIngreso(
            dto.GuiaId,
            dto.AsignacionId,
            dto.SucursalTransbordoId,
            contextoUsuario.UsuarioId,
            dto.Observaciones
        );

        await unit.GuiaTransbordos.AgregarAsync(nuevoTransbordo);

        var guardado = await unit.GuardarCambiosAsync();
        if (guardado <= 0)
            return Respuesta<GuiaTransbordoResponseDto>.Error(CodigosError.Generic.NoCreado);

        return Respuesta<GuiaTransbordoResponseDto>.Exitoso(mapeador.ARespuestaDto(nuevoTransbordo));
    }

    /// <summary>
    /// Registra la subida/despacho de una guía a un vehículo (Carga al camión).
    /// </summary>
    public async Task<Respuesta> RegistrarSalidaAsync(Guid guiaId, Guid asignacionId)
    {
        var guia = await unit.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId);
        if (guia == null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);

        var asignacion = await unit.Asignaciones.ObtenerPorIdAsync(asignacionId, contextoUsuario.EmpresaId);
        if (asignacion == null)
            return Respuesta.Error(CodigosError.Generic.NoEncontrado);
        
        var transbordoActivo = await unit.GuiaTransbordos.ObtenerUltimoTransbordoActivoAsync(guiaId);
        transbordoActivo?.RegistrarSalida();
        
        guia.AsignarATransporte(asignacionId);

        await unit.GuardarCambiosAsync();
        return Respuesta.Exitoso();
    }

    public async Task<Respuesta<IReadOnlyList<GuiaTransbordoResponseDto>>> ObtenerHistorialPorGuiaAsync(Guid guiaId)
    {
        var historial = await unit.GuiaTransbordos.ObtenerPorGuiaIdAsync(guiaId);
        return Respuesta<IReadOnlyList<GuiaTransbordoResponseDto>>.Exitoso(mapeador.AListaRespuestaDto(historial));
    }
}