using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Security.Tokens;
using ToDoList.Infrastructure.DataAccess;
using ToDoList.Infrastructure.DataAccess.Repositories;
using ToDoList.Infrastructure.Security.Tokens.Access.Generator;

namespace ToDoList.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddDbContext(services, configuration);
        AddRepositories(services);
        AddTokens(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("Connection");

        services.AddDbContext<ToDoListDbContext>(opts =>
        {
            opts.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var signingKey = configuration.GetValue<string>("Settings:JWT:SigningKey");
        var expiration = configuration.GetValue<uint>("Settings:JWT:ExpirationInMinutes");

        services.AddScoped<IAccessTokenGenerator>(opts => new GenerateAccessToken(expiration, signingKey!));
    }
}