namespace Paqueteria.Core.Interfaces.Reportes;

public interface IPdfFactory
{
    IGeneradorPdfStrategy SeleccionarPdf(string tipoDocumento);
}