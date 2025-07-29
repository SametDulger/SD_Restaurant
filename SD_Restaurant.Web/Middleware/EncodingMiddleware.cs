using System.Text;

namespace SD_Restaurant.Web.Middleware
{
    public class EncodingMiddleware
    {
        private readonly RequestDelegate _next;

        public EncodingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Set UTF-8 encoding for all responses
            context.Response.ContentType = "text/html; charset=utf-8";
            
            // Set request encoding
            context.Request.EnableBuffering();
            
            await _next(context);
        }
    }

    public static class EncodingMiddlewareExtensions
    {
        public static IApplicationBuilder UseEncodingMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<EncodingMiddleware>();
        }
    }
} 