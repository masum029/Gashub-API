using Microsoft.AspNetCore.Mvc;
using Project.Application.Exceptions;
using System.Net;
using System.Text.Json;

namespace Projects.Api.Middlewares
{
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlingMiddleware> _logger;
   
        public GlobalExceptionHandlingMiddleware(ILogger<GlobalExceptionHandlingMiddleware> logger)
        {
            _logger = logger;
            
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                // if Using Loger Exception Handle
                _logger.LogError(ex, ex.Message);
                // if Using Custom Exception Handle
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            HttpStatusCode status;
            string message;
            string  title;
            switch (exception)
            {
                case BadRequestException badRequestException:
                    message = badRequestException.Message;
                    status = HttpStatusCode.BadRequest;
                    title = "Bad Request Exception Ocard .... ?";
                    break;
                case NotFoundException notFoundException:
                    message = notFoundException.Message;
                    status = HttpStatusCode.NotFound;
                    title = "Not Found Exception Ocard .... ?";
                    break;
                case UnauthorizedException unauthorizedException:
                    message = unauthorizedException.Message;
                    status = HttpStatusCode.Unauthorized;
                    title = "Bad Request Exception Ocard .... ?";
                    break;
                case ForbiddenAccessException forbiddenException:
                    message = forbiddenException.Message;
                    status = HttpStatusCode.Forbidden;
                    title = "Forbidden Access Exception .... ?";
                    break;
                case ValidationException conflictException:
                    message = conflictException.Message;
                    status = HttpStatusCode.Conflict;
                    title = "Validation Exception Ocard .... ?";
                    break;
                case Timeout_Exceptio timeoutException:
                    message = timeoutException.Message;
                    status = HttpStatusCode.RequestTimeout;
                    title = "Timeout Exception Ocard .... ?";
                    break;
                // Add other exception types as needed
                default:
                    status = HttpStatusCode.InternalServerError;
                    title = "Internal Server Error Ocard .... ?";
                    message = "An error occurred while processing your request.";
                    break;
            }

            var problemDetails = new ProblemDetails
            {
                Status = (int)status,
                Title = title,
                Detail = message,
                Type = exception.GetType().Name,
                
            };

            var result = JsonSerializer.Serialize(problemDetails);
            context.Response.ContentType = "application/problem+json";
   
            return context.Response.WriteAsync(result);
        }

    }
}
