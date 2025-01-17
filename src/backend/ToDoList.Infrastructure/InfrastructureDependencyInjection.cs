﻿using FluentMigrator.Runner;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using ToDoList.Domain.Repositories;
using ToDoList.Domain.Security.Criptography;
using ToDoList.Domain.Security.Tokens;
using ToDoList.Domain.Services.LoggedUser;
using ToDoList.Infrastructure.DataAccess;
using ToDoList.Infrastructure.DataAccess.Repositories;
using ToDoList.Infrastructure.Extensions;
using ToDoList.Infrastructure.Security.Criptography;
using ToDoList.Infrastructure.Security.Tokens.Access.Generator;
using ToDoList.Infrastructure.Security.Tokens.Access.Validator;
using ToDoList.Infrastructure.Security.Tokens.Refresh.Generator;
using ToDoList.Infrastructure.Services.LoggedUser;

namespace ToDoList.Infrastructure;

public static class InfrastructureDependencyInjection
{
    public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddRepositories(services);
        AddTokens(services, configuration);
        AddEncripter(services);
        AddLoggedUser(services);

        if (configuration.IsUnitTestEnviroment())
            return;

        AddFluentMigrator(services, configuration);
        AddDbContext(services, configuration);
    }

    private static void AddDbContext(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionStringBuilder();

        services.AddDbContext<ToDoListDbContext>(opts =>
        {
            opts.UseSqlServer(connectionString);
        });
    }

    private static void AddRepositories(IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<ITodoTaskRepository, TodoTaskRepository>();
        services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
    }

    private static void AddTokens(IServiceCollection services, IConfiguration configuration)
    {
        var signingKey = configuration.GetValue<string>("Settings:JWT:SigningKey");
        var expiration = configuration.GetValue<uint>("Settings:JWT:ExpirationInMinutes");

        services.AddScoped<IAccessTokenGenerator>(opts => new GenerateAccessToken(expiration, signingKey!));
        services.AddScoped<ITokenValidator>(opts => new TokenValidator(signingKey!));
        services.AddScoped<IRefreshTokenGenerator, RefreshTokenGenerator>();
    }

    private static void AddEncripter(IServiceCollection services) => services.AddScoped<IPasswordEncripter, BCryptNet>();

    private static void AddFluentMigrator(IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.ConnectionStringBuilder();

        services.AddFluentMigratorCore().ConfigureRunner(opts =>
        {
            opts.AddSqlServer()
            .WithGlobalConnectionString(connectionString)
            .ScanIn(Assembly.Load("ToDoList.Infrastructure")).For.All();
        });
    }

    private static void AddLoggedUser(IServiceCollection services) => services.AddScoped<ILoggedUser, LoggedUser>();
}