using Paqueteria.Aplicacion.Comun.Interfaces;

namespace Paqueteria.Infrastructure.Reportes;

public class FabricaPdf(IEnumerable<IPdfEstrategia> estrategias) : IFabricaPdf
{
    public IPdfEstrategia SeleccionarEstrategia(string tipoDocumento)
    {
        var estrategia = estrategias.FirstOrDefault(
            e => e.TipoDocumento.Equals(tipoDocumento, StringComparison.OrdinalIgnoreCase));
        
        return estrategia ?? throw new InvalidOperationException($"No se encontró una estrategia de PDF configurada para el tipo de documento: '{tipoDocumento}'.");
    }
}