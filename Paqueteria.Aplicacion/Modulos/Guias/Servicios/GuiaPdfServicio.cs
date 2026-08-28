using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Guias.Interfaces;
using Paqueteria.Application.Modulos.Guias.Mapeador;
using Paqueteria.Application.Modulos.Reportes.Constantes;
using Paqueteria.Core.Comun;
using Paqueteria.Core.Comun.Errors;

namespace Paqueteria.Application.Modulos.Guias.Servicios;

public sealed class GuiaPdfServicio(
    IUnitOfWork unidad, 
    IUsuarioContextoServicio contextoUsuario, 
    IFabricaPdf fabricaPdf
    ) 
    : IGuiaPdfServicio
{
    public async Task<Respuesta<byte[]>> GenerarEtiquetaPaqueteAsync(Guid guiaId)
    {
        var guia = await unidad.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId, asTracking: false);

        if (guia == null)
            return Respuesta<byte[]>.Error(CodigosError.Generic.NoCreado);
        
        var dto = guia.AEtiquetaDto();
        var estrategia = fabricaPdf.SeleccionarEstrategia(TipoDocumentoPdf.EtiquetaGuia);
        var pdfBytes = await estrategia.GenerarPdfAsync(dto);

        return Respuesta<byte[]>.Exitoso(pdfBytes);
    }

    public async Task<Respuesta<byte[]>> GenerarRemisionPdfAsync(Guid guiaId)
    {
        try
        {
            var guia = await unidad.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId, asTracking: false);

            if (guia == null)
                return Respuesta<byte[]>.Error(CodigosError.Generic.NoEncontrado);

            var dto = guia.ToRemisionPdfDto();
            var estrategia = fabricaPdf.SeleccionarEstrategia(TipoDocumentoPdf.RemisionGuia);
            var pdfBytes = await estrategia.GenerarPdfAsync(dto);

            return Respuesta<byte[]>.Exitoso(pdfBytes);
        }
        catch (Exception ex)
        {
            return Respuesta<byte[]>.Error(CodigosError.Generic.NoEncontrado);
        }        

    }
}