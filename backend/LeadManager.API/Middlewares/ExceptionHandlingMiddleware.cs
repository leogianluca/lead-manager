using LeadManager.Domain.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Text.Json;
using System.Threading.Tasks;

namespace LeadManager.API.Middlewares
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;
        private readonly IWebHostEnvironment _env;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger, IWebHostEnvironment env)
        {
            _next = next;
            _logger = logger;
            _env = env;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                var statusCode = HttpStatusCode.InternalServerError;
                var message = "Erro interno no servidor.";
                object? details = null;

                switch (ex)
                {
                    case NotFoundException notFound:
                        statusCode = HttpStatusCode.NotFound;
                        message = notFound.Message;
                        break;

                    case ValidationException validation:
                        statusCode = HttpStatusCode.BadRequest;
                        message = validation.Message;
                        break;

                    default:
                        _logger.LogError(ex, "Exceção não tratada.");
                        if (_env.IsDevelopment())
                        {
                            message = ex.Message;
                            details = ex.StackTrace;
                        }
                        break;
                }

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = (int)statusCode;

                var response = JsonSerializer.Serialize(new
                {
                    StatusCode = context.Response.StatusCode,
                    Message = message,
                    Details = details
                });

                await context.Response.WriteAsync(response);
            }
        }
    }

}
