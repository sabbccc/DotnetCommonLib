using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Common.Core.Middleware
{
    public class RequestResponseLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestResponseLoggingMiddleware> _logger;

        public RequestResponseLoggingMiddleware(RequestDelegate next, ILogger<RequestResponseLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Log request
            context.Request.EnableBuffering();
            string requestBody = string.Empty;

            if (context.Request.ContentLength > 0 && context.Request.ContentLength < 1024 * 1024) // 1 MB limit
            {
                using var reader = new StreamReader(context.Request.Body, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: true);
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;
            }

            _logger.LogInformation("HTTP Request {Method} {Path} Body: {Body}",
                context.Request.Method, context.Request.Path, requestBody);

            // Log response
            var originalBody = context.Response.Body;
            await using var responseBodyStream = new MemoryStream();
            context.Response.Body = responseBodyStream;

            try
            {
                await _next(context);
            }
            finally
            {
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                string responseBody = await new StreamReader(responseBodyStream).ReadToEndAsync();
                responseBodyStream.Seek(0, SeekOrigin.Begin);
                await responseBodyStream.CopyToAsync(originalBody);
                context.Response.Body = originalBody;

                _logger.LogInformation("HTTP Response {StatusCode} Body: {Body}", context.Response.StatusCode, responseBody);
            }
        }
    }
}
