using Common.Core.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Common.Core.Services.Auth
{
    public static class AuthServiceRegistrar
    {
        public static void RegisterAuth(IServiceCollection services, IConfiguration config)
        {
            var provider = config["Authentication:Provider"]?.ToLower() ?? "none";

            switch (provider)
            {
                case "jwt":
                    RegisterJwt(services, config);
                    break;

                case "oauth2":
                    RegisterOAuth2(services, config);
                    break;

                case "apikey":
                    RegisterApiKey(services, config);
                    break;

                case "none":
                default:
                    // No authentication
                    break;
            }
        }

        private static void RegisterJwt(IServiceCollection services, IConfiguration config)
        {
            var jwtSettings = config.GetSection("Authentication:Jwt").Get<JwtSettings>();
            services.Configure<JwtSettings>(config.GetSection("Authentication:Jwt"));
            services.AddSingleton<IAuthService, JwtAuthService>();

            var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // true in production
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = TimeSpan.Zero
                };
            });
        }

        private static void RegisterOAuth2(IServiceCollection services, IConfiguration config)
        {
            var oauth2Settings = config.GetSection("Authentication:OAuth2").Get<OAuth2Settings>();
            services.Configure<OAuth2Settings>(config.GetSection("Authentication:OAuth2"));
            services.AddSingleton<IAuthService, OAuth2AuthService>();

            services.AddAuthentication("OAuth2")
                .AddOAuth("OAuth2", options =>
                {
                    options.ClientId = oauth2Settings.ClientId;
                    options.ClientSecret = oauth2Settings.ClientSecret;
                    options.CallbackPath = oauth2Settings.CallbackPath ?? "/signin-oauth";

                    options.AuthorizationEndpoint = oauth2Settings.AuthorizationEndpoint;
                    options.TokenEndpoint = oauth2Settings.TokenEndpoint;
                    options.UserInformationEndpoint = oauth2Settings.UserInfoEndpoint;

                    options.SaveTokens = true;

                    options.ClaimActions.MapJsonKey("sub", "sub");
                    options.ClaimActions.MapJsonKey("name", "name");
                    options.ClaimActions.MapJsonKey("email", "email");

                    options.Events.OnCreatingTicket = async context =>
                    {
                        // Optional: fetch additional user info
                    };
                });
        }

        private static void RegisterApiKey(IServiceCollection services, IConfiguration config)
        {
            var apiKeySettings = config.GetSection("Authentication:ApiKey").Get<ApiKeySettings>();
            services.Configure<ApiKeySettings>(config.GetSection("Authentication:ApiKey"));
            services.AddSingleton<IAuthService, ApiKeyAuthService>();

            // Custom API key authentication scheme
            services.AddAuthentication("ApiKey")
                    .AddScheme<ApiKeyAuthenticationOptions, ApiKeyAuthenticationHandler>("ApiKey", null);
        }
    }

    public enum AuthProvider
    {
        None,
        Jwt,
        OAuth2,
        ApiKey
    }
}
