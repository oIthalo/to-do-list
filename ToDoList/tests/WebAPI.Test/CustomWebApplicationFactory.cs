using CommonTestUtilities.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using ToDoList.Infrastructure.DataAccess;

namespace WebAPI.Test;

public class CustomWebApplicationFactory : WebApplicationFactory<Program>
{
    private ToDoList.Domain.Entities.User _user = default!;
    private string _password = null!;

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("Test")
            .ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(x => x.ServiceType == typeof(DbContextOptions<ToDoListDbContext>));
                if (descriptor is not null)
                    services.Remove(descriptor);

                var provider = services.AddEntityFrameworkInMemoryDatabase().BuildServiceProvider();

                services.AddDbContext<ToDoListDbContext>(opts =>
                {
                    opts.UseInMemoryDatabase("InMemoryDbForTesting");
                    opts.UseInternalServiceProvider(provider);
                });

                using var scope = services.BuildServiceProvider().CreateScope();
                var dbContext = scope.ServiceProvider.GetRequiredService<ToDoListDbContext>();

                dbContext.Database.EnsureDeleted();

                StartDataBase(dbContext);
            });
    }

    public ToDoList.Domain.Entities.User GetUser() => _user;
    public string GetPassword() => _password;
    public string GetEmail() => _user.Email;

    private void StartDataBase(ToDoListDbContext dbContext)
    {
        (_user, _password) = UserBuilder.Build();

        dbContext.Users.Add(_user);
        dbContext.SaveChanges();
    }
}