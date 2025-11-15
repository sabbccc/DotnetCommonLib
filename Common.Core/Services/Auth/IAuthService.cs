using System.Security.Claims;

namespace Common.Core.Services.Auth
{
    public interface IAuthService
    {
        string GenerateToken(IEnumerable<Claim> claims);
        ClaimsPrincipal? ValidateToken(string token);
    }
}
