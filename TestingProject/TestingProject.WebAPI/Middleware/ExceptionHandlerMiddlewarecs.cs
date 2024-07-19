using System.Net;
using System.Text.Json;
using TestingProject.BLL.DTOs;

namespace TestingProject.WebAPI.Middleware
{
    public class ExceptionHandlerMiddlewarecs
    {
        private readonly ILogger<ExceptionHandlerMiddlewarecs> _logger;
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddlewarecs(RequestDelegate next, ILogger<ExceptionHandlerMiddlewarecs> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception _) when (_ is OperationCanceledException or TaskCanceledException)
            {
                _logger.LogInformation("Task canclled");
            }
            catch (Exception ex) 
            {
                await HandleExceptionAsync(context, ex.Message,HttpStatusCode.InternalServerError, ex.Message);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, string exceptionMsg, HttpStatusCode httpStatusCode, string message)
        {
            _logger.LogError(exceptionMsg);

            HttpResponse response = context.Response;
            response.ContentType = "application/json";
            response.StatusCode = (int) httpStatusCode;

            ErrorDTO errorDTO = new()
            {
                Message = message,
                StatusCode = (int)httpStatusCode
            };

            await response.WriteAsJsonAsync(JsonSerializer.Serialize(errorDTO));
        }
    }
}
