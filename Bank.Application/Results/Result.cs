using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank.Application.Results
{
    public abstract record Result
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public int StatusCodoe { get; set; }

        protected Result(bool isSuccess, string? message, int statusCode)
        {
            IsSuccess = isSuccess;
            Message = message;
            StatusCodoe = statusCode;               
        }

        public static Result Success(int statusCode = 200) => new SuccessResult(statusCode);

        private sealed record SuccessResult(int StatusCode) : Result(true, null, StatusCode);
    }
}
