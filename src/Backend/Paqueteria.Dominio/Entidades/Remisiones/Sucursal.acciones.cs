using Paqueteria.Dominio.ValueObjects;

namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class Sucursal
{
    public void ActualizarDatos(string nombre, bool esMatriz, string telefono)
    {
        Nombre = nombre;
        EsMatriz = esMatriz;
        Telefono = telefono;
    }

    public void ActualizarDireccion(Direccion direccion)
    {
        Direccion = direccion;
    }

    public void AsignarIp(string ip)
    {
        ServidorIp = ip;
    }
}