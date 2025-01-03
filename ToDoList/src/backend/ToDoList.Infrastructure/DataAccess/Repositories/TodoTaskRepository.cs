using Microsoft.EntityFrameworkCore;
using ToDoList.Domain.Entities;
using ToDoList.Domain.Repositories;

namespace ToDoList.Infrastructure.DataAccess.Repositories;

public class TodoTaskRepository : ITodoTaskRepository
{
    private readonly ToDoListDbContext _dbContext;

    public TodoTaskRepository(ToDoListDbContext dbContext) =>
        _dbContext = dbContext;

    public async Task Add(TodoTask task) => await _dbContext.Tasks.AddAsync(task);

    public async Task<IList<TodoTask>?> GetAllUserTasks(User user) => await _dbContext.Tasks.AsNoTracking().Where(x => x.Active && x.UserIdentifier.Equals(user.Identifier)).ToListAsync();

    public async Task<TodoTask?> GetById(User user, long id) => await _dbContext.Tasks.AsNoTracking().FirstOrDefaultAsync(x => x.Active && x.Id.Equals(id) && x.UserIdentifier.Equals(user.Identifier));

    public void Update(TodoTask task) => _dbContext.Tasks.Update(task);
}