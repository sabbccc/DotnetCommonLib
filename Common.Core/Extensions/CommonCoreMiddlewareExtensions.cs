using Common.Core.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Common.Core.Extensions
{
    public static class CommonCoreMiddlewareExtensions
    {
        /// <summary>
        /// Registers the default Common.Core middleware pipeline:
        /// - ProblemDetails (exception handling)
        /// - Request/Response logging
        /// - Correlation ID (if implemented)
        /// Add more middleware as needed
        /// </summary>
        public static IApplicationBuilder UseCommonCoreMiddleware(this IApplicationBuilder app)
        {
            app.UseProblemDetails();
            app.UseRequestResponseLogging();
            app.UseCorrelationId(); // We'll implement this next (#10)

            // Optional: Add other default middlewares here
            // app.UseAuthentication();
            // app.UseAuthorization();

            return app;
        }
    }
}
