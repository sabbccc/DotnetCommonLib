using Common.Core.Configuration;
using Common.Core.Jobs;
using Common.Core.Middleware;
using Hangfire;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace Common.Core.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseCommonCore(
            this IApplicationBuilder app,
            IHostEnvironment env)
        {
            // Get module settings from DI
            var modules = app.ApplicationServices
                             .GetRequiredService<IOptions<ModuleSettings>>()
                             .Value;

            // 1️ Centralized Exception Handling / ProblemDetails
            if (modules.EnableProblemDetails)
                app.UseProblemDetails();

            // 2️ Correlation ID / Trace Context
            if (modules.EnableCorrelationId)
                app.UseCorrelationId();

            // 3️ Request / Response Logging
            if (modules.EnableRequestResponseLogging)
                app.UseRequestResponseLogging();

            // 4️ HTTPS Redirection
            app.UseHttpsRedirection();

            // 5️ Routing
            app.UseRouting();

            // 6️ Authentication / Authorization
            if (modules.EnableAuth)
            {
                app.UseAuthentication();
                app.UseAuthorization();
            }

            // 7️ Hangfire Dashboard
            if (modules.BackgroundJobs)
            {
                app.UseHangfireDashboard("/hangfire", new Hangfire.DashboardOptions
                {
                    Authorization = new[] { new HangfireAuthorizationFilter() }
                });
            }

            // 8️ Swagger UI
            if (env.IsDevelopment() || modules.EnableSwagger)
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                    c.RoutePrefix = string.Empty;
                });
            }

            return app;
        }
    }
}
