using Common.Core.Configuration;
using Common.Core.Database;
using Common.Core.Jobs;
using Common.Core.Services;
using Common.Core.Services.Auth;
using Common.Core.Services.Email;
using Common.Core.Services.Http;
using Common.Core.Services.Sms;
using Common.Core.Audit;
using Common.Core.Storage;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Core.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommonCore(
            this IServiceCollection services,
            IConfiguration config)
        {
            // 1️ Database
            var db = config.GetSection("Database").Get<DatabaseSettings>();
            services.AddDatabaseModule(db);

            // 2️ Modules
            var modules = config.GetSection("Modules").Get<ModuleSettings>() ?? new ModuleSettings();
            services.Configure<ModuleSettings>(config.GetSection("Modules"));
            ModuleRegistrar.RegisterModules(services, config);

            // 3️ Email
            if (modules.EnableEmail)
            {
                var email = config.GetSection("EmailSettings").Get<EmailSettings>();
                services.Configure<EmailSettings>(config.GetSection("EmailSettings"));
                services.AddTransient<IEmailService, MailKitEmailService>();
            }

            // 4️ SMS
            if (modules.EnableSms)
            {
                var sms = config.GetSection("SmsSettings").Get<SmsSettings>();
                services.Configure<SmsSettings>(config.GetSection("SmsSettings"));
                services.AddTransient<ISmsService, TwilioSmsService>();
            }

            // 5️ Authentication
            if (modules.EnableAuth)
            {
                var jwt = config.GetSection("Authentication:Jwt").Get<JwtSettings>();
                if (config["Authentication:Provider"] == "Jwt")
                {
                    services.Configure<JwtSettings>(config.GetSection("Authentication:Jwt"));
                    AuthServiceRegistrar.RegisterAuth(services, config);
                }
                // OAuth2 or None can be added here later
            }

            // 6️ HTTP Service
            if (modules.EnableHttp)
            {
                services.AddHttpClient<IHttpService, HttpService>();
            }

            // 7️ Audit Logging
            if (modules.EnableAudit)
                services.AddAuditLogging();

            // 8️ File Storage
            if (modules.EnableFileStorage)
                services.AddLocalFileStorage("uploads"); // configurable folder

            return services;
        }
    }
}
