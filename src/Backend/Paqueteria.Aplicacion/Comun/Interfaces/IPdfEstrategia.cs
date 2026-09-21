namespace Paqueteria.Aplicacion.Comun.Interfaces;

public interface IPdfEstrategia
{
    string TipoDocumento { get; }
    Task<byte[]> GenerarPdfAsync<TDatos>(TDatos datos);
}