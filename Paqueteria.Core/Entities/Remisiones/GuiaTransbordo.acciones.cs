namespace Paqueteria.Core.Entities.Remisiones;

public partial class GuiaTransbordo
{
    public void RegistrarSalida()
    {
        FechaEscaneoSalida = DateTime.UtcNow;
    }
}