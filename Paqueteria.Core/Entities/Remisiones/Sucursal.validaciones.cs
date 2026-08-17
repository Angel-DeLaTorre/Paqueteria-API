using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities.Remisiones;

public partial class Sucursal
{
    public void Desactivar()
    {
        if (Estatus.Equals(EstatusBasico.Inactivo)) 
            throw new InvalidOperationException("Sucursal ya está inactivo");

        Estatus = EstatusBasico.Inactivo;
    }
    
    public void Activar()
    {
        if (Estatus.Equals(EstatusBasico.Activo)) 
            throw new InvalidOperationException("Sucursal ya está activo.");

        Estatus = EstatusBasico.Activo;
    }
}