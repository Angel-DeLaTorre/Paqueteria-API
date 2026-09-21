using Paqueteria.Aplicacion.Comun;
using Paqueteria.Comun.Comun;

namespace Paqueteria.Aplicacion.Modulos.Guias.Interfaces;

public interface IGuiaPdfServicio
{
    public Task<Respuesta<byte[]>> GenerarEtiquetaPaqueteAsync(Guid guiaId);
    public Task<Respuesta<byte[]>> GenerarRemisionPdfAsync(Guid guiaId);
}