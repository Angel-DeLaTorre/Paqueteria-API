using Paqueteria.Core.Enums;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Core.Entidades.Remisiones;

public partial class DireccionCliente
{
    public void Desactivar()
    {
        if (Estatus.Equals(EstatusBasico.Inactivo)) 
            throw new InvalidOperationException("la direccion ya está de baja.");

        Estatus = EstatusBasico.Inactivo;
    }
    
    public void Activar()
    {
        if (Estatus.Equals(EstatusBasico.Activo)) 
            throw new InvalidOperationException("La direccion ya está activo.");

        Estatus = EstatusBasico.Activo;
    }

    public void Actualizar(Direccion direccion)
    {
        Direccion = direccion;
    }
}