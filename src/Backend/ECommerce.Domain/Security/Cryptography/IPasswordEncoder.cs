namespace ECommerce.Domain.Security.Cryptography;

public interface IPasswordEncoder
{
    string Encode(string password);
    bool Compare(string hash, string password);
}