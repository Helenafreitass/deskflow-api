using System.Text.Json;
using DeskFlow.API.Exceptions;

namespace DeskFlow.API.Middlewares;

public class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
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
        catch (Exception ex)
        {
            await TratarExcecaoAsync(context, ex);
        }
    }

    private async Task TratarExcecaoAsync(HttpContext context, Exception ex)
    {
        var (status, mensagem) = ex switch
        {
            NotFoundException => (StatusCodes.Status404NotFound, ex.Message),
            BusinessException => (StatusCodes.Status400BadRequest, ex.Message),
            _ => (StatusCodes.Status500InternalServerError, "Ocorreu um erro interno. Tente novamente mais tarde.")
        };

        if (status == StatusCodes.Status500InternalServerError)
            _logger.LogError(ex, "Erro não tratado");

        context.Response.StatusCode = status;
        context.Response.ContentType = "application/json";

        var corpo = new { status, erro = mensagem, data = DateTime.UtcNow };
        await context.Response.WriteAsync(JsonSerializer.Serialize(corpo));
    }
}