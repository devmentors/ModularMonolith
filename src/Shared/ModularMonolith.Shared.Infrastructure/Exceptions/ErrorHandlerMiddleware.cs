using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using Humanizer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using ModularMonolith.Shared.Abstractions.Exceptions;

namespace ModularMonolith.Shared.Infrastructure.Exceptions
{
    internal class ErrorHandlerMiddleware : IMiddleware
    {
        private readonly ConcurrentDictionary<Type, string> _codes = new();
        private readonly ILogger<ErrorHandlerMiddleware> _logger;

        public ErrorHandlerMiddleware(ILogger<ErrorHandlerMiddleware> logger)
        {
            _logger = logger;
        }
        
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception exception)
            {
                var statusCode = 500;
                var code = "error";
                var message = "There was an error.";

                _logger.LogError(exception, exception.Message);
                if (exception is CustomException customException)
                {
                    statusCode = 400;
                    var exceptionType = customException.GetType();
                    if (!_codes.TryGetValue(exceptionType, out var errorCode))
                    {
                        code = customException.GetType().Name.Underscore().Replace("_exception", string.Empty);
                        _codes.TryAdd(exceptionType, code);
                    }
                    else
                    {
                        code = errorCode;
                    }

                    message = customException.Message;
                }

                context.Response.StatusCode = statusCode;
                await context.Response.WriteAsJsonAsync(new {code, message});
            }
        }
    }
}