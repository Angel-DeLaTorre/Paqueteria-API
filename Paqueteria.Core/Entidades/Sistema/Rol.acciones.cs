using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entidades.Sistema;

public partial class Rol
{
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