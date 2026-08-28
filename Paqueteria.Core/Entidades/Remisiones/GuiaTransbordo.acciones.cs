namespace Paqueteria.Core.Entidades.Remisiones;

public partial class GuiaTransbordo
{
    public void RegistrarSalida()
    {
        FechaEscaneoSalida = DateTime.UtcNow;
    }
}