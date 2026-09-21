using System.Text.Json.Serialization;
using Paqueteria.Comun.Comun.Errores;

namespace Paqueteria.Comun.Comun;

public class Respuesta
{
    [JsonConstructor]
    public Respuesta(bool esExitoso, object? datos, Error? detalleError)
    {
        EsExitoso = esExitoso;
        Datos = datos;
        DetalleError = detalleError;
    }

    private Respuesta(bool esExitoso, Error? detalleError)
    {
        EsExitoso = esExitoso;
        Datos = null;
        DetalleError = detalleError;
    }

    public bool EsExitoso { get; init; }
    public object? Datos { get; init; }
    public Error? DetalleError { get; init; }

    public static Respuesta Exitoso()
    {
        return new Respuesta(true, null);
    }

    public static Respuesta Error(Error detalleError)
    {
        return new Respuesta(false, detalleError);
    }
    
    public static Respuesta Error(string mensaje)
    {
        return new Respuesta(false, null, new Error("", mensaje));
    }
}

[method: JsonConstructor]
public class Respuesta<T>(bool esExitoso, T? datos, Error? detalleError)
{
    public bool EsExitoso { get; } = esExitoso;
    public T? Datos { get; } = datos;
    public Error? DetalleError { get; } = detalleError;

    public static Respuesta<T> Exitoso(T? datos)
    {
        return new Respuesta<T>(true, datos, null);
    }

    public static Respuesta<T> Error(Error detalleError)
    {
        return new Respuesta<T>(false, default, detalleError);
    }

    public static Respuesta<T> Error(T value, Error detalleError)
    {
        return new Respuesta<T>(false, value, detalleError);
    }

    public static Respuesta<T> Error(string mensaje)
    {
        return new Respuesta<T>(false, default, new Error("", mensaje));
    }
}