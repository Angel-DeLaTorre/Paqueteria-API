using Paqueteria.Core.Common.Errors;

namespace Paqueteria.Core.Common;

public class Result
{
    public bool IsSuccess { get; }
    public object? Value { get; }
    public Error? DetalleError { get; }
    
    private Result(bool isSuccess, Error? detalleError)
    {
        IsSuccess = isSuccess;
        Value = null;
        DetalleError = detalleError;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(Error detalleError) => new(false, detalleError);
}

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public Error? DetalleError { get; }
    
    private Result(bool isSuccess, T? value, Error? detalleError)
    {
        IsSuccess = isSuccess;
        Value = value;
        DetalleError = detalleError;
    }

    public static Result<T> Success(T? value) => new(true, value, null);
    public static Result<T> Failure(Error detalleError) => new(false, default, detalleError);
    public static Result<T> Failure(T value, Error detalleError) => new(false, value, detalleError);
}