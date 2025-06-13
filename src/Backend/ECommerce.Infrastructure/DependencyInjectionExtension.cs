using ECommerce.Domain.Repositories;
using ECommerce.Domain.Security.Cryptography;
using ECommerce.Domain.Security.Tokens;
using ECommerce.Infrastructure.DataAccess;
using ECommerce.Infrastructure.DataAccess.Repositories;
using ECommerce.Infrastructure.Security.Cryptography;
using ECommerce.Infrastructure.Security.Tokens.Jwt;
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
        services.AddScoped<IUserReadOnlyRepository, UserRepository>();
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

    private static void AddSecurity(IServiceCollection services, IConfiguration configuration)
    {
        
        var secretKey = configuration.GetValue<string>("Settings:Jwt:SecretKey")!;
        var expirationInMinutes = configuration.GetValue<uint>("Settings:Jwt:ExpirationInMinutes");
        
        services.AddScoped<IAccessTokenGenerator>(_ => new TokenGenerator(secretKey, expirationInMinutes));
        services.AddScoped<IAccessTokenValidator>(_ => new TokenValidator(secretKey));
        services.AddScoped<IPasswordEncoder, BcryptEncoder>();

    }
    
}