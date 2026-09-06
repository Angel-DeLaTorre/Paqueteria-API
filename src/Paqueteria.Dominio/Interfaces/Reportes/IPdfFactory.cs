namespace Paqueteria.Dominio.Interfaces.Reportes;

public interface IPdfFactory
{
    IGeneradorPdfStrategy SeleccionarPdf(string tipoDocumento);
}