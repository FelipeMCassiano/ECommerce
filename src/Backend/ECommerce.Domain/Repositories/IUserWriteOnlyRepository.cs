using ECommerce.Domain.Entities;

namespace ECommerce.Domain.Repositories;

public interface IUserWriteOnlyRepository
{
   Task AddAsync(User user); 
   Task<bool>  ExistsUserWithEmailAsync(string email);
}