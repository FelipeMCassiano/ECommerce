using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Repositories;

public interface IUserReadOnlyRepository
{
    Task<User?> GetUserByEmail(string email);
    
}