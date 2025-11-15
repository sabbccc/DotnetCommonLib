using System.Security.Claims;
using Microsoft.Extensions.Options;
using Common.Core.Configuration;

namespace Common.Core.Services.Auth
{
    public class ApiKeyAuthService : IAuthService
    {
        private readonly ApiKeySettings _settings;

        public ApiKeyAuthService(IOptions<ApiKeySettings> options)
        {
            _settings = options.Value;
        }

        public string GenerateToken(IEnumerable<Claim> claims)
        {
            // API key authentication doesn't generate tokens
            throw new NotSupportedException("API key authentication does not generate tokens.");
        }

        public ClaimsPrincipal? ValidateToken(string token)
        {
            if (string.IsNullOrWhiteSpace(token) || !_settings.ValidKeys.Contains(token))
                return null;

            var identity = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.NameIdentifier, "apikey-user"),
                new Claim(ClaimTypes.Name, "API Key User")
            }, "ApiKey");

            return new ClaimsPrincipal(identity);
        }
    }
}
