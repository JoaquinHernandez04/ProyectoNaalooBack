using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading.Tasks;

public class MiddlewareTime
{
    private readonly RequestDelegate _next;

    public MiddlewareTime(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Iniciar el temporizador
        var stopwatch = Stopwatch.StartNew();

        // Llamar al siguiente middleware en la cadena
        await _next(context);

        // Detener el temporizador
        stopwatch.Stop();

        // Loguear el tiempo de procesamiento
        var processingTime = stopwatch.ElapsedMilliseconds;
        Console.WriteLine($"Request processed in {processingTime} ms");
    }
}