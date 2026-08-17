using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Core.Common;

public class Resultado
{
    public bool IsSuccess { get; }
    public object? Value { get; }
    public Error? DetalleError { get; }
    
    private Resultado(bool isSuccess, Error? detalleError)
    {
        IsSuccess = isSuccess;
        Value = null;
        DetalleError = detalleError;
    }

    public static Resultado Exitoso() => new(true, null);
    public static Resultado Error(Error detalleError) => new(false, detalleError);
}

public class Resultado<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error? DetalleError { get; }
    
    private Resultado(bool isSuccess, T? value, Error? detalleError)
    {
        IsSuccess = isSuccess;
        Value = value;
        DetalleError = detalleError;
    }

    public static Resultado<T> Exitoso(T? value) => new(true, value, null);
    public static Resultado<T> Error(Error detalleError) => new(false, default, detalleError);
    public static Resultado<T> Error(T value, Error detalleError) => new(false, value, detalleError);
}