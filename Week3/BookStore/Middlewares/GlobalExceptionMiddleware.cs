using System.Net;

// GlobalExceptionMiddleware: Uygulamada ki istisnaları yakalar ve  hata yanıtı döner.

// GlobalExceptionMiddleware: Catches exceptions throughout the application and returns error to the client.



namespace BookStore.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionMiddleware> _logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger<GlobalExceptionMiddleware> logger)
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
                _logger.LogError(ex, "An unhandled exception has occurred.");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
               
                // Hata mesajı - Erros messages
                await context.Response.WriteAsync("An internal server error occurred.");
            }
        }
    }
}
