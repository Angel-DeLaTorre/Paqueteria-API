using Paqueteria.Aplicacion.Comun;
using Paqueteria.Aplicacion.Comun.Interfaces;
using Paqueteria.Aplicacion.Interfaces.Persistence;
using Paqueteria.Aplicacion.Modulos.Folios;
using Paqueteria.Aplicacion.Modulos.Guias.Interfaces;
using Paqueteria.Aplicacion.Modulos.Guias.Mapeador;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Comun.Errores;
using Paqueteria.Comun.Dtos.Guias;
using Paqueteria.Comun.Enums;
using Paqueteria.Dominio.Dto;
using Paqueteria.Dominio.Entidades.Remisiones;

namespace Paqueteria.Aplicacion.Modulos.Guias.Servicios;

public sealed class GuiaServicio(
    IUnitOfWork unit,
    IUsuarioContextoServicio contextoUsuario,
    IFolioServicio folioServicio
) : IGuiaServicio
{
    public async Task<Respuesta<IReadOnlyList<GuiaRespuestaDto>>> ObtenerTodosAsync()
    {
        try
        {
            var guias = await unit.Guias.ObtenerTodosAsync(contextoUsuario.EmpresaId, false);
            var guiasRespuesta = guias.Select<Guia, GuiaRespuestaDto>(g => g.MapeaRespuestaDto()).ToList();
            return Respuesta<IReadOnlyList<GuiaRespuestaDto>>.Exitoso(guiasRespuesta);
        }
        catch (Exception ex)
        {
            return Respuesta<IReadOnlyList<GuiaRespuestaDto>>.Error(new Paqueteria.Comun.Comun.Errores.Error("", ex.Message));
        }
    }

    public async Task<Respuesta<GuiaRespuestaDto>> ObtenerPorIdAsync(Guid guiaId)
    {
        var guia = await unit.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId, false);

        if (guia is null)
            return Respuesta<GuiaRespuestaDto>.Error(CodigosError.Comun.NoEncontrado);

        return Respuesta<GuiaRespuestaDto>.Exitoso(guia.MapeaRespuestaDto());
    }

    public async Task<Respuesta<IReadOnlyList<GuiaRespuestaDto>>> ObtenerFiltroAsync(GuiaFiltroDto request)
    {
        var guias = (await unit.Guias.ObtenerFiltroAsync(request, contextoUsuario.EmpresaId, false))
            .Select<Guia, GuiaRespuestaDto>(g => g.MapeaRespuestaDto()).ToList();
        return Respuesta<IReadOnlyList<GuiaRespuestaDto>>.Exitoso(guias);
    }

    public async Task<Respuesta<GuiaCreadaDto>> AgregarAsync(GuiaCrearDto dto)
    {
        try
        {
            var direccionOrigen =
                await unit.Clientes.ObtenerDireccionPorIdAsync(dto.ClienteOrigenId, dto.DireccionOrigenId, false);
            var direccionDestino =
                await unit.Clientes.ObtenerDireccionPorIdAsync(dto.ClienteDestinoId, dto.DireccionDestinoId, false);

            if (direccionOrigen is null || direccionDestino is null)
                return Respuesta<GuiaCreadaDto>.Error(CodigosError.Comun.NoEncontrado);

            var direccionOrigenSnap = DireccionGuiaSnapshot.Crear(direccionOrigen.Direccion);
            var direccionDestinoSnap = DireccionGuiaSnapshot.Crear(direccionDestino.Direccion);

            var clave = await folioServicio.GenerarSiguienteFolioAsync(dto.SucursalOrigenId, TipoFolio.Guia);

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

            var filasAfectadas = await unit.GuardarCambiosAsync();

            if (filasAfectadas <= 0)
                return Respuesta<GuiaCreadaDto>.Error(CodigosError.Comun.NoCreado);

            var guiaCreadaRespuesta = new GuiaCreadaDto(guia.Id, guia.Clave, guia.FechaCaptura);

            return Respuesta<GuiaCreadaDto>.Exitoso(guiaCreadaRespuesta);
        }
        catch (Exception ex)
        {
            return Respuesta<GuiaCreadaDto>.Error(new Paqueteria.Comun.Comun.Errores.Error("", ex.Message));
        }
    }

    public async Task<Respuesta> ActualizarAsync(GuiaActualizarDto dto)
    {
        var guia = await unit.Guias.ObtenerPorIdAsync(dto.GuiaId, contextoUsuario.EmpresaId);

        if (guia is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> EliminarAsync(Guid guiaId)
    {
        var guia = await unit.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId, false);

        if (guia is null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        unit.Guias.Eliminar(guia);
        await unit.GuardarCambiosAsync();

        return Respuesta.Exitoso();
    }

    public async Task<Respuesta> RegistrarEntregaClienteAsync(Guid guiaId)
    {
        var guia = await unit.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId);
        if (guia == null)
            return Respuesta.Error(CodigosError.Comun.NoEncontrado);

        var ultimoTransbordo = await unit.GuiaTransbordos.ObtenerUltimoTransbordoActivoAsync(guiaId);
        ultimoTransbordo?.RegistrarSalida();

        guia.RegistrarEntrega();

        await unit.GuardarCambiosAsync();
        return Respuesta.Exitoso();
    }
}