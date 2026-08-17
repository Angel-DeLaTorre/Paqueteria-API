using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities.Remisiones;

public partial class Cliente
{
    public void Desactivar()
    {
        if (Estatus.Equals(EstatusBasico.Inactivo)) 
            throw new InvalidOperationException("El cliente ya está de baja.");

        Estatus = EstatusBasico.Inactivo;
    }
    
    public void Activar()
    {
        if (Estatus.Equals(EstatusBasico.Activo)) 
            throw new InvalidOperationException("El cliente ya está activo.");

        Estatus = EstatusBasico.Activo;
    }
}