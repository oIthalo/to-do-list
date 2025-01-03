using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Repositories;

public interface ITodoTaskRepository
{
    Task Add(TodoTask task);
    void Update(TodoTask task);
    Task<TodoTask?> GetById(User user, long id);
    Task<IList<TodoTask>?> GetAllUserTasks(User user);
}