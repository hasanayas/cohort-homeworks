
// RequestLoggingMiddleware: HTTP isteği ve yanıtını.

// RequestLoggingMiddleware: Logs the HTTP request and response.

namespace BookStore.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            _logger.LogInformation($"Request: {context.Request.Method} {context.Request.Path} at {DateTime.Now}");
            await _next(context);
            _logger.LogInformation($"Response: {context.Response.StatusCode} at {DateTime.Now}");
        }
    }
}
