using Paqueteria.Comun.Enums;

namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class Ruta
{
    public void ActualizarDatos(string? descripcion, Guid sucursalOrigenId, Guid sucursalDestinoId)
    {
        Descripcion = descripcion;
        SucursalOrigenId = sucursalOrigenId;
        SucursalDestinoId = sucursalDestinoId;
    }
    
    public void Desactivar()
    {
        if (Estatus.Equals(EstatusBasico.Inactivo)) 
            throw new InvalidOperationException("Ruta ya está de baja.");

        Estatus = EstatusBasico.Inactivo;
    }
    
    public void Activar()
    {
        if (Estatus.Equals(EstatusBasico.Activo)) 
            throw new InvalidOperationException("Ruta ya está activo.");

        Estatus = EstatusBasico.Activo;
    }
}