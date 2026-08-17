using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.DTOs;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Guias.Interfaces;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;
using Paqueteria.Core.Dto;
using Paqueteria.Core.Entities.Remisiones;

namespace Paqueteria.Application.Modulos.Guias.Servicios;

public sealed class GuiaServicio(IUnitOfWork unit, IUsuarioContextoServicio contextoUsuario) : IGuiaServicio
{
    public async Task<Resultado<IReadOnlyList<GuiaResponseDto>>> ObtenerTodosAsync()
    {
        var guias = ( await unit.Guias.ObtenerTodosAsync(contextoUsuario.EmpresaId, false) )
            .Select( GuiaResponseDto.FromEntity ).ToList();
        return Resultado<IReadOnlyList<GuiaResponseDto>>.Exitoso(guias);
    }

    public async Task<Resultado<GuiaResponseDto>> ObtenerPorIdAsync(Guid guiaId)
    {
        var guia = await unit.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId, false);

        if (guia is null)
            return Resultado<GuiaResponseDto>.Error(CodigosError.Generic.NoEncontrado);

        return Resultado<GuiaResponseDto>.Exitoso(GuiaResponseDto.FromEntity(guia));
    }
    
    public async Task<Resultado<IReadOnlyList<GuiaResponseDto>>> ObtenerFiltroAsync(GuiaFiltroDto request)
    {
        var guias = ( await unit.Guias.ObtenerFiltroAsync(request, contextoUsuario.EmpresaId, false) )
            .Select( GuiaResponseDto.FromEntity ).ToList();
        return Resultado<IReadOnlyList<GuiaResponseDto>>.Exitoso(guias);
    }

    public async Task<Resultado<GuiaCreadaDto>> AgregarAsync(GuiaCreateDto dto)
    {
        try
        {
            var direccionOrigen =
                await unit.Clientes.ObtenerDireccionPorIdAsync(dto.ClienteOrigenId, dto.DireccionOrigenId, false);
            var direccionDestino =
                await unit.Clientes.ObtenerDireccionPorIdAsync(dto.ClienteDestinoId, dto.DireccionDestinoId, false);

            if (direccionOrigen is null || direccionDestino is null)
                return Resultado<GuiaCreadaDto>.Error(CodigosError.Generic.NoEncontrado);

            var direccionOrigenSnap = DireccionGuiaSnapshot.Crear(direccionOrigen.Direccion);
            var direccionDestinoSnap = DireccionGuiaSnapshot.Crear(direccionDestino.Direccion);

            var clave = await GenerarSiguienteFolioAsync(dto.SucursalOrigenId);

            var guia = Guia.Crear
            (
                clave,
                dto.FormaPago,
                DateTime.UtcNow,
                dto.ClienteOrigenId,
                direccionOrigenSnap,
                dto.ClienteDestinoId,
                direccionDestinoSnap,
                dto.SucursalOrigenId,
                dto.SucursalDestinoId,
                contextoUsuario.UsuarioId,
                dto.UsuarioCobroId,
                dto.Flete,
                dto.CobroSeguro,
                dto.Recoleccion,
                dto.EntregaA,
                dto.Maniobras,
                dto.Peaje,
                dto.Lineas,
                dto.Subtotal,
                dto.Iva,
                dto.IvaRetenido,
                dto.Total,
                dto.CondonaIva,
                dto.EstaAsegurado,
                dto.Observaciones,
                dto.PolizaSeguro,
                dto.SeguroId,
                contextoUsuario.EmpresaId
            );

            foreach (var articuloDto in dto.ArticulosGuia)
            {
                var articulo = ArticuloGuia.Crear(
                    articuloDto.ClaveProdServSat,
                    articuloDto.Descripcion,
                    articuloDto.Cantidad,
                    articuloDto.ClaveUnidadSat,
                    articuloDto.PesoUnitarioKg,
                    articuloDto.ClaveTipoEmbalajeSat,
                    articuloDto.ValorUnidad,
                    articuloDto.Largo,
                    articuloDto.Ancho,
                    articuloDto.Alto,
                    articuloDto.EsMaterialPeligroso,
                    articuloDto.ClaveMaterialPeligrosoSat
                );
                guia.AgregarArticulo(articulo);
            }

            await unit.Guias.AgregarAsync(guia);

            var filasAfectadas = await unit.CompletarAsync();

            if (filasAfectadas <= 0)
                return Resultado<GuiaCreadaDto>.Error(CodigosError.Generic.NoCreado);

            var guiaCreadaRespuesta = new GuiaCreadaDto( guia.Id, guia.Clave, guia.FechaCaptura );

            return Resultado<GuiaCreadaDto>.Exitoso(guiaCreadaRespuesta);
        }
        catch (Exception ex)
        {
            return Resultado<GuiaCreadaDto>.Error(new Error("", ex.Message));
        }
    }

    public async Task<Resultado> ActualizarAsync(GuiaUpdateDto dto)
    {
        var guia = await unit.Guias.ObtenerPorIdAsync(dto.GuiaId, contextoUsuario.EmpresaId);

        if (guia is null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);
        
        await unit.CompletarAsync();

        return Resultado.Exitoso();
    }

    public async Task<Resultado> EliminarAsync(Guid guiaId)
    {
        var guia = await unit.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId, false);

        if (guia is null)
            return Resultado.Error(CodigosError.Generic.NoEncontrado);

        unit.Guias.Eliminar(guia);
        await unit.CompletarAsync();

        return Resultado.Exitoso();
    }
    
    private async Task<string> GenerarSiguienteFolioAsync(Guid sucursalId)
    {
        var sucursal = await unit.Sucursales.ObtenerPorIdAsync(sucursalId, contextoUsuario.EmpresaId);
        if (sucursal == null) throw new Exception("La sucursal no existe.");
        var acronimo = sucursal.Codigo;

        var controlFolio = await unit.FoliosSucursal.ObtenerConBloqueoAsync(sucursalId);
        var siguienteNumero = 1;

        if (controlFolio == null)
        {
            controlFolio = FolioSucursal.Crear(sucursalId, 1);
            await unit.FoliosSucursal.AgregarAsync(controlFolio);
        }
        else
        {
            controlFolio.UltimoConsecutivo += 1;
            siguienteNumero = controlFolio.UltimoConsecutivo;
        }
        return $"{acronimo}-{siguienteNumero:D3}";
    }
}