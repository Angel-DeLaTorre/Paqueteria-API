using Paqueteria.Dominio.Entidades.Sistema;

namespace Paqueteria.Dominio.Comun;

public static class EntidadesCatalogo
{
    private static readonly Dictionary<Type, string> Nombres = new()
    {
        { typeof(Rol), "el Rol" },
        { typeof(Permiso), "el Permiso" },
    };

    public static string ObtenerNombre<T>()
    {
        return Nombres.TryGetValue(typeof(T), out var nombre) 
            ? nombre 
            : typeof(T).Name.ToLower();
    }
}