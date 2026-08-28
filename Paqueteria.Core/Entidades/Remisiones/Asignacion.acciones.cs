namespace Paqueteria.Core.Entidades.Remisiones;

public partial class Asignacion
{
    public void AgregarGuia(Guia guia)
    {
        ArgumentNullException.ThrowIfNull(guia);
        guia.AsignacionId = Id;
        Guias.Add(guia);
    }
}