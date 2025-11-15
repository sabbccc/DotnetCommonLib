using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace Common.Core.Middleware
{
    public class CorrelationIdMiddleware
    {
        private readonly RequestDelegate _next;
        private const string HeaderKey = "X-Correlation-ID";

        public CorrelationIdMiddleware(RequestDelegate next)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Ensure Correlation ID exists
            if (!context.Request.Headers.ContainsKey(HeaderKey) || string.IsNullOrWhiteSpace(context.Request.Headers[HeaderKey]))
            {
                context.Request.Headers[HeaderKey] = Guid.NewGuid().ToString();
            }

            // Add Correlation ID to response headers
            context.Response.OnStarting(() =>
            {
                context.Response.Headers[HeaderKey] = context.Request.Headers[HeaderKey];
                return Task.CompletedTask;
            });

            await _next(context);
        }
    }
}
