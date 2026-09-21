using Paqueteria.Comun.Enums;

namespace Paqueteria.Aplicacion.Modulos.Folios;

public interface IFolioServicio
{
    Task<string> GenerarSiguienteFolioAsync(Guid sucursalId, TipoFolio tipo);
}