using Paqueteria.Comun.Enums;

namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class Cliente
{
    public void ActualizarDatos(string nombre, string rfc, string telefono, string? telefono2, string correo, string contacto, string? numConvenio, string? polizaSeguro)
    {
        Nombre = nombre;
        Rfc = rfc;
        Telefono = telefono;
        Telefono2 = telefono2;
        Correo = correo;
        Contacto = contacto;
        NumConvenio = numConvenio;
        PolizaSeguro = polizaSeguro;
    }
    
    public void AgregarDireccion(DireccionCliente direccionCliente)
    {
        Direcciones.Add(direccionCliente);
    }
    public void Activar()
    {
        if (Estatus.Equals(EstatusBasico.Activo)) 
            throw new InvalidOperationException("El cliente ya está activo.");

        Estatus = EstatusBasico.Activo;
    }
    public void Desactivar()
    {
        if (Estatus.Equals(EstatusBasico.Inactivo)) 
            throw new InvalidOperationException("El cliente ya está de baja.");

        Estatus = EstatusBasico.Inactivo;
    }
}