using Paqueteria.Dominio.Comun;

namespace Paqueteria.Application.Modulos.Guias.Interfaces;

public interface IGuiaPdfServicio
{
    public Task<Respuesta<byte[]>> GenerarEtiquetaPaqueteAsync(Guid guiaId);
    public Task<Respuesta<byte[]>> GenerarRemisionPdfAsync(Guid guiaId);
}