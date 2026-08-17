using Paqueteria.Core.Common;

namespace Paqueteria.Application.Modulos.Guias.Interfaces;

public interface IGuiaPdfServicio
{
    public Task<Resultado<byte[]>> GenerarEtiquetaPaqueteAsync(Guid guiaId);
}