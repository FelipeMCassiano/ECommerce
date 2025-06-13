using ECommerce.Domain.Services.Security.Password;

namespace ECommerce.Infrastructure.Services.Security;

public class BcryptEncoder : IPasswordEncoder
{
    public string Encode(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }
}