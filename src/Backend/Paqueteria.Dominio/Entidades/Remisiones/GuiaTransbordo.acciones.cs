namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class GuiaTransbordo
{
    public void RegistrarSalida()
    {
        FechaEscaneoSalida = DateTime.UtcNow;
    }
}