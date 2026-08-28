using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entidades.Remisiones;

public class FolioSucursal
{
    public Guid SucursalId { get; private set; }
    public TipoFolio Tipo { get; private set; }
    public int UltimoConsecutivo { get; set; }
    
    public Sucursal Sucursal { get; private set; } = null!;

    private  FolioSucursal() { }

    public static FolioSucursal Crear(Guid sucursalId, TipoFolio tipo, int ultimoConsecutivo)
    {
        return new FolioSucursal()
        {
            SucursalId = sucursalId,
            Tipo = tipo,
            UltimoConsecutivo = ultimoConsecutivo
        };
    }
}