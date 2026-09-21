namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class Asignacion
{
    public void AgregarGuia(Guia guia)
    {
        ArgumentNullException.ThrowIfNull(guia);
        guia.AsignacionId = Id;
        Guias.Add(guia);
    }

    public void ActualizarDatos( DateTime? fechaPartida, Guid sucursalOrigenId, Guid sucursalDestinoId )
    {
        FechaPartida = fechaPartida;
        SucursalOrigenId = sucursalOrigenId;
        SucursalDestinoId = sucursalDestinoId;
    }
}