using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;

namespace ECommerce.Infrastructure.Security.Tokens.Jwt;

public class TokenValidator : IAccessTokenValidator
{
    private readonly string _secretKey;

    public TokenValidator(string secretKey)
    {
        _secretKey = secretKey;
    }

    public Guid ValidateAndGetUserId(string token)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));

        var validationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            IssuerSigningKey = securityKey,
            ClockSkew = new TimeSpan(0)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, validationParameters, out _ );

        var userId = principal.Claims.First(c => c.Type == ClaimTypes.Sid).Value;
        return Guid.Parse(userId);
    }
}