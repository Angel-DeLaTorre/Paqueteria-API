namespace Paqueteria.Core.Entities.Remisiones;

public class FolioSucursal
{
    public Guid SucursalId { get; private set; }
    public int UltimoConsecutivo { get; set; }
    
    public Sucursal Sucursal { get; private set; } = null!;

    private  FolioSucursal() { }

    public static FolioSucursal Crear(Guid sucursalId, int ultimoConsecutivo)
    {
        return new FolioSucursal()
        {
            SucursalId = sucursalId,
            UltimoConsecutivo = ultimoConsecutivo
        };
    }
}