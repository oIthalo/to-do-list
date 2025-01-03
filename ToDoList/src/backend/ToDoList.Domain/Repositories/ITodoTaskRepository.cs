using ToDoList.Domain.Entities;

namespace ToDoList.Domain.Repositories;

public interface ITodoTaskRepository
{
    Task Add(TodoTask task);
}