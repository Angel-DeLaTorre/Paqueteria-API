using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entidades.Sistema;

public partial class Permiso
{
    public void Desactivar()
    {
        if (Estatus.Equals(EstatusBasico.Inactivo)) 
            throw new InvalidOperationException("Permiso ya está inactivo");

        Estatus = EstatusBasico.Inactivo;
    }
    
    public void Activar()
    {
        if (Estatus.Equals(EstatusBasico.Activo)) 
            throw new InvalidOperationException("Permiso ya está activo.");

        Estatus = EstatusBasico.Activo;
    }
}