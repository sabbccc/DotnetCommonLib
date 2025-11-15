using Common.Core.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text.Encodings.Web;

namespace Common.Core.Services.Auth
{
    public class ApiKeyAuthenticationHandler : AuthenticationHandler<ApiKeyAuthenticationOptions>
    {
        private readonly IAuthService _authService;
        private readonly ApiKeySettings _settings;

        public ApiKeyAuthenticationHandler(
            IOptionsMonitor<ApiKeyAuthenticationOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            IAuthService authService,
            IOptions<ApiKeySettings> settings)
            : base(options, logger, encoder) // Use default SystemClock
        {
            _authService = authService;
            _settings = settings.Value;
        }

        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.TryGetValue(_settings.HeaderName, out var apiKeyHeaderValues))
            {
                return Task.FromResult(AuthenticateResult.NoResult());
            }

            var providedKey = apiKeyHeaderValues.FirstOrDefault();

            var principal = _authService.ValidateToken(providedKey);

            if (principal == null)
                return Task.FromResult(AuthenticateResult.Fail("Invalid API Key"));

            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}
