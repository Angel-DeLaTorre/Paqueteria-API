namespace Paqueteria.Aplicacion.Interfaces.Services.Reportes;

public interface IGuiaDocumentoService
{
    public Task<byte[]> GenerarEtiquetaPaqueteAsync(Guid guiaId);
}