using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Security.Tokens;

public interface IAccessTokenGenerator
{
    string GenerateToken(User user);
}