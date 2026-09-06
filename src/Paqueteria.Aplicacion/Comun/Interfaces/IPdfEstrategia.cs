namespace Paqueteria.Application.Comun.Interfaces;

public interface IPdfEstrategia
{
    string TipoDocumento { get; }
    Task<byte[]> GenerarPdfAsync<TDatos>(TDatos datos);
}