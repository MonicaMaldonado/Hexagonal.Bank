using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Results;

public abstract record Result
{
    public bool IsSuccess { get; init; }
    public string Message { get; init; }
    public int CodeError { get; init; }

    protected Result(bool isSuccess, string? message, int codeError)
    {
        IsSuccess = isSuccess;
        Message = message;
        CodeError = codeError;               
    }

    public static Result Success(string message = "Successfuly process",int codeError = 200) => new SuccessResult(message, codeError);

    private sealed record SuccessResult(string message, int CodeError) : Result(true, message, CodeError);

    public static Result Failure(string message, int CodeError = 500) => new FailureResult(message, CodeError);

    private sealed record FailureResult(string Message, int CodeError) : Result(false, Message, CodeError);
}

public sealed record Result<T> : Result
{
    public T? Data { get; init; }
    protected Result(bool isSuccess, T? data, string? message, int codeError) : base(isSuccess, message, codeError)
    {
        Data = data;
    }
    public static Result<T> Ok(string message, T data, int codeError = 200) => new Result<T>(true, data, message, codeError);

    public static Result<T> Fail(string message, int codeError = 500) => new Result<T>(false, default, message, codeError);
}
