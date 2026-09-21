using Paqueteria.Comun.Enums;

namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class Seguro
{
    public void ActualizarDatos(string nombre)
    {
        Nombre = nombre;
    }
    
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