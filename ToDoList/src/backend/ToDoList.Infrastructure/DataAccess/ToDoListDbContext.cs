using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;

namespace ToDoList.Infrastructure.DataAccess;

public class ToDoListDbContext : DbContext
{
    public ToDoListDbContext(DbContextOptions opts) 
        : base(opts) { }

    public DbSet<User> Users { get; set; }
    public DbSet<TodoTask> Tasks { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder) =>
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ToDoListDbContext).Assembly);
}