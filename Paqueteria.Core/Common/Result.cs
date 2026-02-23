using Paqueteria.Core.Enums;

namespace Paqueteria.Core.Common;

public class Result
{
    public bool IsSuccess { get; }
    public BaseError? Error { get; }

    private Result(bool isSuccess, BaseError? error)
    {
        IsSuccess = isSuccess;
        Error = error;
    }

    public static Result Success() => new(true, null);
    public static Result Failure(CodigoRespuesta code, string description) => new(false, new BaseError(code, description));
}

public class Result<T>
{
    public bool IsSuccess { get; }
    public T? Value { get; }
    public BaseError? Error { get; }

    private Result(bool isSuccess, T? value, BaseError? error)
    {
        IsSuccess = isSuccess;
        Value = value;
        Error = error;
    }

    public static Result<T> Success(T? value) => new(true, value, null);
    public static Result<T> Failure(CodigoRespuesta code, string description) => new(false, default, new BaseError(code, description));
}