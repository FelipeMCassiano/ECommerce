using ECommerce.Domain.Entities;
using ECommerce.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.DataAccess.Repositories;

public class UserRepository :  IUserWriteOnlyRepository
{
    private readonly ECommerceDbContext _dbContext;

    public UserRepository(ECommerceDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task AddAsync(User user)
    {
        await _dbContext.AddAsync(user);
    }

    public async Task<bool> ExistsUserWithEmailAsync(string email)
    {
        return await _dbContext.Users.AnyAsync(u => u.IsActive && u.Email == email);
    }
    
}