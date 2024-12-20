using ToDoList.Domain.Repositories;

namespace ToDoList.Infrastructure.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private readonly ToDoListDbContext _dbContext;

    public UnitOfWork(ToDoListDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Commit() =>
        await _dbContext.SaveChangesAsync();
}