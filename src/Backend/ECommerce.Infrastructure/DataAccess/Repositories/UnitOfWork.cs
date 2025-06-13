using ECommerce.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure.DataAccess.Repositories;

public class UnitOfWork : IUnitOfWork
{
    private readonly ECommerceDbContext _context;

    public UnitOfWork(ECommerceDbContext context)
    {
        _context = context;
    }

    public async Task CommitAsync()
    {
        
        await _context.SaveChangesAsync();
    }
}