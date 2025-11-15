using Microsoft.AspNetCore.Http;

namespace Common.Core.Middleware
{
    public static class HttpContextExtensions
    {
        private const string CorrelationHeader = "X-Correlation-ID";

        public static string GetCorrelationId(this HttpContext context)
        {
            if (context.Items.TryGetValue(CorrelationHeader, out var value))
                return value?.ToString() ?? string.Empty;

            return string.Empty;
        }
    }
}
