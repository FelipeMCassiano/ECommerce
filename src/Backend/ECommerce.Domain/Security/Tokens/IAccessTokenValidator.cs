namespace ECommerce.Domain.Security.Tokens;

public interface IAccessTokenValidator
{
    Guid ValidateAndGetUserId(string token);
}