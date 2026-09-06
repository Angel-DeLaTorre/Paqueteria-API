namespace Paqueteria.Application.Comun.Interfaces;

public interface IFabricaPdf
{
    IPdfEstrategia SeleccionarEstrategia(string tipoDocumento);
}