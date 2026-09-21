using Paqueteria.Aplicacion.Comun;
using Paqueteria.Aplicacion.Comun.Interfaces;
using Paqueteria.Aplicacion.Interfaces.Persistence;
using Paqueteria.Aplicacion.Modulos.Guias.Interfaces;
using Paqueteria.Aplicacion.Modulos.Guias.Mapeador;
using Paqueteria.Aplicacion.Modulos.Reportes.Constantes;
using Paqueteria.Comun.Comun;
using Paqueteria.Comun.Comun.Errores;
using Paqueteria.Comun.Dtos.Guias;

namespace Paqueteria.Aplicacion.Modulos.Guias.Servicios;

public sealed class GuiaPdfServicio(
    IUnitOfWork unidad,
    IUsuarioContextoServicio contextoUsuario,
    IFabricaPdf fabricaPdf
)
    : IGuiaPdfServicio
{
    public async Task<Respuesta<byte[]>> GenerarEtiquetaPaqueteAsync(Guid guiaId)
    {
        var guia = await unidad.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId, false);

        if (guia == null)
            return Respuesta<byte[]>.Error(CodigosError.Comun.NoCreado);

        var dto = guia.AEtiquetaDto();
        var estrategia = fabricaPdf.SeleccionarEstrategia(TipoDocumentoPdf.EtiquetaGuia);
        var pdfBytes = await estrategia.GenerarPdfAsync<EtiquetaPaqueteRespuestaDto>(dto);

        return Respuesta<byte[]>.Exitoso(pdfBytes);
    }

    public async Task<Respuesta<byte[]>> GenerarRemisionPdfAsync(Guid guiaId)
    {
        try
        {
            var guia = await unidad.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId, false);

            if (guia == null)
                return Respuesta<byte[]>.Error(CodigosError.Comun.NoEncontrado);

            var dto = guia.ToRemisionPdfDto();
            var estrategia = fabricaPdf.SeleccionarEstrategia(TipoDocumentoPdf.RemisionGuia);
            var pdfBytes = await estrategia.GenerarPdfAsync<RemisionPdfDto>(dto);

            return Respuesta<byte[]>.Exitoso(pdfBytes);
        }
        catch (Exception ex)
        {
            return Respuesta<byte[]>.Error(CodigosError.Comun.NoEncontrado);
        }
    }
}