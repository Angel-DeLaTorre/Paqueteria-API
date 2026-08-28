using Paqueteria.Core.Enums;
using Paqueteria.Core.ValueObjects;

namespace Paqueteria.Core.Entidades.Remisiones;

public partial class Chofer
{

    public void ActualizarDatosPersonales
    (
        string nombre, 
        string apellidoPaterno, 
        string apellidoMaterno, 
        string telefono,
        Direccion nuevaDireccion
    )
    {
        ValidarDatosPersonales(nombre, apellidoPaterno, apellidoMaterno);
        Nombre = nombre;
        ApellidoPaterno = apellidoPaterno;
        ApellidoMaterno = apellidoMaterno;
        Telefono = telefono;
        Direccion = nuevaDireccion;
    }
    
    public void AsignarCamion(string? numCamion, string? numContenedor, string? numContenedor2)
    {
        NumCamion = numCamion;
        NumContenedor = numContenedor;
        NumContenedor2 = numContenedor2;
    }
    
    public void Desactivar()
    {
        if (Estatus.Equals(EstatusBasico.Inactivo)) 
            throw new InvalidOperationException("El chofer ya está de baja.");

        Estatus = EstatusBasico.Inactivo;
        FechaBaja = DateTime.Now;
    }
    
    public void Activar()
    {
        if (Estatus.Equals(EstatusBasico.Activo)) 
            throw new InvalidOperationException("El chofer ya está activo.");

        Estatus = EstatusBasico.Activo;
    }
    
    
}