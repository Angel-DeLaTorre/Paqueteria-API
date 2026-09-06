using Paqueteria.Dominio.Comun.Errors;

namespace Paqueteria.Dominio.Comun;

public class Respuesta
{
    public bool EsExitoso { get; }
    public object? Datos { get; }
    public Error? DetalleError { get; }
    
    private Respuesta(bool isSuccess, Error? detalleError)
    {
        EsExitoso = isSuccess;
        Datos = null;
        DetalleError = detalleError;
    }

    public static Respuesta Exitoso() => new(true, null);
    public static Respuesta Error(Error detalleError) => new(false, detalleError);
}

public class Respuesta<T>
{
    public bool EsExitoso { get; }
    public T? Datos { get; }
    public Error? DetalleError { get; }
    
    private Respuesta(bool isSuccess, T? value, Error? detalleError)
    {
        EsExitoso = isSuccess;
        Datos = value;
        DetalleError = detalleError;
    }

    public static Respuesta<T> Exitoso(T? value) => new(true, value, null);
    public static Respuesta<T> Error(Error detalleError) => new(false, default, detalleError);
    public static Respuesta<T> Error(T value, Error detalleError) => new(false, value, detalleError);
}