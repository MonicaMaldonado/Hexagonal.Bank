using Bank.Application.Results;
using Microsoft.AspNetCore.Diagnostics;
using System.Security.Cryptography.X509Certificates;
using System.Text.Json;

namespace Bank.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next; //espera la comunicacion de un evento, el middleware es un consumidor de eventos 
        //el trabajo de este delegado es indicar si el proceso sigue o no

        private readonly ILogger<ExceptionHandlerMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate request, ILogger<ExceptionHandlerMiddleware> logger)
        {
            _next = request;
            _logger = logger;
        }

        public async Task InvokeAsyn(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Unhandle exception processing request");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = StatusCodes.Status500InternalServerError;
            var result = Result.Failure(exception.Message, statusCode);
            
            context.Response.StatusCode = statusCode;
            context.Response.ContentType = "application/json";

            var options = new JsonSerializerOptions //formato que va a contener la respuets
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                WriteIndented = true,
            };

            var payload = JsonSerializer.Serialize(result, options);  //serializacion del resultado
            await context.Response.WriteAsync(payload);
        }
    }
}
