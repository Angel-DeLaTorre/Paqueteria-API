using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entidades.Remisiones;

public partial class Seguro
{
    public void Desactivar()
    {
        if (Estatus.Equals(EstatusBasico.Inactivo)) 
            throw new InvalidOperationException("Seguro ya está inactivo");

        Estatus = EstatusBasico.Inactivo;
    }
    
    public void Activar()
    {
        if (Estatus.Equals(EstatusBasico.Activo)) 
            throw new InvalidOperationException("Seguro ya está activo.");

        Estatus = EstatusBasico.Activo;
    }
}