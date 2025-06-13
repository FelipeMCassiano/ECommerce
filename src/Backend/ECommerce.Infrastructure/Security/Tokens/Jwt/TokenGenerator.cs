using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ECommerce.Domain.Entities;
using ECommerce.Domain.Security.Tokens;
using Microsoft.IdentityModel.Tokens;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace ECommerce.Infrastructure.Security.Tokens.Jwt;

public class TokenGenerator : IAccessTokenGenerator
{
    private readonly string _secretKey;
    private readonly uint _expirationInMinutes;
    public TokenGenerator(string secretKey, uint expirationInMinutes)
    {
        _secretKey = secretKey;
        _expirationInMinutes = expirationInMinutes;
    }
    public string GenerateToken(User user)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_secretKey));
        
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        var descriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(JwtRegisteredClaimNames.Sid, user.Id.ToString()),
            ]),
            Expires = DateTime.UtcNow.AddMinutes(_expirationInMinutes),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        
        var securityToken = tokenHandler.CreateToken(descriptor);
        
        return tokenHandler.WriteToken(securityToken);
    }

}