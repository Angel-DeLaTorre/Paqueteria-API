using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Entities.Sistema;

public partial class Usuario
{
    public void Actualizar(string nombre)
    {
        Nombre = nombre;
    }
    public void RegistrarAcceso()
    {
        FechaUltimoAcceso = DateTime.UtcNow;
    }
    
    public void Desactivar()
    {
        if (Estatus.Equals(EstatusBasico.Inactivo)) 
            throw new InvalidOperationException("Usuario ya está inactivo");

        Estatus = EstatusBasico.Inactivo;
    }
    
    public void Activar()
    {
        if (Estatus.Equals(EstatusBasico.Activo)) 
            throw new InvalidOperationException("Usuario ya está activo.");

        Estatus = EstatusBasico.Activo;
    }
}