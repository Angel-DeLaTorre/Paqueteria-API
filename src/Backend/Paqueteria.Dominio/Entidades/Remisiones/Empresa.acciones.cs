using Paqueteria.Dominio.ValueObjects;

namespace Paqueteria.Dominio.Entidades.Remisiones;

public partial class Empresa
{
    public void ActualizarDatos( string nombre, string? nombreCorto, string rfc, Direccion direccion)
    {
        Nombre =  nombre;
        NombreCorto = nombreCorto;
        Rfc = rfc;
        Direccion = direccion;
    }
}