using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories;

namespace ToDoList.Infrastructure.DataAccess.Repositories;

public class TodoTaskRepository : ITodoTaskRepository
{
    private readonly ToDoListDbContext _dbContext;

    public TodoTaskRepository(ToDoListDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Add(TodoTask task) => await _dbContext.AddAsync(task);
}