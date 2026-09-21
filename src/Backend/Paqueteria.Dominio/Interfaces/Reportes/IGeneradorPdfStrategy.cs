namespace Paqueteria.Dominio.Interfaces.Reportes;

public interface IGeneradorPdfStrategy
{
    string TipoDocumento { get; }
    
    Task<byte[]> GenerarPdfAsync<TData>(TData datos);
}