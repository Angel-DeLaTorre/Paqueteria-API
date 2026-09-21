namespace Paqueteria.Aplicacion.Comun.Interfaces;

public interface IFabricaPdf
{
    IPdfEstrategia SeleccionarEstrategia(string tipoDocumento);
}