using ECommerce.Domain.Security.Cryptography;

namespace ECommerce.Infrastructure.Security.Cryptography;

public class BcryptEncoder : IPasswordEncoder
{
    public string Encode(string password)
    {
        return BCrypt.Net.BCrypt.HashPassword(password);
    }

    public bool Compare(string hash, string password)
    {
        return BCrypt.Net.BCrypt.Verify(password, hash);
    }
}