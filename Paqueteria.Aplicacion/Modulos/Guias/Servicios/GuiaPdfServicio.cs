using Paqueteria.Application.Comun.Interfaces;
using Paqueteria.Application.Interfaces.Persistence;
using Paqueteria.Application.Modulos.Guias.Interfaces;
using Paqueteria.Application.Modulos.Guias.Mapeador;
using Paqueteria.Application.Modulos.Reportes.Constantes;
using Paqueteria.Core.Common;
using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Application.Modulos.Guias.Servicios;

public class GuiaPdfServicio(
    IUnitOfWork unidadDeTrabajo, 
    IUsuarioContextoServicio contextoUsuario, 
    IFabricaPdf fabricaPdf
    ) 
    : IGuiaPdfServicio
{
public async Task<Resultado<byte[]>> GenerarEtiquetaPaqueteAsync(Guid guiaId)
{
    // 1. Obtener la entidad con sus relaciones desde la persistencia
    var guia = await unidadDeTrabajo.Guias.ObtenerPorIdAsync(guiaId, contextoUsuario.EmpresaId, asTracking: false);

    if (guia == null)
        return Resultado<byte[]>.Error(CodigosError.Generic.NoCreado);

    // 2. Proyectar la entidad al DTO necesario para el reporte
    var dto = guia.AEtiquetaDto();

    // 3. Seleccionar la estrategia de PDF mediante la fábrica
    var estrategia = fabricaPdf.SeleccionarEstrategia(TipoDocumentoPdf.EtiquetaGuia);

    // 4. Generar el arreglo de bytes
    var pdfBytes = await estrategia.GenerarPdfAsync(dto);

    return Resultado<byte[]>.Exitoso(pdfBytes);
}
}