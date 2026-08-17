using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.GuiasTransbordo.Dtos;
using Paqueteria.Application.Modulos.GuiasTransbordo.Interfaces;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.Modulos.GuiasTransbordo.Servicios;

public class GuiaTransbordoServicio (
    IUnitOfWork unit, 
    IGuiaTransbordoMapeador mapeador,
    IUsuarioContextoServicio contextoUsuario) : IGuiaTransbordoServicio
{
    public async Task<Resultado<GuiaTransbordoResponseDto>> RegistrarIngresoAsync(RegistrarIngresoTransbordoDto dto)
    {
        var guia = await unit.Guias.ObtenerPorIdAsync(dto.GuiaId, contextoUsuario.EmpresaId);
        if (guia == null)
            return Resultado<GuiaTransbordoResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        var asignacion = await unit.Asignaciones.ObtenerPorIdAsync(dto.AsignacionId, contextoUsuario.EmpresaId);
        if (asignacion == null)
            return Resultado<GuiaTransbordoResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        // 1. Finalizar transbordo previo en caso de existir
        var transbordoActivo = await unit.GuiaTransbordos.ObtenerUltimoTransbordoActivoAsync(dto.GuiaId);
        transbordoActivo?.RegistrarSalida();

        // 2. Vincular guía a la nueva asignación
        guia.Asignar(dto.AsignacionId);

        // 3. Crear el evento de ingreso
        var nuevoTransbordo = GuiaTransbordo.RegistrarIngreso(
            dto.GuiaId,
            dto.AsignacionId,
            dto.SucursalTransbordoId,
            contextoUsuario.UsuarioId,
            dto.Observaciones
        );

        await unit.GuiaTransbordos.AgregarAsync(nuevoTransbordo);
        var guardado = await unit.CompletarAsync();

        if (guardado <= 0)
            return Resultado<GuiaTransbordoResponseDto>.Error(CodigosError.Generic.NoCreado);

        // Transformación mediante el método en memoria del mapeador
        return Resultado<GuiaTransbordoResponseDto>.Exitoso(mapeador.ARespuestaDto(nuevoTransbordo));
    }

    public async Task<Resultado> RegistrarSalidaAsync(Guid guiaId)
    {
        var guia = await unit.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId);
        if (guia == null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        var transbordoActivo = await unit.GuiaTransbordos.ObtenerUltimoTransbordoActivoAsync(guiaId);
        if (transbordoActivo == null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        // Se registra la hora de salida del transbordo y se desasigna la guía del transporte actual
        transbordoActivo.RegistrarSalida();
        guia.Desasignar();

        await unit.CompletarAsync();
        return Resultado.Exitoso();
    }

    public async Task<Resultado<IReadOnlyList<GuiaTransbordoResponseDto>>> ObtenerHistorialPorGuiaAsync(Guid guiaId)
    {
        var historial = await unit.GuiaTransbordos.ObtenerPorGuiaIdAsync(guiaId);
        
        // Transformación de la colección usando el mapeador inyectado
        var dtos = mapeador.AListaRespuestaDto(historial);

        return Resultado<IReadOnlyList<GuiaTransbordoResponseDto>>.Exitoso(dtos);
    }
}