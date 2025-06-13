namespace ECommerce.Domain.Services.Security.Password;

public interface IPasswordEncoder
{
    string Encode(string password);
}