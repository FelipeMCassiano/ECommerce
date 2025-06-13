using ECommerce.Domain.Repositories;
using ECommerce.Domain.Services.Security;
using ECommerce.Domain.Services.Security.Password;
using ECommerce.Infrastructure.DataAccess;
using ECommerce.Infrastructure.DataAccess.Repositories;
using ECommerce.Infrastructure.Services.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.Infrastructure;

public static class DependencyInjectionExtension
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services, configuration);
        AddSecurity(services, configuration);
        
    }

    private static void AddRepositories(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IUserWriteOnlyRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");
        services.AddDbContext<ECommerceDbContext>(options =>
        {
            options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString), b => b.MigrationsAssembly("ECommerce.API"));
        });
    }

    public static void AddSecurity(IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IPasswordEncoder, BcryptEncoder>();

    }
    
}