using Paqueteria.Core.Enums;

namespace Paqueteria.Application.Modulos.Folios;

public interface IFolioServicio
{
    Task<string> GenerarSiguienteFolioAsync(Guid sucursalId, TipoFolio tipo);
}