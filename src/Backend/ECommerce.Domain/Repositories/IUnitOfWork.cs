namespace ECommerce.Domain.Repositories;

public interface IUnitOfWork
{
    Task CommitAsync();
}